namespace NewGoogleTest
{
    using System;
    using System.Threading;
    using TechTalk.SpecFlow;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;
    using NUnit.Framework;

    [Binding]
    public class GmailAccountSteps
    {
        IWebDriver driver;
        public static string BaseUrl = "http://www.gmail.com";
        string myCustomUserName = GenerateUsername();

        private static string GenerateUsername()
        {
            return "Test.User501" + GetRandomNumber();
        }

        private static int GetRandomNumber()
        {
            return new Random().Next(100000, 100000000);
        }

        [Given(@"I am on the Gmail homepage")]
        public void GivenIAmOnTheGmailHomepage()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(BaseUrl);
        }

        [Given(@"I have selected to create an account")]
        public void GivenIHaveSelectedToCreateAnAccount()
        {
            driver.FindElement(By.CssSelector("div.bdf4dc > div:nth-child(2) div:nth-child(2)  div.fImV7 > div:nth-child(2)")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div.JPdR6b > div > div > content")).Click();
        }

        [Given(@"I have logged into my account")]
        public void GivenIHaveLoggedIntoMyAccount()
        {
            driver.FindElement(By.CssSelector("div.Xb9hP")).SendKeys("test.user50138318395@gmail.com");
            driver.FindElement(By.CssSelector(".CwaK9")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.CssSelector("div.Xb9hP")).SendKeys("password999");
            driver.FindElement(By.CssSelector(".CwaK9")).Click();
            Thread.Sleep(1000);

            //Assert
            IWebElement body = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(body.Text.Contains("Test"));
        }

        [When(@"I enter my personal details")]
        public void WhenIEnterMyPersonalDetails()
        {
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("form > div > fieldset > label > input")).SendKeys("Test");
            driver.FindElement(By.CssSelector("form > div > fieldset > label.lastname > input")).SendKeys("User");
            driver.FindElement(By.CssSelector("form > div:nth-of-type(2) > label > input")).SendKeys(this.myCustomUserName);
            driver.FindElement(By.CssSelector("form > div:nth-of-type(3) > label > input")).SendKeys("password999");
            driver.FindElement(By.CssSelector("form > div:nth-of-type(4) > label > input")).SendKeys("password999");

            //DOB
            driver.FindElement(By.CssSelector("form > div:nth-of-type(5) > fieldset > label > span > div")).Click();
            driver.FindElement(By.CssSelector("form > div:nth-of-type(5) > fieldset > label > span > div > div > div")).Click();
            driver.FindElement(By.CssSelector("form > div:nth-of-type(5) > fieldset > label.day > input")).SendKeys("01");
            driver.FindElement(By.CssSelector("form > div:nth-of-type(5) > fieldset > label.year > input")).SendKeys("1988");

            //Gender
            driver.FindElement(By.CssSelector("form > div:nth-of-type(6) > label > div")).Click();
            driver.FindElement(By.CssSelector("form > div:nth-of-type(6) > label > div > div > div > div")).Click();

            //Mobile Number
            driver.FindElement(By.CssSelector("form > div:nth-of-type(7) > table > tbody > tr > td > input")).SendKeys("7531084350");

            //Email
            driver.FindElement(By.CssSelector("form > div:nth-of-type(8) > label > input")).SendKeys("signuptest@test.com");

            driver.FindElement(By.CssSelector("form > div:nth-of-type(12) > input")).Click();

            IWebElement icon = driver.FindElement(By.CssSelector("div.tos-scroll-fab-container > div"));
            icon.Click();
            icon.Click();
            Thread.Sleep(100);
            //Accept T&C's
            driver.FindElement(By.CssSelector("div.tos-popup-container > div.tos-button-div > div > input:nth-of-type(2)")).Click();

            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("form > div:nth-of-type(2) > input")).Click();

            Thread.Sleep(20000);
            driver.FindElement(By.CssSelector("form > div > div:nth-child(2) > input:nth-child(2)")).Click();

            //Manual intervention is needed here as the user has to manually input the code received on there device.
            //Potential work around would be to test this scenario in a stubbed environment in order to bypass the mobile verification.

            Thread.Sleep(3500);
            driver.FindElement(By.CssSelector("form > input:nth-child(2)")).Click();
        }

        [When(@"I enter an invalid email address")]
        public void WhenIEnterAnInvalidEmailAddress()
        {
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("form > div:nth-of-type(8) > label > input")).SendKeys("email@domain");
            driver.FindElement(By.CssSelector("form div:nth-of-type(12)")).Click();
        }

        [When(@"I select Log Out")]
        public void WhenISelectLogOut()
        {
            driver.FindElement(By.CssSelector("div.nH div > div > div:nth-child(4) > div > div > div > div:nth-child(2) > div:nth-child(4) > div > a")).Click();
            driver.FindElement(By.CssSelector("div.nH div > div > div:nth-child(4) > div > div > div > div:nth-child(2) > div:nth-child(4) > div:nth-child(2) > div.gb_yb > div:nth-child(2) > a")).Click();
        }

        [Then(@"I will have sucessfully created my new account")]
        public void ThenIWillHaveSucessfullyCreatedMyNewAccount()
        {
            Thread.Sleep(4000);
            IWebElement body = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(body.Text.Contains("Test User"));
        }

        [Then(@"I will be presented with an email validation error")]
        public void ThenIWillBePresentedWithAnEmailValidationError()
        {
            IWebElement emailValidation = driver.FindElement(By.CssSelector("form > div:nth-of-type(8) > span"));
            Assert.IsTrue(emailValidation.Displayed);
            Assert.IsTrue(emailValidation.Text.Equals("Your email address contains the invalid domain name 'domain'."));
        }

        [Then(@"I will have sucessfully logged out of my account")]
        public void ThenIWillHaveSucessfullyLoggedOutOfMyAccount()
        {
            IWebElement body = driver.FindElement(By.TagName("body"));
            Assert.IsFalse(body.Text.Contains("Test"));
        }

        [AfterScenario()]
        public void AfterScenario()
        {
            driver.Close();
        }
    }
}

