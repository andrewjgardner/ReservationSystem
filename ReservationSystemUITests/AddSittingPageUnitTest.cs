using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;

namespace ReservationSystemUITests
{
    [TestClass]
    public class AddSittingPageUnitTest
    {
        private string _appUrl = "https://localhost:7156/Admin/Sitting/Create/";

        [TestMethod]
        public void ChromeTitleTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_appUrl);
            driver.FindElement(By.Id("email")).SendKeys("manager@manager.com");
            driver.FindElement(By.Id("password")).SendKeys("manager" + Keys.Enter);
            Assert.IsTrue(driver.Title.Contains("Add Sitting"));
            driver.Quit();
        }

        [TestMethod]
        public void ChromeElementsTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_appUrl);
            driver.FindElement(By.Id("email")).SendKeys("manager@manager.com");
            driver.FindElement(By.Id("password")).SendKeys("manager" + Keys.Enter);
            Assert.IsNotNull(driver.FindElement(By.TagName("h1")));
            Assert.IsNotNull(driver.FindElement(By.TagName("form")));
            Assert.IsNotNull(driver.FindElement(By.TagName("header")));
            Assert.IsNotNull(driver.FindElement(By.TagName("footer")));
        }

        [TestMethod]
        public void ChromeNavCountTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_appUrl);
            driver.FindElement(By.Id("email")).SendKeys("manager@manager.com");
            driver.FindElement(By.Id("password")).SendKeys("manager" + Keys.Enter);
            ReadOnlyCollection<IWebElement> navItems = driver.FindElements(By.ClassName("form-group"));
            Assert.IsTrue(navItems.Count >= 6, "At least 6 navigation items are expected");
            driver.Quit();
        }
        
        [TestMethod]
        public void ChromeTuteLink()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(_appUrl);
            driver.FindElement(By.Id("email")).SendKeys("manager@manager.com");
            driver.FindElement(By.Id("password")).SendKeys("manager" + Keys.Enter);
            driver.FindElement(By.Id("admin")).Click();
            Assert.AreEqual("https://localhost:7156/Admin", driver.Url);
            driver.Quit();
        }

    }
}
