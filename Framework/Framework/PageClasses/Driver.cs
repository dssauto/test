using Framework.BaseClasses;
using Framework.HelperClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Framework.PageClasses
{
    public class Driver: DriverBase
    {
        private static SelectElement select;
        public static void WaitFor(int iSecond)
        {
            Driver.Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(iSecond));
        }
        public static void WaitForElement(string elementXPath)
        {
            (new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10))).Until(ExpectedConditions.ElementToBeClickable(By.XPath(elementXPath)));
        }
        public static void ClickOn(string elementXPath)
        {
            Driver.WaitForElement(elementXPath);
            Driver.Instance.FindElement(By.XPath(elementXPath)).Click();
        }
        public static void ClickOnById(string elementId)
        {
            Driver.WaitForElement(elementId);
            Driver.Instance.FindElement(By.Id(elementId)).Click();
        }
        public static void ClickOnByLinkText(string linkText)
        {
            (new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10))).Until(ExpectedConditions.ElementToBeClickable(By.LinkText(linkText)));
            Driver.Instance.FindElement(By.LinkText(linkText)).Click();
        }
        public static void InsertText(string inputXPath, string inputValue)
        {
            Driver.WaitForElement(inputXPath);
            //Driver.Instance.FindElement(By.XPath(inputXPath)).Clear();
            Driver.Instance.FindElement(By.XPath(inputXPath)).SendKeys(inputValue);
        }
        public static void SelectElement(By locator, string visibletext)
        {
            select = new SelectElement(GenericHelper.GetElement(locator));
            select.SelectByText(visibletext);
        }
        public static void SelectFromDropDown(string eXPath, string eValue)
        {
            Driver.WaitForElement(eXPath);
            Driver.SelectElement(By.Id(eXPath), eValue);
        }
        public static void Select(string _locationPath, string nDivision)
        {
            Driver.WaitForElement(_locationPath);
            Driver.Instance.FindElement(By.XPath(_locationPath)).SendKeys(nDivision);
        }
        public static void GoBack()
        {
            Driver.Instance.Navigate().Back();
        }
        public static void Close()
        {
            Instance.Quit();
        }
    }
}
