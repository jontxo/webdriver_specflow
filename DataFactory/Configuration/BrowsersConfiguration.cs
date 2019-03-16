// <copyright file="BrowsersConfiguration.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace DataFactory.Configuration
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using AutomationHelpers.Enums;

    /// <summary>
    /// The BrowsersConfiguration class.
    /// </summary>
    public class BrowsersConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowsersConfiguration"/> class.
        /// </summary>
        public BrowsersConfiguration()
        {
            this.Browser = ConfigurationManager.AppSettings[nameof(this.Browser)].ToEnum<Browser>();
            this.ChromeBrowserCommandTimeout = TimeSpan.FromSeconds(
                int.Parse(
                    ConfigurationManager.AppSettings[nameof(this.ChromeBrowserCommandTimeout)],
                    new CultureInfo("es-ES")));
            this.PageLoadTimeout = int.Parse(
                ConfigurationManager.AppSettings[nameof(this.PageLoadTimeout)],
                new CultureInfo("es-ES"));
            this.AsynchronousJavascriptTimeout = int.Parse(
                ConfigurationManager.AppSettings[nameof(this.AsynchronousJavascriptTimeout)],
                new CultureInfo("es-ES"));
            this.ImplicitWaitTimeout = int.Parse(
                ConfigurationManager.AppSettings[nameof(this.ImplicitWaitTimeout)],
                new CultureInfo("es-ES"));
            this.ChromeFileSessionPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Constants.ChromeSessionId);
            this.ChromeDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.IeDriverPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WebDriver");
            this.FirefoxProcessName = Constants.FirefoxProcessName;
            this.FirefoxBinaryPath = Path.Combine(
                nameof(Environment.SpecialFolder.ProgramFiles),
                Constants.FirefoxExecutableName);
            this.FirefoxDriverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.SoftwareVersion = ConfigurationManager.AppSettings[nameof(this.SoftwareVersion)];
            if (!this.ReuseBrowser)
            {
                this.FirefoxSessionId = ConfigurationManager.AppSettings[nameof(this.FirefoxSessionId)];
                this.ChromeSessionId = ConfigurationManager.AppSettings[nameof(this.ChromeSessionId)];
                this.InternetExplorerSessionId = ConfigurationManager.AppSettings[nameof(this.InternetExplorerSessionId)];
                this.ChromeUrl = string.IsNullOrEmpty(ConfigurationManager.AppSettings[nameof(this.ChromeUrl)])
                    ? null : new Uri(ConfigurationManager.AppSettings[nameof(this.ChromeUrl)]);
                this.FirefoxUrl = string.IsNullOrEmpty(ConfigurationManager.AppSettings[nameof(this.FirefoxUrl)])
                    ? null : new Uri(ConfigurationManager.AppSettings[nameof(this.FirefoxUrl)]);
                this.InternetExplorerUrl = string.IsNullOrEmpty(ConfigurationManager.AppSettings[nameof(this.InternetExplorerUrl)])
                    ? null : new Uri(ConfigurationManager.AppSettings[nameof(this.InternetExplorerUrl)]);
            }
        }

        /// <summary>
        /// Gets or sets the type of the execution.
        /// </summary>
        public string ExecutionType { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        public Browser Browser { get; set; }

        /// <summary>
        /// Gets or sets the chrome browser command timeout.
        /// </summary>
        public TimeSpan ChromeBrowserCommandTimeout { get; set; }

        /// <summary>
        /// Gets or sets the page load timeout.
        /// </summary>
        public int PageLoadTimeout { get; set; }

        /// <summary>
        /// Gets or sets the asynchronous javascript timeout.
        /// </summary>
        public int AsynchronousJavascriptTimeout { get; set; }

        /// <summary>
        /// Gets or sets the implicit wait timeout.
        /// </summary>
        public int ImplicitWaitTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [reuse browser].
        /// </summary>
        public bool ReuseBrowser { get; set; }

        /// <summary>
        /// Gets or sets the chrome file session path.
        /// </summary>
        public string ChromeFileSessionPath { get; set; }

        /// <summary>
        /// Gets or sets the chrome driver path.
        /// </summary>
        public string ChromeDriverPath { get; set; }

        /// <summary>
        /// Gets or sets the ie driver path.
        /// </summary>
        public string IeDriverPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the firefox process.
        /// </summary>
        public string FirefoxProcessName { get; set; }

        /// <summary>
        /// Gets or sets the firefox driver path.
        /// </summary>
        public string FirefoxDriverPath { get; set; }

        /// <summary>
        /// Gets or sets the firefox binary path.
        /// </summary>
        public string FirefoxBinaryPath { get; set; }

        /// <summary>
        /// Gets or sets the software version.
        /// </summary>
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// Gets or sets the chrome session identifier.
        /// </summary>
        public string ChromeSessionId
        {
            get => ConfigurationManager.AppSettings[nameof(this.ChromeSessionId)];

            set
            {
                UpdateAppSettingsKey(nameof(this.ChromeSessionId), value);
            }
        }

        /// <summary>
        /// Gets or sets the firefox session identifier.
        /// </summary>
        public string FirefoxSessionId
        {
            get => ConfigurationManager.AppSettings[nameof(this.FirefoxSessionId)];

            set
            {
                UpdateAppSettingsKey(nameof(this.FirefoxSessionId), value);
            }
        }

        /// <summary>
        /// Gets or sets the internet explorer session identifier.
        /// </summary>
        public string InternetExplorerSessionId
        {
            get => ConfigurationManager.AppSettings[nameof(this.InternetExplorerSessionId)];

            set
            {
                UpdateAppSettingsKey(nameof(this.InternetExplorerSessionId), value);
            }
        }

        /// <summary>
        /// Gets or sets the chrome URL.
        /// </summary>
        public Uri ChromeUrl
        {
            get => string.IsNullOrEmpty(ConfigurationManager.AppSettings[nameof(this.ChromeUrl)])
                ? null
                : new Uri(ConfigurationManager.AppSettings[nameof(this.ChromeUrl)]);

            set
            {
                UpdateAppSettingsKey(nameof(this.ChromeUrl), value?.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the firefox URL.
        /// </summary>
        public Uri FirefoxUrl
        {
            get => string.IsNullOrEmpty(ConfigurationManager.AppSettings[nameof(this.FirefoxUrl)])
                ? null
                : new Uri(ConfigurationManager.AppSettings[nameof(this.FirefoxUrl)]);

            set
            {
                UpdateAppSettingsKey(nameof(this.FirefoxUrl), value?.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the internet explorer URL.
        /// </summary>
        public Uri InternetExplorerUrl
        {
            get => string.IsNullOrEmpty(ConfigurationManager.AppSettings[nameof(this.InternetExplorerUrl)])
                ? null
                : new Uri(ConfigurationManager.AppSettings[nameof(this.InternetExplorerUrl)]);

            set
            {
                UpdateAppSettingsKey(nameof(this.InternetExplorerUrl), value?.ToString());
            }
        }

        /// <summary>
        /// Updates the application settings key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        private static void UpdateAppSettingsKey(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
