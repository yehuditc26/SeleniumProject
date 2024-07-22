using NUnit.Framework.Interfaces;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumProject.PagesObjects;

namespace SeleniumProject
{
    [TestFixture]
    public class GoogleSearchTest
    {
        private IWebDriver driver;
        private GoogleHomePage googleHomePage;
        private GoogleResultsPage googleResultsPage;
        public static IEnumerable<TestData> TestCases => XmlDataReader.ReadTestData("T:\\הנדסת תוכנה\\שנה ב\\קבוצה ב\\קורס אוטומציה\\שיעור4\\SeleniumProject\\SeleniumProject\\TestData.xml");


        [OneTimeSetUp]
        public void SetUp()
        {
            string path = "T:\\הנדסת תוכנה\\שנה ב\\קבוצה ב\\קורס אוטומציה\\שיעור4\\SeleniumProject\\SeleniumProject";

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver(path + @"\drivers\");
            googleHomePage = new GoogleHomePage(driver);
            googleResultsPage = new GoogleResultsPage(driver);
        }

        [Test, TestCaseSource(nameof(TestCases))]

        public void TestGoogleSearch(TestData testData)
        {
            // Navigate to Google
            googleHomePage.NavigateTo();

            // Verify the title of the page
            ClassicAssert.AreEqual("Google", driver.Title);

            // Search for a term
            googleHomePage.Search(testData.SearchTerm);

            googleHomePage.Search("Selenium WebDriver");

            //Verify that results are displayed
            ClassicAssert.IsTrue(googleResultsPage.ResultsDisplayed());

            // Get the title of the first result and click it
            string firstResultTitle = googleResultsPage.GetFirstResultTitle();
            googleResultsPage.ClickFirstResult();

            // Verify the title of the new page
            ClassicAssert.IsTrue(driver.Title.Contains(firstResultTitle));

            // Navigate back to the Google search results page
            driver.Navigate().Back();

            //Verify the search box still contains the search term
              ClassicAssert.AreEqual("Selenium WebDriver", driver.FindElement(By.Name("q")).GetAttribute("value"));
        }



        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
