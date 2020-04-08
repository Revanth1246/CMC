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
    public class ResultsPageObjects : Shared_Functions
    {
        //Results COunt
        public IWebElement Results_Count => driver.FindElement(By.XPath("//h1[@class='breadcrumbs__summary--enhanced']"));

        //Results_Per_Page dropdown
        public IList<IWebElement> Search_Results_page => driver.FindElements(By.XPath("//select[@class='select__select']"));
        //Results_Per_Page count
        public IList<IWebElement> Results_Count_per_page => driver.FindElements(By.XPath("//*[@class='panel-body panel-body--flat-panel-shadow user-ad-collection__list-wrapper']"));

        public IList<IWebElement> Page_Navigation => driver.FindElements(By.XPath("//div[@class='page-number-navigation']/a"));

        //Image Favourite Symbol
        public IWebElement Result_Ad_Image_Facourite => driver.FindElement(By.XPath("//*[@class='vip-ad-image__legend']"));

        //Right Slider
        public IWebElement Result_Ad_Image_RSlider => driver.FindElement(By.XPath("//*[@class='vip-ad-gallery__controls']/button[2]"));


        // Get Image count
        public IWebElement Result_Ad_Image_Count => driver.FindElement(By.XPath("//*[@class='vip-ad-gallery__carousel-legend']"));

    }
}
