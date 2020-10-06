using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;
using MvvmGenerator.GenerateCodeModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace MvvmGenerator
{
    [Generator]
    public class AutoImplementNotifyPropertyChangedGenerator : ISourceGenerator
    {
        private static readonly string AutoNotifyCode = @$"
using System;
namespace {Consts.Namespace}
{{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class {Consts.AutoNotifyAttributeName} : Attribute
    {{
        public string PropertyName {{ get; }}

        public AutoNotifyAttribute() {{}}
        public AutoNotifyAttribute(string propertyName) {{ PropertyName = propertyName; }}
    }}
}}";

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("hello", SourceText.From(AutoNotifyCode, Encoding.UTF8));
            if (context.SyntaxReceiver is not SyntaxReceiver r)
            {
                return;
            }

            var generateCodeContext = CreateGenerateCodeContext(context, r.CandidateClasses);
            GenerateCodes(context, generateCodeContext);
        }

        private void GenerateCodes(GeneratorExecutionContext context, GenerateCodeContext generateCodeContext)
        {
            foreach (var clazz in generateCodeContext.Classes.Values)
            {
                context.AddSource(clazz.Name, SourceText.From(clazz.ToCSharpCode(), Encoding.UTF8));
            }
        }

        private static GenerateCodeContext CreateGenerateCodeContext(GeneratorExecutionContext context, IEnumerable<ClassDeclarationSyntax> classes)
        {
            var options = (context.Compilation as CSharpCompilation)?.SyntaxTrees.FirstOrDefault()?.Options as CSharpParseOptions;
            var compilation = context
                .Compilation
                .AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(AutoNotifyCode, Encoding.UTF8), options));

            var autoNotifyAttributeSymbol = compilation.GetTypeByMetadataName(Consts.AutoNotifyAttributeFullName) ?? throw new InvalidOperationException();
            var inpcSymbol = compilation.GetTypeByMetadataName(typeof(INotifyPropertyChanged).FullName)?? throw new InvalidOperationException();
            return new GenerateCodeContext(classes.Select(x => processClass(x)));

            Clazz processClass(ClassDeclarationSyntax c)
            {
                var model = compilation.GetSemanticModel(c.SyntaxTree);
                if (model.GetDeclaredSymbol(c) is not ITypeSymbol typeSymbol)
                {
                    throw new InvalidOperationException();
                }

                var isAlreadyImplementedINPC = typeSymbol.Interfaces.Contains(inpcSymbol);
                return new Clazz(
                    typeSymbol.ContainingNamespace.ToDisplayString(),
                    typeSymbol.Name,
                    processFields(c.Members.OfType<FieldDeclarationSyntax>(), c.Members.OfType<PropertyDeclarationSyntax>()));
            }

            IEnumerable<Property> processFields(IEnumerable<FieldDeclarationSyntax> fields, IEnumerable<PropertyDeclarationSyntax> properties)
            {
                var candidateFields = fields
                    .Select(field => (field, model: compilation.GetSemanticModel(field.SyntaxTree)))
                    .SelectMany(x => x.field.Declaration.Variables.Select(variable => x.model.GetDeclaredSymbol(variable) as IFieldSymbol))
                    .Where(x => x.GetAttributes().Any(y => y.AttributeClass?.Equals(autoNotifyAttributeSymbol, SymbolEqualityComparer.Default) ?? false));
                var candidateProperties = properties
                    .Select(property => (property, model: compilation.GetSemanticModel(property.SyntaxTree)))
                    .Select(x => (x.property, propertySymbol: x.model.GetDeclaredSymbol(x.property) ?? throw new InvalidOperationException()))
                    .Where(x => x.propertySymbol.GetAttributes().Any(y => y.AttributeClass?.Equals(autoNotifyAttributeSymbol, SymbolEqualityComparer.Default) ?? false))
                    .ToArray();

                var result = candidateFields.Select(x => new Property(x.Type.ToString(), x.Name, getPropertyName(x), true))
                    .Where(x => !string.IsNullOrEmpty(x.Name))
                    .ToList();

                if (candidateProperties.Any())
                {
                    var propMap = result.ToDictionary(x => x.Name);
                    var dependParis = candidateProperties
                        .SelectMany(x => 
                            x.property
                                .DescendantNodes(y => !y.IsKind(SyntaxKind.IdentifierName))
                                .OfType<IdentifierNameSyntax>()
                                .Select(y => (propertyName: x.property.Identifier.Text, dependPropertyName: y.Identifier.Text)))
                        .Where(x => propMap.ContainsKey(x.dependPropertyName));
                    foreach (var pair in dependParis)
                    {
                        propMap[pair.dependPropertyName].RelatedProperties.Add(pair.propertyName);
                    }

                    result.AddRange(dependParis
                        .Select(x => x.propertyName)
                        .Distinct()
                        .Select(x => new Property("", "", x, false)));
                }

                return result;
            }

            string getPropertyName(IFieldSymbol f)
            {
                var attrData = f.GetAttributes().Single(x => x.AttributeClass?.Equals(autoNotifyAttributeSymbol, SymbolEqualityComparer.Default) ?? false);
                var overridenNameOpt = attrData.ConstructorArguments.FirstOrDefault().Value as string;
                return (f.Name.TrimStart('_'), overridenNameOpt) switch
                {
                    (_, string y) when !string.IsNullOrEmpty(y) => y,
                    ({ Length: 0 }, _) => "",
                    ({ Length: 1 } y, _) => y.ToUpper(),
                    (string y, _) => $"{y.Substring(0, 1).ToUpper()}{y.Substring(1)}",
                    _ => "",
                };
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        class SyntaxReceiver : ISyntaxReceiver
        {
            public List<ClassDeclarationSyntax> CandidateClasses { get; } = new();

            public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
            {
                static bool hasAutoNotifyAttribute(SyntaxList<AttributeListSyntax> attrs) =>
                   attrs.SelectMany(x => x.Attributes)
                       .Any(x => x.Name.ToString() == Consts.AutoNotifyName);
                static bool isCandidate(ClassDeclarationSyntax c)
                {
                    var fields = c.Members.OfType<FieldDeclarationSyntax>()
                        .Where(x => hasAutoNotifyAttribute(x.AttributeLists));
                    var properties = c.Members.OfType<PropertyDeclarationSyntax>()
                        .Where(x => hasAutoNotifyAttribute(x.AttributeLists));
                    return fields.Any() || properties.Any();
                }

                if (syntaxNode is ClassDeclarationSyntax clazz && isCandidate(clazz))
                {
                    CandidateClasses.Add(clazz);
                }
            }
        }
    }
}
