// <copyright file="WebDriverExtensions.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverExtensions
{
    using System;
    using System.Threading;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// The web driver extensions.
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// The get screenshot.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns>The <see cref="Screenshot"/>.</returns>
        public static Screenshot GetScreenshot(this IWebDriver driver)
        {
            return ((ITakesScreenshot)driver).GetScreenshot();
        }

        /// <summary>
        /// The is alert present.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsAlertPresent(this IWebDriver driver)
        {
            try
            {
                var alert = driver.SwitchTo().Alert();
                if (alert != null)
                {
                    driver.SwitchTo().DefaultContent();
                    return true;
                }

                driver.SwitchTo().DefaultContent();
                return false;
            }
            catch (Exception)
            {
                // No logging is needed
                driver.SwitchTo().DefaultContent();
                return false;
            }
        }

        /// <summary>
        /// The scroll down page.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public static void ScrollDownPage(this IWebDriver driver)
        {
            GetJavaScriptExecutor(driver).ExecuteScript("var body = document.body, html  = document.documentElement; var height = Math.max(body.scrollHeight,body.offsetHeight, html.clientHeight, html.scrollHeight, html.offsetHeight); window.scrollBy(0, height)");
        }

        /// <summary>
        /// The wait for ajax.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public static void WaitForAjax(this IWebDriver driver)
        {
            while (true)
            {
                bool ajaxIsComplete;

                var pageHasJQuery =
                    (bool)GetJavaScriptExecutor(driver).ExecuteScript("if (!window.jQuery) { return false; } else { return true; }");
                if (pageHasJQuery)
                {
                    ajaxIsComplete =
                        (bool)GetJavaScriptExecutor(driver).ExecuteScript(
                                "if (!window.jQuery) { return false; } else { return jQuery.active == 0; }");
                    if (ajaxIsComplete)
                    {
                        break;
                    }
                }
                else
                {
                    ajaxIsComplete = (bool)GetJavaScriptExecutor(driver).ExecuteScript("return document.readyState == 'complete'");
                    if (ajaxIsComplete)
                    {
                        break;
                    }
                }

                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// The wait for page to load.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="timeSpan">The time span.</param>
        public static void WaitForPageToLoad(this IWebDriver driver, TimeSpan? timeSpan = null)
        {
            if (timeSpan == null)
            {
                timeSpan = TimeSpan.FromSeconds(30);
            }

            var wait = new WebDriverWait(driver, (TimeSpan)timeSpan);

            wait.Until(d =>
            {
                try
                {
                    var readyState = GetJavaScriptExecutor(driver).ExecuteScript(
                        "if (document.readyState) return document.readyState;").ToString();
                    return readyState.Contains("complete");
                }
                catch (InvalidOperationException e)
                {
                    // Window is no longer available
                    return e.Message.Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    // Browser is no longer available
                    return e.Message.IndexOf("unable to connect", StringComparison.CurrentCultureIgnoreCase) >= 0;
                }
                catch (Exception)
                {
                    // No logging is needed
                return false;
                }
            });
        }

        /// <summary>
        /// The wait until document is ready.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="timeout">The time span.</param>
        public static void WaitUntilDocumentIsReady(this IWebDriver driver, TimeSpan timeout)
        {
            var javaScriptExecutor = driver as IJavaScriptExecutor;
            var wait = new WebDriverWait(driver, timeout);

            // Check if document is ready
            bool ReadyCondition(IWebDriver webDriver)
            {
                var executeScript = javaScriptExecutor?.ExecuteScript(
                    "return (document.readyState == 'complete' && jQuery.active == 0)");
                return
                    executeScript != null && (bool)executeScript;
            }

            wait.Until(ReadyCondition);
        }

        /// <summary>
        /// Switches to frame.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="frameName">WebElement of the frame.</param>
        public static void SwitchToFrame(this IWebDriver driver, IWebElement frameName)
        {
            driver.SwitchTo().Frame(frameName);
        }

        /// <summary>
        /// Switches to main frame.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public static void SwitchToMainFrame(this IWebDriver driver)
        {
            driver.SwitchTo().ActiveElement();
        }
    }
}