using Framework.HelperClasses;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using Framework.BaseClasses;

namespace Framework.PageClasses
{
    public class Browser
    {
        public static void Open()
        {
            String url = ConfigurationManager.AppSettings["URL"];
            Driver.Instance.Navigate().GoToUrl(url);
            //To Handale SSL Certificate Error
            if (GenericHelper.IsElemetPresent(By.LinkText("Continue to this website (not recommended).")))
            {
                GenericHelper.ExecuteScript("document.getElementById('overridelink').click()");
            }
            else
            {
            }
        }
        public static void Maximize()
        {
            Driver.Instance.Manage().Window.Maximize();
        }
        public static void GoBack()
        {
            Driver.Instance.Navigate().Back();
        }
        public static void RefreshPage()
        {
            Driver.Instance.Navigate().Refresh();
        }
        public static void SwitchToWindow(int index = 0)
        {
            Driver.WaitFor(1);
            ReadOnlyCollection<string> windows = Driver.Instance.WindowHandles;
            if ((windows.Count - 1) < index)
            {
                throw new NoSuchWindowException("Invalid Browser Window Index" + index);
            }
            Driver.Instance.SwitchTo().Window(windows[index]);
            Driver.WaitFor(1);
            Browser.Maximize();
        }
        public static void SwitchToParent()
        {
            var windowids = Driver.Instance.WindowHandles;
            for (int i = windowids.Count - 1; i > 0;)
            {
                Driver.Instance.Close();
                i = i - 1;
                Driver.WaitFor(1);
                Driver.Instance.SwitchTo().Window(windowids[i]);
            }
            Driver.Instance.SwitchTo().Window(windowids[0]);
        }
        public static void SwitchToIFrame(By locator)
        {
            Driver.Instance.SwitchTo().Frame(DriverBase.Instance.FindElement(locator));
        }

        public static void GoTo()
        {
            
        }
    }
}

















