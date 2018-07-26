using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNETClassLibraryUsingUWPAPIs
{
    class LiveTileHelper
    {
        [DllExport(CallingConvention.StdCall)]
        public static string UpdatePrimaryTile(string text, int durationSeconds = 10)
        {
            var template = Windows.UI.Notifications.TileTemplateType.TileSquare150x150Block;
            var tileXml = Windows.UI.Notifications.TileUpdateManager.GetTemplateContent(template);

            var tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].AppendChild(tileXml.CreateTextNode(text));

            var tileNotification = new Windows.UI.Notifications.TileNotification(tileXml);

            tileNotification.ExpirationTime = DateTime.Now.AddSeconds(durationSeconds);
            Windows.UI.Notifications.TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            return "Ok";
        }
    }
}
