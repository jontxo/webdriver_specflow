// <copyright file="ElementExtensions.Properties.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverExtensions
{
    using OpenQA.Selenium;
    using System;
    using System.Drawing;
    using System.Globalization;

    /// <summary>
    /// WebDriver Extensions.
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// The displayed.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool Displayed(this IWebElement webElement)
        {
            return webElement.Displayed;
        }

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool Exists(this IWebElement webElement)
        {
            try
            {
                return webElement != null;
            }
            catch (Exception)
            {
                // No logging is needed
                return false;
            }
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The background color of the element.</returns>
        public static string GetBackgroundColor(this IWebElement element)
        {
            var driver = element.GetWebDriver();
            const string script = "return window.getComputedStyle ? window.getComputedStyle(arguments[0], null).getPropertyValue('background-color') : arguments[0].style.backgroundColor;";
            var colorRGBA = element.GetJsScriptExecutor().ExecuteScript(script, element);
            return ConvertColorFromRGBAToHex((string)colorRGBA);
        }

        /// <summary>
        /// Gets the height of the line.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static int GetLineHeight(this IWebElement element)
        {
            var propertyFromComputedStyle = element.GetPropertyFromComputedStyle(ByProperty.LineHeight);
            var lineHeight = propertyFromComputedStyle != null ? propertyFromComputedStyle : element.GetPropertyFromStyle(ByProperty.StyleLineHeight);
            return Convert.ToInt32(lineHeight.Replace("px", string.Empty));
        }

        /// <summary>
        /// Gets the height of the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static int GetElementHeight(this IWebElement element)
        {
            var offsetHeight = Convert.ToInt32(element.GetElementProperties(ByProperty.OffsetHeight));
            var marginTopFromComputedStyle = element.GetPropertyFromComputedStyle(ByProperty.MarginTop).Replace("px", string.Empty);
            var marginTop = Convert.ToInt32((marginTopFromComputedStyle != null ? marginTopFromComputedStyle : element.GetPropertyFromStyle(ByProperty.StyleMarginTop)).Replace("px", string.Empty));
            var marginBottomFromComputedStyle = element.GetPropertyFromComputedStyle(ByProperty.MarginBottom).Replace("px", string.Empty);
            var marginBottom = Convert.ToInt32((marginBottomFromComputedStyle != null ? marginBottomFromComputedStyle : element.GetPropertyFromStyle(ByProperty.StyleMarginBottom)).Replace("px", string.Empty));
            return offsetHeight + marginTop + marginBottom;
        }

        /// <summary>
        /// Gets the element line numbers.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static int GetElementLineNumbers(this IWebElement element)
        {
            var lineHeight = element.GetLineHeight() != 0 ? element.GetLineHeight() : element.GetCalculatedElementLineHeight();
            var elementHeight = element.GetElementHeight();
            return elementHeight / lineHeight;
        }

        /// <summary>
        /// Gets the calculated height of the element line.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static int GetCalculatedElementLineHeight(this IWebElement element)
        {
            var driver = element.GetWebDriver();
            return Convert.ToInt32(element.GetJsScriptExecutor().ExecuteScript(
                "clone = arguments[0].cloneNode(); " +
                "clone.innerHTML = '<br>';" +
                "clone.style.visibility = 'hidden';" +
                "arguments[0].appendChild(clone); " +
                "singleLineHeight = clone.offsetHeight;" +
                "clone.innerHTML = '<br><br>';" +
                "doubleLineHeight = clone.offsetHeight; " +
                "arguments[0].removeChild(clone);" +
                "var lineHeightCalculated = doubleLineHeight - singleLineHeight; return lineHeightCalculated", element));
        }

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Get the class name of a webElement.</returns>
        public static dynamic GetClassName(this IWebElement element)
        {
            return element.GetElementProperties(ByProperty.ClassName);
        }

        /// <summary>
        /// Gets the inner HTML.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Get the innerHTML of a webElement.</returns>
        public static dynamic GetInnerHTML(this IWebElement element)
        {
            return element.GetElementProperties(ByProperty.InnerHTML);
        }

        /// <summary>
        /// Gets the outer HTML.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Get the outerHTML property of a webElement.</returns>
        public static dynamic GetOuterHTML(this IWebElement element)
        {
            return element.GetElementProperties(ByProperty.OuterHTML);
        }

        /// <summary>
        /// Determines whether this instance is overflowing.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><c>true</c> if the specified element is overflowing; otherwise, <c>false</c>.</returns>
        public static bool IsOverflowing(this IWebElement element)
        {
            var scrollWith = element.GetElementProperties(ByProperty.ScrollWidth);
            var clientWith = element.GetElementProperties(ByProperty.ClientWidth);

            return scrollWith > clientWith;
        }

        /// <summary>
        /// Gets the element properties.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="property">The property.</param>
        /// <returns>Get webElement properties.</returns>
        private static dynamic GetElementProperties(this IWebElement element, ByProperty property)
        {
            return element.GetJsScriptExecutor().ExecuteScript($"return arguments[0].{property.PropertyValue};", element);
        }

        /// <summary>
        /// Converts the color from RGBA to hexadecimal.
        /// </summary>
        /// <param name="rgbaColor">Color of the RGBA.</param>
        /// <returns>The RGBA color converted to a string.</returns>
        private static string ConvertColorFromRGBAToHex(string rgbaColor)
        {
            var colorvalue1 = rgbaColor.Split('(');
            var colorvalue2 = colorvalue1[1].Split(')');

            var colorvalue = colorvalue2[0];
            var colorCodeRGBValue = colorvalue.Split(',');
            var myColor = Color.FromArgb(
                Convert.ToInt32(colorCodeRGBValue[0], new CultureInfo("es-ES")),
                Convert.ToInt32(colorCodeRGBValue[1], new CultureInfo("es-ES")),
                Convert.ToInt32(colorCodeRGBValue[2], new CultureInfo("es-ES")));

            var hexValue = myColor.R.ToString("X2", new CultureInfo("es-ES", false)) +
                myColor.G.ToString("X2", new CultureInfo("es-ES", false)) +
                myColor.B.ToString("X2", new CultureInfo("es-ES", false));
            return hexValue;
        }

        private static string GetPropertyFromComputedStyle(this IWebElement element, ByProperty property)
        {
            var script = $"return window.getComputedStyle ? window.getComputedStyle(arguments[0], null).getPropertyValue('" + property.PropertyValue + "') : " +
                "null;";
            var result = element.GetJsScriptExecutor().ExecuteScript(script, element).ToString();
            return result;
        }

        private static string GetPropertyFromStyle(this IWebElement element, ByProperty property)
        {
            var driver = element.GetWebDriver();
            var script = $"return arguments[0].style." + property.PropertyValue + "; ";
            var result = element.GetJsScriptExecutor().ExecuteScript(script, element).ToString();
            return result;
        }
    }
}