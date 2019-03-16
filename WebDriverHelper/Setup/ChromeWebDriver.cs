// <copyright file="ChromeWebDriver.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverHelper
{
    using DataFactory.Configuration;
    using global::WebDriverHelper.Setup;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    /// <summary>
    /// The browser web driver chrome.
    /// </summary>
    public static class ChromeWebDriver
    {
        /// <summary>
        /// Creates the new chrome web driver.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The webDriver.</returns>
        public static IWebDriver CreateNewWebDriver(ConfigurationParameters configurationParameters)
        {
            if (configurationParameters.BrowsersConfiguration.ReuseBrowser && configurationParameters.BrowsersConfiguration.ExecutionType == "serial")
            {
               return CreateReusableWebDriver(configurationParameters);
            }

            return CreateWebDriver(configurationParameters);
        }

        /// <summary>
        /// Creates the reusable web driver.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The webdriver object.</returns>
        public static IWebDriver CreateReusableWebDriver(ConfigurationParameters configurationParameters)
        {
            var sessionId = configurationParameters.BrowsersConfiguration.ChromeSessionId;
            var url = configurationParameters.BrowsersConfiguration.ChromeUrl;

            if (sessionId != null && url != null)
            {
                return new ReuseRemoteWebDriver(
                   url,
                   sessionId,
                   CreateChromeOptions(configurationParameters));
            }

            var driver = CreateWebDriver(configurationParameters);
            configurationParameters.BrowsersConfiguration.ChromeSessionId = driver.SessionId.ToString();
            configurationParameters.BrowsersConfiguration.ChromeUrl = ReuseRemoteWebDriver.GetExecutorURLFromDriver(driver);
            return driver;
        }

        /// <summary>
        /// Creates the web driver.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The chrome webdriver.</returns>
        public static ChromeDriver CreateWebDriver(ConfigurationParameters configurationParameters)
        {
            var driver = new ChromeDriver(
            ChromeDriverService.CreateDefaultService(configurationParameters.BrowsersConfiguration.ChromeDriverPath),
            CreateChromeOptions(configurationParameters),
            configurationParameters.BrowsersConfiguration.ChromeBrowserCommandTimeout);
            configurationParameters.BrowsersConfiguration.ChromeSessionId = driver.SessionId.ToString();
            configurationParameters.BrowsersConfiguration.ChromeUrl = ReuseRemoteWebDriver.GetExecutorURLFromDriver(driver);

            return driver;
        }

        /// <summary>
        /// Creates the chrome options.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The chrome driver.</returns>
        private static ChromeOptions CreateChromeOptions(ConfigurationParameters configurationParameters)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", "DownloadPath");
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            chromeOptions.AddArgument("--window-size=1920,1080");
            chromeOptions.AddArguments("disable-infobars");

            if (!string.IsNullOrEmpty(configurationParameters.FiddlerPort))
            {
                chromeOptions.Proxy = Helpers.FiddlerHelper.CreateProxy(configurationParameters.FiddlerPort);
            }

            return chromeOptions;
        }
    }
}