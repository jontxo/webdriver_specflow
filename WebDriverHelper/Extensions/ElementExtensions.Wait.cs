//-------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.Wait.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Automation.WebDriverExtensions
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// WebElement extensions.
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Defines the KeysExclude.
        /// </summary>
        private static readonly string[] KeysExclude = { string.Empty, Keys.Escape };

        /// <summary>
        /// Waits the until.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="waitSeconds">The wait seconds.</param>
        /// <returns>a webElement if a condition is met in less than the waitSeconds time.</returns>
        public static IWebElement WaitUntil(this IWebElement element, Func<IWebElement, bool> condition, double waitSeconds = 2)
        {
            var wait = element.GetWaitUntil(waitSeconds);
            wait.Until(condition);

            return element;
        }

        /// <summary>
        /// Waits the until find element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="by">The by.</param>
        /// <param name="waitSeconds">The wait seconds.</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        public static IWebElement WaitUntilFindElement(this IWebElement element, By by, double waitSeconds = 2)
        {
            var wait = element.GetWaitUntil(waitSeconds);
            var findElement = wait.Until(x => x.FindElement(by));

            return findElement;
        }

        /// <summary>
        /// Waits the until is enabled.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
        /// <param name="waitSeconds">The wait seconds.</param>
        /// <returns>a webElement if it is enabled in less than the waitTime.</returns>
        public static IWebElement WaitUntilIsEnabled(this IWebElement element, bool isEnabled = true, double waitSeconds = 2)
        {
            var wait = element.GetWaitUntil(waitSeconds);
            wait.Until(p => p.Enabled == isEnabled);

            return element;
        }

        /// <summary>
        /// Waits the until check is enabled.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
        /// <param name="waitSeconds">The wait seconds.</param>
        /// <returns>True if the element is found.</returns>
        public static bool WaitUntilCheckIsEnabled(this IWebElement element, bool isEnabled = true, double waitSeconds = 1)
        {
            var wait = element.GetWaitUntil(waitSeconds);
            wait.IgnoreExceptionTypes(typeof(NotFoundException), typeof(NoSuchElementException), typeof(TimeoutException), typeof(WebDriverTimeoutException));

            try
            {
                return wait.Until(p => p != null && p.Enabled == isEnabled);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                if (exception is WebDriverTimeoutException || exception is NoSuchElementException)
                {
                    return false;
                }

                throw;
            }
        }

        /// <summary>
        /// The WaitUntilCheckExists.
        /// </summary>
        /// <param name="element">The element <see cref="IWebElement"/>.</param>
        /// <param name="isDisplayed">The isDisplayed <see cref="bool"/>.</param>
        /// <param name="waitSeconds">The waitSeconds <see cref="double"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool WaitUntilCheckExists(this IWebElement element, bool isDisplayed = true, double waitSeconds = 1)
        {
            var wait = element.GetWaitUntil(waitSeconds);
            wait.IgnoreExceptionTypes(typeof(NotFoundException), typeof(NoSuchElementException), typeof(TimeoutException), typeof(WebDriverTimeoutException));

            try
            {
                // Displayed is not working on iOS and Android We use enabled instead of displayed
                // until new verions return wait.Until(p => p != null && p.Displayed == isDisplayed);
                return wait.Until(p => isDisplayed ? p != null && p.Enabled == isDisplayed : p == null && p.Enabled == isDisplayed);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                if (exception is WebDriverTimeoutException || exception is NoSuchElementException)
                {
                    return false;
                }

                throw;
            }
        }

        /// <summary>
        /// The WaitUntilFindElements.
        /// </summary>
        /// <param name="element">The element <see cref="IWebElement"/>.</param>
        /// <param name="by">The by <see cref="By"/>.</param>
        /// <param name="waitSeconds">The waitSeconds <see cref="double"/>.</param>
        /// <returns>The <see cref="ReadOnlyCollection{IWebElement}"/>.</returns>
        public static ReadOnlyCollection<IWebElement> WaitUntilFindElements(this IWebElement element, By by, double waitSeconds = 2)
        {
            var wait = element.GetWaitUntil(waitSeconds);
            var findElements = wait.Until(x => x.FindElements(by));

            return findElements;
        }

        /// <summary>
        /// The ClickWait.
        /// </summary>
        /// <param name="element">The element <see cref="IWebElement"/>.</param>
        public static void ClickWait(this IWebElement element)
        {
            element.WaitUntilIsEnabled();

            element.Click();

            // This is needed because some elements has the debounce time set at 300
            WaitThread(300);
        }

        /// <summary>
        /// The GetTextWait.
        /// </summary>
        /// <param name="element">The element <see cref="IWebElement"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetTextWait(this IWebElement element)
        {
            element.WaitUntilIsEnabled();

            return element.Text;
        }

        /// <summary>
        /// The SendKeysWait.
        /// </summary>
        /// <param name="element">The element <see cref="IWebElement"/>.</param>
        /// <param name="keys">The keys <see cref="string"/>.</param>
        public static void SendKeysWait(this IWebElement element, string keys)
        {
            element.WaitUntilIsEnabled();

            if (KeysExclude.All(p => p != keys))
            {
                element.Click();
            }

            var newKeys = keys.Replace("/", Keys.Divide);
            element.SendKeys(newKeys);

            // This is needed because some elements has the debounce time set at 300
            WaitThread(400);
        }

        /// <summary>
        /// The ClearWait.
        /// </summary>
        /// <param name="element">The element <see cref="IWebElement"/>.</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        public static IWebElement ClearWait(this IWebElement element)
        {
            element.WaitUntilIsEnabled();
            element.Clear();
            return element;
        }

        /// <summary>
        /// The WaitThread.
        /// </summary>
        /// <param name="milliseconds">The milliseconds <see cref="int"/>.</param>
        public static void WaitThread(int milliseconds = 250)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(milliseconds));
        }

        /// <summary>
        /// The wait for element visible.
        /// </summary>
        /// <param name="driver">The driver <see cref="IWebDriver"/>.</param>
        /// <param name="element">The element <see cref="IWebElement"/>.</param>
        /// <param name="timespan">The timespan <see cref="TimeSpan"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool WaitForElementVisible(this IWebDriver driver, IWebElement element, TimeSpan timespan)
        {
            try
            {
                var wait = new WebDriverWait(driver, timespan);
                wait.Until(d => element.Displayed);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Waits the until.
        /// </summary>
        /// <param name="webDriver">The web driver.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="waitSeconds">The wait seconds.</param>
        /// <returns>The wait element.</returns>
        public static bool WaitUntil(this IWebDriver webDriver, Func<IWebDriver, bool> condition, double waitSeconds = 3)
        {
            var webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(waitSeconds));
            return webDriverWait.Until(condition);
        }

        /// <summary>
        /// The WaitUntilFindElement.
        /// </summary>
        /// <param name="webDriver">The webDriver <see cref="IWebDriver"/>.</param>
        /// <param name="by">The by <see cref="By"/>.</param>
        /// <param name="waitSeconds">The waitSeconds <see cref="double"/>.</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        public static IWebElement WaitUntilFindElement(this IWebDriver webDriver, By by, double waitSeconds = 3)
        {
            var webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(waitSeconds));
            var findElement = webDriverWait.Until(x => x.FindElement(by));

            return findElement;
        }

        /// <summary>
        /// The WaitUntilFindElements.
        /// </summary>
        /// <param name="webDriver">The webDriver <see cref="IWebDriver"/>.</param>
        /// <param name="by">The by <see cref="By"/>.</param>
        /// <param name="waitSeconds">The waitSeconds <see cref="double"/>.</param>
        /// <returns>The <see cref="ReadOnlyCollection{IWebElement}"/>.</returns>
        public static ReadOnlyCollection<IWebElement> WaitUntilFindElements(this IWebDriver webDriver, By by, double waitSeconds = 3)
        {
            var webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(waitSeconds));
            var findElements = webDriverWait.Until(x => x.FindElements(by));

            return findElements;
        }

        /// <summary>
        /// The GetWaitUntil.
        /// </summary>
        /// <param name="element">The element <see cref="IWebElement"/>.</param>
        /// <param name="waitSeconds">The waitSeconds <see cref="double"/>.</param>
        /// <returns>The <see cref="IWait{IWebElement}"/>.</returns>
        private static IWait<IWebElement> GetWaitUntil(this IWebElement element, double waitSeconds)
        {
            return new DefaultWait<IWebElement>(element)
            {
                Timeout = TimeSpan.FromSeconds(waitSeconds),
                PollingInterval = TimeSpan.FromMilliseconds(500),
            };
        }
    }
}