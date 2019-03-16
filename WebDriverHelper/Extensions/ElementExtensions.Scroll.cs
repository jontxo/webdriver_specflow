// <copyright file="ElementExtensions.Scroll.cs" company="demian INC.">
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

    /// <summary>
    /// WebDriver Extensions.
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// The ScrollToElement.
        /// </summary>
        /// <param name="element">The element <see cref="IWebElement"/>.</param>
        public static void ScrollToElement(this IWebElement element)
        {
            GetJsScriptExecutor(element).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}
