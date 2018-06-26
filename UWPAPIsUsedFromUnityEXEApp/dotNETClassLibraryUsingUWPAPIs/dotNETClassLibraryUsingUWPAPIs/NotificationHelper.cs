using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace dotNETClassLibraryUsingUWPAPIs
{
    class NotificationHelper
    {
        [DllExport(CallingConvention.StdCall)]
        public static string Notify(string toastTitle, string toastContent)
        {
            try
            {
                string xml = @"<toast activationType='foreground'>
                                        <visual>
                                                 <binding template='ToastGeneric'>
                                                 </binding>
                                        </visual>
                               </toast>";

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                var binding = doc.SelectSingleNode("//binding");

                var el = doc.CreateElement("text");
                el.InnerText = toastTitle;

                binding.AppendChild(el);

                el = doc.CreateElement("text");
                el.InnerText = toastContent;
                binding.AppendChild(el);

                var toast = new ToastNotification(doc);

                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
            catch (AggregateException ex)
            {
                return "Exception:" + ex.ToString() + "exMessage:" + ex.Message;
            }
            return "Ok";
        }
    }
}
