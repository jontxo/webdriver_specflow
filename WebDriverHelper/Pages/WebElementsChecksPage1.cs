// <copyright file="WebElementsChecksPage1.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.Pages
{
    using System;
    using System.Globalization;
    using Automation.WebDriverExtensions;
    using Automation.WebDriverHelper;
    using BoDi;
    using DataFactory.Configuration;
    using OpenQA.Selenium;
    using SeleniumExtras.PageObjects;

    /// <summary>
    /// The WebElement Checks page 1.
    /// </summary>
    public class WebElementsChecksPage1
    {
        #region webelements
#pragma warning disable 0169, 0649

        /// <summary>
        /// The blue foo button.
        /// </summary>
        [FindsBy(How = How.XPath, Using = ".//*[@id='form_submit']")]
        private readonly IWebElement blueFooButton;

#pragma warning restore 0169, 0649
        #endregion webelements

        /// <summary>
        /// Initializes a new instance of the <see cref="WebElementsChecksPage1"/> class.
        /// </summary>
        /// <param name="webDriverContext">The web driver context.</param>
        public WebElementsChecksPage1(WebDriverContext webDriverContext)
        {
            PageFactory.InitElements(webDriverContext.WebDriver, this);
        }

        /// <summary>
        /// Gets the color of the blue button background.
        /// </summary>
        /// <returns>The background color of the blue element.</returns>
        public string GetBlueButtonBackgroundColor()
        {
            var color = this.blueFooButton.GetBackgroundColor();
            return GetColor(color);
        }

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The color converted.</returns>
        /// <exception cref="Exception">Unexpected Case.</exception>
        private static string GetColor(string color)
        {
            switch (color.ToUpper(new CultureInfo("es-ES", false)))
            {
                case Constants.RedHexValue:
                    {
                        return "RED";
                    }

                default:
                    throw new Exception("Unexpected Case");
            }
        }
    }
}