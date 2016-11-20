using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Framework.BaseClasses
{
    public class DriverBase
    {
        public static IWebDriver Instance { get; set; }
        public static void Initialize()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var relativePath = @"..\..\Drivers";
            var driverPath = Path.GetFullPath(Path.Combine(outPutDirectory, relativePath));
            switch (ConfigurationManager.AppSettings["Browser"].ToString())
            {
                case "FIREFOX":
                    Instance = new FirefoxDriver();
                    break;
                case "IE":
                    Instance = new InternetExplorerDriver(driverPath,GetIEOptions());
                    break;
                case "CHROME":                    
                    Instance = new ChromeDriver(driverPath,GetChromeOptions());
                    break;
                case "PhantomJS":
                    Instance = new PhantomJSDriver(GetPhantomJsDriverService());
                    break;
                default:
                    Instance = new FirefoxDriver();
                    break;
            }
            switch (ConfigurationManager.AppSettings["ScreenSize"].ToString())
            {
                case "Maximize":
                    Instance.Manage().Window.Maximize();
                    break;
                case "Phone":
                    Instance.Manage().Window.Size = new Size(360, 640);
                    break;
                case "iPad":
                    Instance.Manage().Window.Size = new Size(768, 1024);
                    break;
                case "1280, 720":
                    Instance.Manage().Window.Size = new Size(1280, 720);
                    break;
                case "1600, 900":
                    Instance.Manage().Window.Size = new Size(1600, 900);
                    break;
            }
        }
        private static InternetExplorerOptions GetIEOptions()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            options.IgnoreZoomLevel = true;
            options.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true);
            return options;
        }
        private static ChromeOptions GetChromeOptions()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("start-maximized");
            return option;
        }
        private static PhantomJSDriverService GetPhantomJsDriverService()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var relativePath = @"..\..\Drivers";
            var driverPath = Path.GetFullPath(Path.Combine(outPutDirectory, relativePath));
            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService(driverPath);
            service.HideCommandPromptWindow = true;
            service.IgnoreSslErrors = true;
            return service;
        }
    }
}
