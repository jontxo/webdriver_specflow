// <copyright file="HomePage.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.Pages
{
    using Automation.WebDriverExtensions;
    using Automation.WebDriverHelper;
    using OpenQA.Selenium;
    using SeleniumExtras.PageObjects;

    /// <summary>
    /// The login page.
    /// </summary>
    public class HomePage
    {
        #region WebElements
#pragma warning disable CA1823,0169,0649

        /// <summary>
        /// The password.
        /// </summary>
        [FindsBy(How = How.XPath, Using = "//form[@id='load_form'][@class='ajaxlogin']//input[@name='password']")]
        private readonly IWebElement password;

        /// <summary>
        /// The user name.
        /// </summary>
        [FindsBy(How = How.XPath, Using = "//form[@id='load_form'][@class='ajaxlogin']//input[@name='username']")]
        private readonly IWebElement userName;

        /// <summary>
        /// The SIGNIN.
        /// </summary>
        [FindsBy(How = How.LinkText, Using = "SIGN-ON")]
        private readonly IWebElement singnOn;

        /// <summary>
        /// The register.
        /// </summary>
        [FindsBy(How = How.LinkText, Using = "REGISTER")]
        private readonly IWebElement register;

        [FindsBy(How = How.XPath, Using = ".//*[contains(text(),'Registered')]/..")]
        private readonly IWebElement textField;

#pragma warning restore CA1823,0169,0649
        #endregion WebElements

        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        /// <param name="webDriverContext">The web driver context.</param>
        public HomePage(WebDriverContext webDriverContext)
        {
            PageFactory.InitElements(webDriverContext.WebDriver, this);
        }

        /// <summary>
        /// The enter user name.
        /// </summary>
        /// <param name="userNameValue">The user name.</param>
        public void EnterUserName(string userNameValue)
        {
            this.userName.SendKeys(userNameValue);
        }

        /// <summary>
        /// The enter password.
        /// </summary>
        /// <param name="passwordValue">The password value.</param>
        public void EnterPassword(string passwordValue)
        {
            this.password.SendKeys(passwordValue);
        }

        /// <summary>
        /// The click on register button.
        /// </summary>
        public void ClickOnRegisterButton()
        {
            this.register.Hover();
            this.register.Click();
        }

        /// <summary>
        /// Checks the user correctly logged.
        /// </summary>
        public void CheckUserCorrectlyLogged()
        {
            this.userName.WaitUntilCheckIsEnabled(false, 10);
        }

        /// <summary>
        /// Gets the element line numbers.
        /// </summary>
        /// <returns>The number of lines of the webelement.</returns>
        public int GetElementLineNumbers()
        {
            return this.textField.GetElementLineNumbers();
        }
    }
}