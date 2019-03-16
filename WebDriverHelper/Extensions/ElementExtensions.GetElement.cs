// <copyright file="ElementExtensions.GetElement.cs" company="demian INC.">
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
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// The webelement extensions.
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Gets the child element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="by">The by.</param>
        /// <returns>The child element obtained using the by condition.</returns>
        public static IWebElement GetChildElement(this IWebElement element, By by)
        {
            return element.FindElement(by);
        }

        /// <summary>
        /// Gets the child element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Get child element.</returns>
        public static IWebElement GetChildElement(this IWebElement element)
        {
            return element.FindElement(By.XPath(".//*"));
        }

        /// <summary>
        /// Gets the parent element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Get the parent element of a webElement.</returns>
        public static IWebElement GetParentElement(this IWebElement element)
        {
            return element.FindElement(By.XPath(".."));
        }
    }
}
