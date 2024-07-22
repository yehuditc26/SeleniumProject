using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.PagesObjects
{
    public class GoogleResultsPage
    {
        private IWebDriver driver;
        private IList<IWebElement> Results;
        ReadOnlyCollection<IWebElement> searchResults;
        private WebDriverWait wait;

        public GoogleResultsPage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); // Initialize explicit wait

        }

        public bool ResultsDisplayed()
        {
            // Get the search results panel that contains the link for each result.
            IWebElement resultsPanel = driver.FindElement(By.Id("search"));

            // Get all the links only contained within the search result panel.
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible((By.XPath(".//a"))));
            searchResults = resultsPanel.FindElements(By.XPath(".//a"));
            return searchResults.Count() > 0;   
        }
        public string GetFirstResultTitle()
        {
            return searchResults[0].FindElement(By.TagName("h3")).Text;
        }
        public void ClickFirstResult()
        {
            searchResults[0].FindElement(By.TagName("h3")).Click();
        }
    }
}