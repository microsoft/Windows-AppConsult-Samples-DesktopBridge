using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App03_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		public MainPage()
		{
			this.InitializeComponent();
		}


		private async Task WriteAndReadSharedFile()
		{
			StorageFolder sharedFolder1 = ApplicationData.Current.GetPublisherCacheFolder("Folder1");
			StorageFile sharedFile1 = await sharedFolder1.CreateFileAsync("TextFile1.txt",
				Windows.Storage.CreationCollisionOption.OpenIfExists);
			await FileIO.AppendTextAsync(sharedFile1,
			$"From {Package.Current.DisplayName}\t{DateTime.Now.ToLongDateString()}-{DateTime.Now.ToLongTimeString()}\r\n");

			string fileText = await Windows.Storage.FileIO.ReadTextAsync(sharedFile1);
			var f = new Flyout { Content = new TextBlock { Text = fileText } };
			f.ShowAt(btnWriteAndRead);
		}

		private async void btnWriteAndRead_Tapped(object sender, TappedRoutedEventArgs e)
		{
			await WriteAndReadSharedFile();
		}
	}
}
