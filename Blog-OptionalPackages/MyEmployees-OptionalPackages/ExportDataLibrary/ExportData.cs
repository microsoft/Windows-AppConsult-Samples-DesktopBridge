using CsvHelper;
using MyEmployees.PluginInterface;
using System;
using System.Collections;
using System.IO;

namespace ExportDataLibrary
{
    public class ExportData: IPlugin
    {
        public string Name => "Export Data";

        public event EventHandler OnExecute;

        public bool Execute(IList data, string filePath)
        {
            try
            {
                using (TextWriter textWriter = File.CreateText(filePath))
                {
                    CsvWriter csvWriter = new CsvWriter(textWriter);
                    csvWriter.Configuration.Delimiter = ";";
                    csvWriter.WriteRecords(data);
                }

                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public string GetDescription()
        {
            return "Export data to CSV";
        }
    }
}
