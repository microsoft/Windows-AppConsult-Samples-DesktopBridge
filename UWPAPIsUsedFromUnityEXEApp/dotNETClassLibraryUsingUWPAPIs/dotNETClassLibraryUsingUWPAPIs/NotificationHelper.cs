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
                ToastNotification toast = CreateAToast(toastTitle, toastContent);

                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
            catch (AggregateException ex)
            {
                return "Exception:" + ex.ToString() + "exMessage:" + ex.Message;
            }
            return "Ok";
        }

        private static ToastNotification CreateAToast(string toastTitle, string toastContent)
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
            return toast;
        }


        private static ScheduledToastNotification CreateAScheduleToast(
                    string toastTitle,
                    string toastContent,
                    DateTime scheduleTime)
        {

            var doc = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            var strings = doc.GetElementsByTagName("text");
            strings[0].AppendChild(doc.CreateTextNode(toastTitle));
            strings[1].AppendChild(doc.CreateTextNode("Scheduled: " + toastContent));

            var toast = new ScheduledToastNotification(doc, scheduleTime);
            return toast;
        }

        [DllExport(CallingConvention.StdCall)]
        public static string NotifyWithDelay(string toastTitle, string toastContent, int delayinMilliseconds)
        {
            try
            {
                var scheduleTime = DateTime.Now.AddMilliseconds(delayinMilliseconds);
                ScheduledToastNotification toast = CreateAScheduleToast(toastTitle, toastContent, scheduleTime);


                // Add to the schedule.
                ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
            }
            catch (AggregateException ex)
            {
                return "Exception:" + ex.ToString() + "exMessage:" + ex.Message;
            }
            return "Ok";
        }
    }
}
