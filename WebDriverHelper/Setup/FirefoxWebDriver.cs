// <copyright file="FirefoxWebDriver.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverHelper
{
    using DataFactory.Configuration;
    using global::WebDriverHelper.Setup;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    /// <summary>
    /// The browser web driver firefox.
    /// </summary>
    public static class FirefoxWebDriver
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
            var sessionId = configurationParameters.BrowsersConfiguration.FirefoxSessionId;
            var url = configurationParameters.BrowsersConfiguration.FirefoxUrl;

            if (sessionId != null && url != null)
            {
                return new ReuseRemoteWebDriver(
                   url,
                   sessionId,
                   CreateFirefoxOptions(configurationParameters));
            }

            var driver = CreateWebDriver(configurationParameters);
            configurationParameters.BrowsersConfiguration.FirefoxSessionId = driver.SessionId.ToString();
            configurationParameters.BrowsersConfiguration.FirefoxUrl = ReuseRemoteWebDriver.GetExecutorURLFromDriver(driver);
            return driver;
        }

        /// <summary>
        /// Creates the web driver.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The firefox webdriver.</returns>
        public static FirefoxDriver CreateWebDriver(ConfigurationParameters configurationParameters)
        {
            var driver = new FirefoxDriver(
            FirefoxDriverService.CreateDefaultService(configurationParameters.BrowsersConfiguration.FirefoxDriverPath),
            CreateFirefoxOptions(configurationParameters),
            configurationParameters.BrowsersConfiguration.ChromeBrowserCommandTimeout);

            return driver;
        }

        /// <summary>
        /// Creates the firefox options.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The firefox options.</returns>
        private static FirefoxOptions CreateFirefoxOptions(ConfigurationParameters configurationParameters)
        {
            var firefoxOptions = new FirefoxOptions
            {
                Profile = CreateFirefoxProfile(configurationParameters),
            };
            return firefoxOptions;
        }

        /// <summary>
        /// Creates the firefox profile.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>A firefox profile.</returns>
        private static FirefoxProfile CreateFirefoxProfile(ConfigurationParameters configurationParameters)
        {
            // Configure the profile
            var firefoxProfile = new FirefoxProfile();

            // Configure the url with which the browser start
            firefoxProfile.SetPreference("browser.startup.homepage", "about:blank");
            firefoxProfile.SetPreference("startup.homepage_welcome_url", "about:blank");
            firefoxProfile.SetPreference("startup.homepage_welcome_url.additional", "about:blank");
            firefoxProfile.SetPreference("browser.startup.homepage_override.mstone", "ignore");

            firefoxProfile.SetPreference("browser.download.dir", @"C:\GIT\Downloads");
            firefoxProfile.SetPreference("browser.download.folderList", 1);
            firefoxProfile.SetPreference("browser.download.pannel.show", true);
            firefoxProfile.SetPreference("browser.download.useDownloadDir", false);

            // Set the binary path
            firefoxProfile.SetPreference(
                "webdriver.firefox.bin",
                configurationParameters.BrowsersConfiguration.FirefoxBinaryPath);

            return firefoxProfile;
        }
    }
}