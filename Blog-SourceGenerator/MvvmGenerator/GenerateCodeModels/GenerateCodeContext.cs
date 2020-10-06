using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace MvvmGenerator.GenerateCodeModels
{
    class GenerateCodeContext
    {
        private Dictionary<string, Clazz> InnerClasses { get; } = new();

        public IReadOnlyDictionary<string, Clazz> Classes => InnerClasses.ToImmutableDictionary();

        public GenerateCodeContext(IEnumerable<Clazz>? classes = null)
        {
            if (classes != null)
            {
                AddClasses(classes);
            }
        }

        public void AddClasses(IEnumerable<Clazz> classes)
        {
            foreach (var c in classes)
            {
                InnerClasses.Add(c.Name, c);
            }
        }

    }
}
