using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmGenerator.GenerateCodeModels
{
    class Clazz
    {
        public string Namespace { get; }
        public string Name { get; }
        public bool IsAlreadyImplementsINPC { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public IDictionary<string, Property> PropertyMap { get; }

        public Clazz(string ns, string name, IEnumerable<Property> properties)
        {
            Namespace = ns;
            Name = name;
            Properties = properties.ToArray();
            PropertyMap = Properties.ToDictionary(x => x.Name);
        }

        public string ToCSharpCode()
        {
            var s = new StringBuilder();
            s.Append($@"
namespace {Namespace}
{{
    public partial class {Name} : System.ComponentModel.INotifyPropertyChanged
    {{
        {generatePropertyChanged(IsAlreadyImplementsINPC)}");

            foreach (var prop in Properties)
            {
                s.Append(prop.ToCSharpCode());
            }

            s.Append($@"
    }}
}}
");
            return s.ToString();

            static string generatePropertyChanged(bool isAlreadyImplementsINPC) =>
                isAlreadyImplementsINPC ?
                    "" :
                    "public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;";
        }
    }
}
