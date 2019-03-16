//-------------------------------------------------------------------------------------------------
// <copyright file="AndroidWebDriver.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Automation.WebDriverHelper
{
    using System;
    using System.Diagnostics;
    using DataFactory.Configuration;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Appium;
    using OpenQA.Selenium.Appium.Service;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// Android webDriver.
    /// </summary>
    public static class AndroidWebDriver
    {
        /// <summary>
        /// The start appium.
        /// </summary>
        /// <returns>The appium process.</returns>
        public static Process StartAppium()
        {
            Process process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = @"C:\Users\jontxo\AppData\Roaming\npm\appium.cmd",
            };
            process.StartInfo = startInfo;
            process.Start();
            return process;
        }

        /// <summary>
        /// Create and appium local service.
        /// </summary>
        /// <returns>An appium local service.</returns>
        public static AppiumLocalService CreateAppiumLocalService()
        {
            return new AppiumServiceBuilder().UsingPort(4723).Build();
        }

        /// <summary>
        /// Creates the new chrome web driver.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The webDriver.</returns>
        public static IWebDriver CreateNewWebDriver(ConfigurationParameters configurationParameters)
        {
            return CreateWebDriver(configurationParameters);
        }

        /// <summary>
        /// Creates the web driver.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The firefox webdriver.</returns>
        public static IWebDriver CreateWebDriver(ConfigurationParameters configurationParameters)
        {
            var driver = new RemoteWebDriver(
                new Uri("http://127.0.0.1:4723/wd/hub"),
                CreateAppiumOptions(configurationParameters).ToCapabilities(),
                TimeSpan.FromSeconds(120));
            return driver;
        }

        /// <summary>
        /// Create appium options.
        /// </summary>
        /// <param name="configurationParameters">The configuration parameters.</param>
        /// <returns>The appium options.</returns>
        public static AppiumOptions CreateAppiumOptions(ConfigurationParameters configurationParameters)
        {
            var appiumOptions = new AppiumOptions();
            if (configurationParameters.BrowsersConfiguration.Browser == Browser.ChromeAndroid7)
            {
                appiumOptions.AddAdditionalCapability("deviceName", "generic_x86");
                appiumOptions.AddAdditionalCapability("platformVersion", "7.0");
            }
            else if (configurationParameters.BrowsersConfiguration.Browser == Browser.ChromeAndroid9)
            {
                appiumOptions.AddAdditionalCapability("deviceName", "Nexus_5X_API_28");
                appiumOptions.AddAdditionalCapability("platformVersion", "9");
            }

            appiumOptions.AddAdditionalCapability("platformName", "Android");
            appiumOptions.AddAdditionalCapability("fastReset", "True");
            appiumOptions.AddAdditionalCapability("browserName", "Chrome");
            appiumOptions.AddAdditionalCapability("unicodeKeyboard", true);
            appiumOptions.AddAdditionalCapability("resetKeyboard", true);

            return appiumOptions;
        }
    }
}