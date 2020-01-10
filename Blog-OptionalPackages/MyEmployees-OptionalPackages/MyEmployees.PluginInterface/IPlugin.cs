using System;
using System.Collections;

namespace MyEmployees.PluginInterface
{
    public interface IPlugin
    {
        string Name { get; }

        string GetDescription();

        bool Execute(IList data, string filePath);

        event EventHandler OnExecute;
    }
}
