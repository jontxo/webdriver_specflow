// <copyright file="IEWebDriver.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverHelper
{
    using DataFactory.Configuration;
    using global::WebDriverHelper.Setup;
    using OpenQA.Selenium;
    using OpenQA.Selenium.IE;

    /// <summary>
    /// The browser web driver IE.
    /// </summary>
    public static class IeWebdriver
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
            var sessionId = configurationParameters.BrowsersConfiguration.InternetExplorerSessionId;
            var url = configurationParameters.BrowsersConfiguration.InternetExplorerUrl;

            if (sessionId != null && url != null)
            {
                return new ReuseRemoteWebDriver(
                   url,
                   sessionId,
                   CreateIeOPtions());
            }

            var driver = CreateWebDriver(configurationParameters);
            configurationParameters.BrowsersConfiguration.InternetExplorerSessionId = driver.SessionId.ToString();
            configurationParameters.BrowsersConfiguration.InternetExplorerUrl = ReuseRemoteWebDriver.GetExecutorURLFromDriver(driver);
            return driver;
        }

        /// <summary>
        /// Creates the web driver.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The firefox webdriver.</returns>
        public static InternetExplorerDriver CreateWebDriver(ConfigurationParameters configurationParameters)
        {
            var driver = new InternetExplorerDriver(
            InternetExplorerDriverService.CreateDefaultService(configurationParameters.BrowsersConfiguration.IeDriverPath),
            CreateIeOPtions(),
            configurationParameters.BrowsersConfiguration.ChromeBrowserCommandTimeout);

            return driver;
        }

        /// <summary>
        /// Creates the internet explorer options.
        /// </summary>
        /// <returns>The internet explorer options.</returns>
        private static InternetExplorerOptions CreateIeOPtions()
        {
            // Configure the profile
            return new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                IgnoreZoomLevel = true,
                ForceCreateProcessApi = true,
                EnsureCleanSession = true,
                RequireWindowFocus = true,
                EnablePersistentHover = false,
                BrowserCommandLineArguments = "-private",
            };
        }
    }
}