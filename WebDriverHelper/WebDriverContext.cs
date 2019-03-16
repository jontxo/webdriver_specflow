// <copyright file="WebDriverContext.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverHelper
{
    using System;
    using System.Globalization;
    using System.IO;
    using DataFactory.Configuration;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Appium.Service;
    using Protractor;

    /// <summary>
    /// The front end basic context.
    /// </summary>
    public class WebDriverContext : IDisposable
    {
        /// <summary>
        /// The configuration parameters.
        /// </summary>
        private readonly ConfigurationParameters configurationParameters;

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDriverContext"/> class.
        /// </summary>
        /// <param name="configurationParameters"> The configuration parameters object.</param>
        public WebDriverContext(ConfigurationParameters configurationParameters)
        {
            this.configurationParameters = configurationParameters;
            this.CreateWebDriver();
        }

        /// <summary>
        /// Gets or sets the internal driver.
        /// </summary>
        /// <value>
        /// The internal driver.
        /// </value>
        public IWebDriver WebDriver { get; set; }

        /// <summary>
        /// Gets or sets the Driver.
        /// </summary>
        public NgWebDriver NgWebDriver { get; set; }

        /// <summary>
        /// Gets or sets the appium local service.
        /// </summary>
        public AppiumLocalService AppiumLocalService { get; set; }

        /// <summary>
        /// Determines whether [is web driver null].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is web driver null]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsWebDriverNull()
        {
            return this.WebDriver != null;
        }

        /// <summary>
        /// Creates the web driver.
        /// </summary>
        /// <exception cref="Exception">Unknown browser.</exception>
        public void CreateWebDriver()
        {
            switch (this.configurationParameters.BrowsersConfiguration.Browser)
            {
                case Browser.IE11:
                    {
                        this.WebDriver = IeWebdriver.CreateNewWebDriver(this.configurationParameters);
                        break;
                    }

                case Browser.ChromeDesktop:
                    {
                        this.WebDriver = ChromeWebDriver.CreateNewWebDriver(this.configurationParameters);
                        break;
                    }

                case Browser.Firefox:
                    {
                        this.WebDriver = FirefoxWebDriver.CreateNewWebDriver(this.configurationParameters);
                        break;
                    }

                case Browser.ChromeAndroid9:
                    {
                        this.WebDriver = AndroidWebDriver.CreateNewWebDriver(this.configurationParameters);
                        break;
                    }

                default:
                    {
                        throw new Exception("Unknown browser");
                    }
            }

            this.WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(this.configurationParameters.BrowsersConfiguration.PageLoadTimeout);
            this.WebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(this.configurationParameters.BrowsersConfiguration.AsynchronousJavascriptTimeout);
            this.WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(this.configurationParameters.BrowsersConfiguration.ImplicitWaitTimeout);
            this.NgWebDriver = new NgWebDriver(this.WebDriver);
        }

        /// <summary>
        /// Close and dispose the browser driver if it exists.
        /// </summary>
        public void CloseBrowser()
        {
            if (this.NgWebDriver != null)
            {
                this.NgWebDriver.Quit();
                this.NgWebDriver.Dispose();
            }

            if (this.WebDriver != null)
            {
                this.WebDriver.Quit();
                this.WebDriver.Dispose();
            }
        }

        /// <summary>
        /// The make web screenshot.
        /// </summary>
        /// <param name="scenario">The scenario.</param>
        /// <param name="contextPath">The context Path.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string MakeWebScreenshot(string scenario, string contextPath)
        {
            var screenshot = ((ITakesScreenshot)this.NgWebDriver).GetScreenshot();
            var screenshotName = $"{scenario}.jpeg";

            var fullPathFile = contextPath + @"\" + screenshotName;

            if (fullPathFile.Length > 259)
            {
                fullPathFile = fullPathFile.Substring(0, fullPathFile.Length - (fullPathFile.Length - 260 + 6)) + ".jpeg";
            }

            screenshot.SaveAsFile(fullPathFile, ScreenshotImageFormat.Jpeg);

            return fullPathFile;
        }

        /// <summary>
        /// Goes to URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void GoToUrl(Uri url)
        {
            this.NgWebDriver.Navigate().GoToUrl(url, false);
        }

        /// <summary>
        /// Takes the screenshot.
        /// </summary>
        public void TakeScreenshot()
        {
            // Create an empty temp folder to store the screenshots
            var tempFolder = Path.Combine(
                                 Path.GetTempPath(),
                                 "SpecflowExecutionResults" + DateTime.Now.ToString("yyyy_MM_dd", new CultureInfo("es-ES", false)));

            Directory.CreateDirectory(tempFolder);

            var ss = ((ITakesScreenshot)this.NgWebDriver.WrappedDriver).GetScreenshot();

            var screenshotFileName = "screenshot" + DateTime.Now.ToString("yyyyMMddHHmmss", new CultureInfo("es-ES", false)) +
                "_" + Guid.NewGuid() + ".jpeg";

            var screenshotFileNameAndPath = Path.Combine(tempFolder, screenshotFileName);

            ss.SaveAsFile(screenshotFileNameAndPath, OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
            Console.WriteLine("file:///" + screenshotFileNameAndPath);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose function.
        /// </summary>
        /// <param name="disposing">The disposing variable.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.NgWebDriver?.Quit();
                    this.WebDriver?.Quit();
                }
            }

            // dispose unmanaged resources
            this.disposed = true;
        }
    }
}