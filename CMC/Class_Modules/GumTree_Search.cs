using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMC.Page_Objects;
using CMC.Utility_Scripts;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Threading;
using RelevantCodes.ExtentReports;
using NUnit.Framework;
using CMC.Class_Modules;
using CMC.Page_Objects;
using OpenQA.Selenium.Interactions;

namespace CMC.Class_Modules
{
    public class GumTree_Search : Shared_Functions
    {
        EnterSearchTextObjects GTP = new EnterSearchTextObjects();
        public void Search_Results()
        {
            WaitForElement(GTP.Search_Textbox);
            TestContext.Progress.WriteLine("Search Textbox displayed fine");
            GTP.Search_Location.SendKeys("Wollongong Region, NSW");           
            ClickElement(GTP.Search_Distance);              
            Scroll(GTP.Distance_value);         
            ClickElement(GTP.Distance_value);
            WaitForElement(GTP.Search_Textbox);
            GTP.Search_Textbox.SendKeys("Toyota");
            Thread.Sleep(4000);
            ClickElement(GTP.Search_value);
            Capture_Screenshot("GumTree_Search_Text", "Pass", "GumTree Search Test credentials");
            Thread.Sleep(10000);
        }

        public void Results_Count()
        {
               
            ResultsPageObjects resultspage = new ResultsPageObjects();
            WaitForElement(resultspage.Results_Count);     
            var Resutls_Count = GetText(resultspage.Results_Count);
            TestContext.Progress.WriteLine("Resutls_Count is "+ Resutls_Count);
            extentTest.Log(LogStatus.Pass, "Number of Search Results displayed is " + Resutls_Count);
            ScrollToBottom();
            Thread.Sleep(3000);
            var Results_page_count = resultspage.Search_Results_page.Count;
            WaitForElement(resultspage.Search_Results_page[Results_page_count-1]);
            Scroll(resultspage.Search_Results_page[Results_page_count - 1]);            
            SelectDropdown(resultspage.Search_Results_page[Results_page_count - 1], "96 results per page");
            extentTest.Log(LogStatus.Pass, "Search Resuls with Text :96 results per page selected successfully");
            WaitForElement(resultspage.Search_Results_page[Results_page_count - 1]);
            var count1 = resultspage.Results_Count_per_page[1].FindElements(By.XPath("a")).Count;
            TestContext.Progress.WriteLine("Results_Count_per_page is "+ count1);
            Capture_Screenshot("GumTree_Search_Results", "Pass", "GumTree Search Results");
        }

        public void TraverseResults()
        {
            try
            {
                ResultsPageObjects resultspage = new ResultsPageObjects();
                for (int i = 1; i <= 5; i++)
                {
                    //Thread.Sleep(2000);
                    WaitForElement(resultspage.Page_Navigation[i]);
                    ScrollToBottom();
                 //   Thread.Sleep(2000);
                    Scroll(resultspage.Page_Navigation[i]);
                    ClickElement(resultspage.Page_Navigation[i]);
                    if (i == 5)
                    {
                        WaitForElement(resultspage.Page_Navigation[i+1]);
                        IList<IWebElement> result_ads = resultspage.Results_Count_per_page[1].FindElements(By.XPath("a"));
                        //Generate random no
                        int number = GetRandomNo(1, 96);
                        TestContext.Progress.WriteLine("Random number generated is :" + number);
                        Scroll(result_ads[number]);
                        Thread.Sleep(2000);
                        ClickElement(result_ads[number]);
                        Capture_Screenshot("AdNUmber", "Pass", "Clicked GumTree Ad No:" +number);
                        Thread.Sleep(2000);
                        WaitForElement(resultspage.Result_Ad_Image_Facourite);
                        Scroll(resultspage.Result_Ad_Image_Facourite);
                        Capture_Screenshot("Ad_Images", "Pass", "Ad Images");
                        ClickElement(resultspage.Result_Ad_Image_Facourite);
                        WaitForElement(resultspage.Result_Ad_Image_Count);
                        var Image_count = resultspage.Result_Ad_Image_Count.Text;
                        TestContext.Progress.WriteLine("Image_count is :" + Image_count);
                        Capture_Screenshot("Ad Images1", "Pass", "GumTree Ad No:");
                        while (resultspage.Result_Ad_Image_RSlider.Displayed)
                        {
                            WaitForElement(resultspage.Result_Ad_Image_RSlider);
                            ClickElement(resultspage.Result_Ad_Image_RSlider);
                        }
                        Capture_Screenshot("Final_Image", "Pass", "Clicked on GumTree Final Image");
                        TestContext.Progress.WriteLine("Image_count is :" );
                    }
                }
            }
            catch(Exception Trav)
            {
                TestContext.Progress.WriteLine("Error on TraverseResulsts:"+Trav.Message);
                Capture_Screenshot("TraversError", "Fail", "Error on TraverseResulsts:" + Trav.Message);
            }
        }
     
    }
}
