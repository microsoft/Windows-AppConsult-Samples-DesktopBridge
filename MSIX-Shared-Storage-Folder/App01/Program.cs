using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace App01
{
	class Program
	{
		static void Main(string[] args)
		{
			WriteAndReadSharedFile();
			Console.WriteLine("Press ENTER to exit");
			Console.ReadLine();
		}



		private static void WriteAndReadSharedFile()
		{
			StorageFolder sharedFolder1 = ApplicationData.Current.GetPublisherCacheFolder("Folder1");
			string fileFullPath = System.IO.Path.Combine(sharedFolder1.Path, "TextFile1.txt");
			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
			using (StreamWriter sw = File.AppendText(fileFullPath))
			{
				sw.WriteLine($"From {Package.Current.DisplayName}\t{DateTime.Now.ToLongDateString()}-{DateTime.Now.ToLongTimeString()}");
			}

			string fileText = System.IO.File.ReadAllText(fileFullPath);
			Console.WriteLine(fileText);
		}

	}
}
