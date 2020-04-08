using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

using CMC.Utility_Scripts;
using System.Threading;

namespace CMC.Page_Objects
{
    public class EnterSearchTextObjects:Shared_Functions
    {

        
        //DropDown text
        public IWebElement Category_dropdown => driver.FindElement(By.ClassName("search-bar__category-name"));

        public IWebElement Category_value => driver.FindElement(By.XPath("//div[text()='Cars & Vehicles']"));
        //Search Text field
        public IWebElement Search_Textbox => driver.FindElement(By.Id("search-query"));

        public IWebElement Search_value => driver.FindElement(By.XPath("//*[@id='search-query-wrp']/ul/li[1]"));
        //Search Location
            
        public IWebElement Search_Location => driver.FindElement(By.Id("search-area"));

        //Search Location

        public IWebElement Search_Distance => driver.FindElement(By.Id("srch-radius-input"));

        public IWebElement Distance_value => driver.FindElement(By.XPath("//div[text()='250km']"));

        //Next button on Search button
        public IWebElement Search_Submit_button => driver.FindElement(By.Id("//*[@type='submit']"));

        

       

    }
}
