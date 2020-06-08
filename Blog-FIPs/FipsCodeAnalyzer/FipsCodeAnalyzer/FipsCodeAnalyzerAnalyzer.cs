// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace FipsCodeAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class FipsAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "FipsAnalyzer";

        /// <summary>
        /// These are provided for demonstration purposes only.
        /// There are no claims or actual validation that these are truly FIPs compliant.
        /// You must test and confirm BCL implementation and NIST.
        /// </summary>
        private List<string> fipsOkayCryptoList = new List<string>() { "SHA512CryptoServiceProvider",
                                    "RSACryptoServiceProvider",
                                    "DSACryptoServiceProvider",
                                    "HMACSHA1",
                                    "DESCryptoServiceProvider",
                                    "TripleDESCryptoServiceProvider",
                                    "DSACryptoServiceProvider",
                                    "RSACryptoServiceProvider"};

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
            context.RegisterCompilationStartAction((CompilationStartAnalysisContext context) =>
            {
                context.RegisterOperationAction(
                    (OperationAnalysisContext operationAnalysisContext) =>
                    {
                        IMethodSymbol method;

                        switch (operationAnalysisContext.Operation)
                        {
                            case IInvocationOperation invocationOperation:
                                method = invocationOperation.TargetMethod;
                                break;
                            case IObjectCreationOperation objectCreationOperation:
                                method = objectCreationOperation.Constructor;
                                break;
                            default:
                                Debug.Fail($"Unhandled IOperation {operationAnalysisContext.Operation.Kind}");
                                return;
                        }

                        INamedTypeSymbol type = method.ContainingType;
                        DiagnosticDescriptor rule = null;
                        string algorithmName = string.Empty;

                       // INamedTypeSymbol? DES;
                        //DES = context.Compilation.GetOrCreateTypeByMetadataName(WellKnownTypeNames.SystemSecurityCryptographyDES);
                        string fullName = type.ToDisplayString();

                        if (fullName == null || !fullName.StartsWith("System.Security.Cryptography"))
                            return;

                        //-- this safe approach is less than ideal... 
                        //   it will result in several false positives, however,
                        //   as the .net core framework expands this is better than
                        //   trying to block the non-fips ones.
                        if (!fipsOkayCryptoList.Contains(type.Name))
                        {
                            rule = Rule;
                            algorithmName = type.Name;
                        }

                        if (rule == null)
                        {
                            return;
                        }

                        operationAnalysisContext.ReportDiagnostic(
                        Diagnostic.Create(
                            rule,
                            operationAnalysisContext.Operation.Syntax.GetLocation(),
                            operationAnalysisContext.ContainingSymbol.Name,
                            algorithmName));

                        //if (type.DerivesFrom())
                        //{
                        //    rule = DoNotUseBrokenCryptographyRule;
                        //    algorithmName = cryptTypes.DES.Name;
                        //}

                    },
                        OperationKind.Invocation,
                        OperationKind.ObjectCreation);
            });
        }


        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            // Find just those named type symbols with names containing lowercase letters.
            if (namedTypeSymbol.ContainingType != null && namedTypeSymbol.ContainingType.Name.Contains("Security"))
            {
                // For all such symbols, produce a diagnostic.
                var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
