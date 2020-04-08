using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Threading;
using System.Drawing.Imaging;
//using ExcelLibrary.SpreadSheet;
//using ExcelLibrary.CompoundDocumentFormat;
using OpenQA.Selenium.Remote;
using RelevantCodes.ExtentReports;
using OpenQA.Selenium.Interactions;

using System.Diagnostics;
using System.IO;

namespace CMC.Utility_Scripts
{
    public class Shared_Functions
    {
        public static IWebDriver driver;   
        public static string datapath,URL;     
        public static string ScreenshotPath, Resultspath;
        public static ExtentReports extent;
        public static ExtentTest extentTest;
        public string screenshotpic;            

        public void CreateHTMLReport(String TestName)
        {
            Console.WriteLine("Datapath is " + datapath);
            Resultspath = datapath + "TestResults\\" + TestName + "\\" + TestName + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            CreateDirectory(Resultspath);
            extent = new ExtentReports(Resultspath + "\\TestResults.html", DisplayOrder.OldestFirst);
            extent.LoadConfig(@datapath + "extent-config-latest.xml");

        }
        public static string CreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Console.WriteLine("That path exists already.");

            }
            else
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));


                Console.WriteLine(path);
            }
            ScreenshotPath = path;
            return path;
        }
        public static IWebDriver getBrowser(String browserType,string url)
        {
            Console.WriteLine("Browsertype is " + browserType);

            if (driver == null)
            {

                if (browserType.ToLower() == ("firefox"))

                {
                    Console.WriteLine("Browsertype is firefox");

                    FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(datapath + "Drivers\\", "geckodriver.exe");
                    driver = new FirefoxDriver(service);
                    //driver = new FirefoxDriver();
                }
                else if (browserType.ToLower() == ("chrome"))
                {

                    Console.WriteLine("Browsertype is chrome");
                    string chromeDriverDirectory = datapath + "Drivers\\";
                    var options = new ChromeOptions();
                    options.AddArgument("no-sandbox");
                    //driver = new ChromeDriver(datapath + "Drivers\\");
                    driver = new ChromeDriver(chromeDriverDirectory, options,
                        TimeSpan.FromMinutes(2));
                }
                else if (browserType.ToLower() == ("ie"))
                {
                    Console.WriteLine("Browsertype is ie");


                    String service = @datapath + "Drivers\\IEDriverServer.exe";
                    System.Environment.SetEnvironmentVariable("webdriver.ie.driver", service);

                    driver = new InternetExplorerDriver();

                    TestContext.Progress.WriteLine("Choosed IE Driver successfully");
                }

            }

            driver.Navigate().GoToUrl(url);
            // maximizing the window
            driver.Manage().Window.Maximize();
            return driver;

        }
        public static void driver_wait(By by)
        {
            TestContext.Progress.WriteLine("Waiting for " + by.ToString());
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(150));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                Console.WriteLine("Element is visible");
                return d.FindElement(by);
            });
        }
        public static String GetText(IWebElement element)
        {
            string text = null;

            text = element.Text;

            return text;
        }
        public void ScrollToBottom()
        {
            long scrollHeight = 0;

            do
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                if (newScrollHeight == scrollHeight)
                {
                    break;
                }
                else
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(400);
                }
            } while (true);
        }
        public static void Capture_Screenshot(String filename, String Resultstatus, String Message)
        {
            //var location = screenshotpath +""+ filename+"_" + DateTime.Now + ".png";
            var location = ScreenshotPath + "\\" + filename + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            var ssdriver = driver as ITakesScreenshot;
            var screenshot = ssdriver.GetScreenshot();
            //screenshot.SaveAsFile(location, ImageFormat.Png);
            screenshot.SaveAsFile(location, OpenQA.Selenium.ScreenshotImageFormat.Png);
            if (Resultstatus.ToLower() == "fail")
            {
                extentTest.Log(LogStatus.Fail, Message + " " + extentTest.AddScreenCapture(location));
            }
            else
            {
                extentTest.Log(LogStatus.Pass, Message + " " + extentTest.AddScreenCapture(location));
            }

        }
        public static void WaitForElement(IWebElement Element)
        {
            try
            {
                TestContext.Progress.WriteLine("Waiting for " + Element.GetAttribute("value"));
            }
            catch (Exception e) { }

            for (int i = 0; i < 300; i++)
            {
                Thread.Sleep(1000);
                try
                {
                    if (Element.Displayed)
                    {
                        Console.WriteLine("Element displayed at " + i);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Element NOT displayed at " + i);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Element NOT displayed at " + i);
                }

            }
        }
        public static void SelectDropdown(IWebElement element, string value)
        {

            Scroll(element);
            Thread.Sleep(1000);
            SelectElement se = new SelectElement(element);         
            se.SelectByText(value);
            Thread.Sleep(2000);

        }
        public static int GetRandomNo(int start,int end)
        {
            Random r = new Random();
            int randomNo = r.Next(start, end);
            return randomNo;
        }
     
        public static void ClickElement(IWebElement element)
        {
            try
            {
                Scroll(element);
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Error while scrolling element with data value ");
            }

            Thread.Sleep(1000);
            element.Click();
            Thread.Sleep(1000);
           
        }
        public static void Scroll(IWebElement Element)
        {
            try
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(Element);
                actions.Perform();
                Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                TestContext.Progress.WriteLine("Error while scrolling " + e.Message);
            }
        }

    }
}
