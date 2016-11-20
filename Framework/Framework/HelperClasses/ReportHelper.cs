using RelevantCodes.ExtentReports;
using System;
using System.Configuration;

namespace Framework.HelperClasses
{
    public class ReportHelper
    {
        static string reportPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ReportPath"];
        static string now = DateTime.Now.ToString("MM-dd-yyy hh-mm tt ");
        public static readonly ExtentReports _instance = new ExtentReports(reportPath + now + " Test Report.html", DisplayOrder.NewestFirst);
        static ReportHelper() { }
        public ReportHelper() { }
        public static ExtentReports ReportInstance
        {
            get
            {
                return _instance;
            }
        }
    }
}
