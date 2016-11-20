using Framework.PageClasses;
using OpenQA.Selenium;
using System;
using System.Configuration;

namespace Framework.HelperClasses
{
    public class ScreenshotHelper
    {
        public static string TakeScreenshot()
        {
            string screenshotPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ReportPath"];
            string now = DateTime.Now.ToString("MMddyyyhhmmss");
            string screenName = now + "Screenshot.png";
            try
            {
                Screenshot ss = ((ITakesScreenshot)Driver.Instance).GetScreenshot();
                ss.SaveAsFile(screenshotPath + screenName, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return screenName;
        }
    }
}
