using System.Collections.Generic;
using System.Text;

namespace MvvmGenerator.GenerateCodeModels
{
    class Property
    {
        public List<string> RelatedProperties { get; } = new List<string>();
        public string TypeName { get; }
        public string FieldName { get; }
        public string Name { get; }
        public bool IsAutoGenerateTarget { get; }

        public Property(string typeName, string fieldName, string name, bool isAutoGenerateTarget)
        {
            TypeName = typeName;
            FieldName = fieldName;
            Name = name;
            IsAutoGenerateTarget = isAutoGenerateTarget;
        }

        public string ToCSharpCode()
        {
            var s = new StringBuilder();
            s.Append($"private static readonly System.ComponentModel.PropertyChangedEventArgs {Name}PropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs(nameof({Name}));");
            if (IsAutoGenerateTarget)
            {
                s.Append($@"public {TypeName} {Name}
{{
    get => this.{FieldName};
    set
    {{
        if (System.Collections.Generic.EqualityComparer<{TypeName}>.Default.Equals(this.{FieldName}, value))
        {{
            return;
        }}
        this.{FieldName} = value;
        PropertyChanged?.Invoke(this, {Name}PropertyChangedEventArgs);");

                foreach (var prop in RelatedProperties)
                {
                    s.Append($"PropertyChanged?.Invoke(this, {prop}PropertyChangedEventArgs);");
                }

                s.Append($@"
        {Name}Changed();
    }}
}}

partial void {Name}Changed();");
            }

            return s.ToString();
        }
    }
}
