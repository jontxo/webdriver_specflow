// <copyright file="ElementExtensions.Common.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Internal;

    /// <summary>
    /// WebDriver Extensions.
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Unwraps the webElement.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The unwrapped webElement.</returns>
        private static IWebElement UnWrapElement(this IWebElement element)
        {
            return element is IWrapsElement ? ((IWrapsElement)element).WrappedElement : element;
        }

        /// <summary>
        /// Gets the web driver.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Get the webDriver of a webElement.</returns>
        private static IWebDriver GetWebDriver(this IWebElement element)
        {
            var realElement = element.UnWrapElement();
            return ((IWrapsDriver)realElement).WrappedDriver;
        }

        /// <summary>
        /// Gets the java script executor.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <returns>The Javascript Executor.</returns>
        private static IJavaScriptExecutor GetJavaScriptExecutor(this IWebDriver webDriver)
        {
            return webDriver as IJavaScriptExecutor;
        }

        /// <summary>
        /// Gets the script executor.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The javascript executor.</returns>
        private static IJavaScriptExecutor GetJsScriptExecutor(this IWebElement element)
        {
            return element.GetWebDriver().GetJavaScriptExecutor();
        }
    }
}
