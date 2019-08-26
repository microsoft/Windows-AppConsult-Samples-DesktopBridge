using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace WpfUiTesting.Tests
{
    [TestClass]
    public class WpfTest
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        //private const string WpfAppId = @"C:\Users\mpagani\Source\Samples\WpfUiTesting\WpfUiTesting.Wpf\bin\Debug\WpfUiTesting.Wpf.exe";
        private const string WpfAppId = @"WpfUiTesting_e627vcndsd2rc!App";

        protected static WindowsDriver<WindowsElement> session;
        protected static WindowsDriver<WindowsElement> DesktopSession;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            if (session == null)
            {
                // Create a new session to bring up an instance of the Calculator application
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", WpfAppId);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                DesktopSession = null;
                try
                {
                    Console.WriteLine("Trying to Launch App");
                    DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
                }
                catch
                {
                    Console.WriteLine("Failed to attach to app session (expected).");
                }

                appiumOptions.AddAdditionalCapability("app", "Root");
                DesktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
                var mainWindow = DesktopSession.FindElementByAccessibilityId("WpfUITestingMainWindow");
                Console.WriteLine("Getting Window Handle");
                var mainWindowHandle = mainWindow.GetAttribute("NativeWindowHandle");
                mainWindowHandle = (int.Parse(mainWindowHandle)).ToString("x"); // Convert to Hex
                appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("appTopLevelWindow", mainWindowHandle);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
            }

            //if (session == null)
            //{
            //    var appiumOptions = new AppiumOptions();
            //    appiumOptions.AddAdditionalCapability("app", WpfAppId);
            //    appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            //    session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

            //    session.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5000);
            //}
        }

        [TestInitialize]
        public void Clear()
        {
            var txtName = session.FindElementByAccessibilityId("txtName");
            txtName.Clear();
        }
        

        [TestMethod]
        public void AddNameToTextBox()
        {
            var txtName = session.FindElementByAccessibilityId("txtName");
            txtName.Clear();
            txtName.SendKeys("Matteo");
            session.FindElementByAccessibilityId("sayHelloButton").Click();
            var txtResult = session.FindElementByAccessibilityId("txtResult");
            Assert.AreEqual(txtResult.Text, $"Hello {txtName.Text}");
        }

        [TestMethod]
        public void AddWrongNameToTextBox()
        {
            var txtName = session.FindElementByAccessibilityId("txtName");
            txtName.SendKeys("Matteo");
            session.FindElementByAccessibilityId("sayHelloButton").Click();
            var txtResult = session.FindElementByAccessibilityId("txtResult");
            Assert.AreEqual(txtResult.Text, $"Hello Matt");
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            if (session != null)
            {
                session.Close();
                session.Quit();
            }
        }
    }
}
