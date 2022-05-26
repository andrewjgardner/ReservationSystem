using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.Threading;

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
            try
            {
                driver.Navigate().GoToUrl(_appUrl);
                Thread.Sleep(500);
                driver.FindElement(By.Id("email")).SendKeys("manager@manager.com");
                driver.FindElement(By.Id("password")).SendKeys("manager" + Keys.Enter);
                Thread.Sleep(500);
                Assert.IsTrue(driver.Title.Contains("Add Sitting"));
            }
            finally
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void ChromeElementsTest()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(_appUrl);
                Thread.Sleep(500);
                driver.FindElement(By.Id("email")).SendKeys("manager@manager.com");
                driver.FindElement(By.Id("password")).SendKeys("manager" + Keys.Enter);
                Thread.Sleep(500);
                Assert.IsNotNull(driver.FindElement(By.TagName("h1")));
                Assert.IsNotNull(driver.FindElement(By.TagName("form")));
                Assert.IsNotNull(driver.FindElement(By.TagName("header")));
                Assert.IsNotNull(driver.FindElement(By.TagName("footer")));
            }
            finally
            {
                driver.Quit();
            }

        }

        [TestMethod]
        public void ChromeNavCountTest()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(_appUrl);
                Thread.Sleep(500);
                driver.FindElement(By.Id("email")).SendKeys("manager@manager.com");
                driver.FindElement(By.Id("password")).SendKeys("manager" + Keys.Enter);
                Thread.Sleep(500);
                ReadOnlyCollection<IWebElement> navItems = driver.FindElements(By.ClassName("form-group"));
                Assert.IsTrue(navItems.Count >= 6, "At least 6 navigation items are expected");
            }
            finally
            {
                driver.Quit();
            }


        }

        [TestMethod]
        public void ChromeTuteLink()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(_appUrl);
                Thread.Sleep(500);
                driver.FindElement(By.Id("email")).SendKeys("manager@manager.com");
                driver.FindElement(By.Id("password")).SendKeys("manager" + Keys.Enter);
                Thread.Sleep(500);
                driver.FindElement(By.Id("admin")).Click();
                Assert.AreEqual("https://localhost:7156/Admin", driver.Url);
            }
            finally
            {
                driver.Quit();
            }
        }

        [TestMethod]
        public void ChromeAddSittingFormValidation()
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl(_appUrl);
                Thread.Sleep(500);
                driver.FindElement(By.Id("email")).SendKeys("manager@manager.com");
                driver.FindElement(By.Id("password")).SendKeys("manager" + Keys.Enter);
                Thread.Sleep(500);
                driver.FindElement(By.Id("submit")).Click();
                bool formVal = driver.FindElement(By.TagName("span")).Displayed;
                Assert.IsTrue(formVal);
            }
            finally
            {
                driver.Quit();
            }
        }

    }
}
