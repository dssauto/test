using Framework.BaseClasses;
using Framework.HelperClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;

namespace Framework.PageClasses
{
    public class VerifyElement : BaseSetup
    {
        public static void IsPresent(string elementName, string elementXPath)
        {
            try
            {
                Driver.WaitForElement(elementXPath);
                Assert.AreEqual(elementName, Driver.Instance.FindElement(By.XPath(elementXPath)).Text);
                string screenName = ScreenshotHelper.TakeScreenshot();
                string scrrenShotPath = TestReport.AddScreenCapture(screenName);
                TestReport.Log(LogStatus.Pass, "'" + elementName + "' - Verification passed!", scrrenShotPath);
            }
            catch (AssertionException e)
            {
                string screenName = ScreenshotHelper.TakeScreenshot();
                string scrrenShotPath = TestReport.AddScreenCapture(screenName);
                TestReport.Log(LogStatus.Warning, "Verification failed! - " + e.Message, scrrenShotPath);
            }
        }
        public static void TitleAreEqualTo(string title)
        {
            try
            {
                (new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10))).Until(ExpectedConditions.TitleContains(title));
                Assert.AreEqual(title, Driver.Instance.Title);
                string screenName = ScreenshotHelper.TakeScreenshot();
                string scrrenShotPath = TestReport.AddScreenCapture(screenName);
                TestReport.Log(LogStatus.Pass, title + " Title verification passed!", scrrenShotPath);
            }
            catch (AssertionException e)
            {
                string screenName = ScreenshotHelper.TakeScreenshot();
                string scrrenShotPath = TestReport.AddScreenCapture(screenName);
                TestReport.Log(LogStatus.Warning, "Title verification failed! - " + e.Message, scrrenShotPath);
            }
        }
    }
}
