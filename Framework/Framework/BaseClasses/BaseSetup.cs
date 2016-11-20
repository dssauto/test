using Framework.HelperClasses;
using Framework.PageClasses;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using System.Diagnostics;
using System.Reflection;

namespace Framework.BaseClasses
{
    public class BaseSetup
    {
        protected ExtentReports ReportLog;
        protected static ExtentTest TestReport;
        public string GetTestName()
        {
            var stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            return methodBase.Name;
        }

        [SetUp]
        public void BaseInitialize()
        {
            DriverBase.Initialize();
            ReportLog = ReportHelper.ReportInstance;
            TestReport = ReportLog.StartTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void BaseCleanup()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.Message);
            LogStatus logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = LogStatus.Fail;
                    string screenName = ScreenshotHelper.TakeScreenshot();
                    string scrrenshotPath = TestReport.AddScreenCapture(screenName);
                    TestReport.Log(LogStatus.Fail, "Screenshot on Fail", scrrenshotPath);
                    break;
                case TestStatus.Inconclusive:
                    logstatus = LogStatus.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = LogStatus.Skip;
                    break;
                default:
                    logstatus = LogStatus.Pass;
                    break;
            }
            TestReport.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            ReportLog.EndTest(TestReport);
            ReportLog.Flush();
            Driver.Close();
        }
    }
}
