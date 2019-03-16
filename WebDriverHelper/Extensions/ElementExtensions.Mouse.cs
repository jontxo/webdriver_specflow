// <copyright file="ElementExtensions.Mouse.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverExtensions
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// The webelement extensions.
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Hovers the specified web element.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public static void Hover(this IWebElement webElement)
        {
            var webdriver = webElement.GetWebDriver();
            var action = new Actions(webdriver);
            action.MoveToElement(webElement).Build().Perform();
        }

        /// <summary>
        /// Hovers the and click.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public static void HoverAndClick(this IWebElement webElement)
        {
            var webdriver = webElement.GetWebDriver();
            var action = new Actions(webdriver);
            action.MoveToElement(webElement).Perform();
            action.Click(webElement);
            action.Perform();
        }

        /// <summary>
        /// Jses the hover and click.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public static void JSHoverAndClick(this IWebElement webElement)
        {
            webElement.GetWebDriver().GetJavaScriptExecutor().ExecuteScript("arguments[0].focus();", webElement);
        }

        /// <summary>
        /// Javascript the click.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public static void JSClick(this IWebElement webElement)
        {
            var webdriver = webElement.GetWebDriver();
            var javascriptExecutor = (IJavaScriptExecutor)webdriver;

            javascriptExecutor.ExecuteScript("arguments[0].click();", webElement);
        }

        /// <summary>
        /// The select from drop down by index.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <param name="index">The index.</param>
        public static void SelectFromDropDownByIndex(this IWebElement webElement, int index)
        {
            if (webElement.GetAttribute("multiple") != null)
            {
                var optionElements = webElement.FindElements(By.TagName("option"));
                if (optionElements.Count > 0)
                {
                    optionElements[index].Click();
                }
                else
                {
                    var selectElement = new SelectElement(webElement);
                    selectElement.SelectByIndex(index);
                }
            }
            else
            {
                var selectElement = new SelectElement(webElement);
                selectElement.SelectByIndex(index);
            }
        }

        /// <summary>
        /// The select from drop down by text.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <param name="value">The value.</param>
        public static void SelectFromDropDownByText(this IWebElement webElement, string value)
        {
            var selectElement = new SelectElement(webElement);
            selectElement.SelectByText(value);
        }

        /// <summary>
        /// The select from drop down by value.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <param name="value">The value.</param>
        public static void SelectFromDropDownByValue(this IWebElement webElement, string value)
        {
            var selectElement = new SelectElement(webElement);
            selectElement.SelectByValue(value);
        }

        /// <summary>
        /// The click and hold.
        /// </summary>
        /// <param name="webElement">The webElement <see cref="IWebElement"/>.</param>
        public static void ClickAndHold(this IWebElement webElement)
        {
            var driver = GetWebDriver(webElement);
            var action = new Actions(driver);
            action.ClickAndHold(webElement).Build().Perform();
        }

        /// <summary>
        /// The double click.
        /// </summary>
        /// <param name="webElement">The webElement <see cref="IWebElement"/>.</param>
        public static void DoubleClick(this IWebElement webElement)
        {
            var driver = GetWebDriver(webElement);
            var action = new Actions(driver);
            action.DoubleClick(webElement).Build().Perform();
        }

        /// <summary>
        /// The right click.
        /// </summary>
        /// <param name="webElement">The webElement <see cref="IWebElement"/>.</param>
        public static void RightClick(this IWebElement webElement)
        {
            var driver = GetWebDriver(webElement);
            var action = new Actions(driver);
            action.ContextClick(webElement).Build().Perform();
        }
    }
}
