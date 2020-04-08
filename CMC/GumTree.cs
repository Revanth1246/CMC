using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using CMC.Page_Objects;
using CMC.Utility_Scripts;
using CMC.Class_Modules;
using NUnit.Framework;
using System;

namespace CMC
{
    public class GumTree:Shared_Functions
    {
        GumTree_Search GTree = new GumTree_Search();

        [OneTimeSetUp]
        public void Setup()
        {
            URL = ConfigurationManager.AppSettings["URL"];
            TestContext.Progress.WriteLine(URL);
            datapath = ConfigurationManager.AppSettings["datapath"];
            TestContext.Progress.WriteLine("Datapath is " + datapath);
            CreateHTMLReport("CMC_Automation_TestResult");
            extentTest = extent.StartTest("CMC_Results", "CMC_GumTree");
            driver = getBrowser("chrome",URL);
          
        }

        [Test]
        public void GumTree_Test()
        {
            try
            {
                extentTest = extent.StartTest("GumTree_EnterSearchText", "GumTree_EnterSearchText");               
                GTree.Search_Results();
                extent.EndTest(extentTest);           
                extent.Flush();
                extentTest = extent.StartTest("Results_Page", "Results_Page");
                TestContext.Progress.WriteLine("Calling Results_Count Method");
                GTree.Results_Count();
                extent.EndTest(extentTest);             
                extent.Flush();
                extentTest = extent.StartTest("Ad_Traversing", "Ad_Traversing");
                TestContext.Progress.WriteLine("Calling TraverseResults Method");
                GTree.TraverseResults();
                extent.EndTest(extentTest);
                extent.Flush();
            }
            catch(Exception GTree_Error)
            {
                TestContext.Progress.WriteLine("GTree_Error Error:"+ GTree_Error.Message);
                Capture_Screenshot("GumTree_Error", "Fail", "GumTree Error:" + GTree_Error.Message);
            }
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            try
            {
                extentTest.Log(LogStatus.Info, "Execution Completed");
               // writing everything to document.
                extent.Flush();
                driver.Close();
                TestContext.Progress.WriteLine("Execution Completed");
            }
            catch (Exception e) { }
           
        }
    }
}
