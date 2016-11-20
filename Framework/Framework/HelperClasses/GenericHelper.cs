using Framework.PageClasses;
using OpenQA.Selenium;
using System;

namespace Framework.HelperClasses
{
    public class GenericHelper
    {
        public static bool IsElemetPresent(By locator)
        {
            try
            {
                return Driver.Instance.FindElements(locator).Count == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static IWebElement GetElement(By locator)
        {
            if (IsElemetPresent(locator))
                return Driver.Instance.FindElement(locator);
            else
                throw new NoSuchElementException("Element Not Found : " + locator.ToString());
        }
        public static object ExecuteScript(string script)
        {
            IJavaScriptExecutor executor = ((IJavaScriptExecutor)Driver.Instance);
            return executor.ExecuteScript(script);
        }
    }
}
