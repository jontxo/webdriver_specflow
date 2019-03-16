// <copyright file="CustomChromeRemoteWebDriver.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using DataFactory.Configuration;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// The custom remote web driver.
    /// </summary>
    public class CustomChromeRemoteWebDriver : RemoteWebDriver
    {
        /// <summary>
        /// The session ID path.
        /// </summary>
        private string sessionIdPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomChromeRemoteWebDriver"/> class.
        /// </summary>
        /// <param name="remoteAddress">The remote address.</param>
        /// <param name="driverOptions">The driver options.</param>
        public CustomChromeRemoteWebDriver(Uri remoteAddress, DriverOptions driverOptions)
            : base(remoteAddress, driverOptions)
        {
        }

        /// <summary>
        /// Gets or sets the Parameters.
        /// </summary>
        private ConfigurationParameters Parameters { get; set; }

        /// <summary>
        /// The initialize parameters.
        /// </summary>
        public void InitializeParameters()
        {
            this.Parameters = new ConfigurationParameters();

            var folderName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (folderName != null)
            {
                this.sessionIdPath = Path.Combine(folderName, this.Parameters.BrowsersConfiguration.ChromeFileSessionPath);
            }
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="driverCommandToExecute">The driver command to execute.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The <see cref="Response"/>.</returns>
        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            this.InitializeParameters();

            if (driverCommandToExecute == DriverCommand.NewSession)
            {
                if (File.Exists(this.sessionIdPath))
                {
                    var sidText = File.ReadAllText(this.sessionIdPath);

                    return new Response
                    {
                        SessionId = sidText,
                    };
                }
                else
                {
                    var folderName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    if (folderName != null)
                    {
                        var absoluteFileNameAndPath = Path.Combine(folderName, this.Parameters.BrowsersConfiguration.ChromeDriverPath);
                        Process.Start(absoluteFileNameAndPath);
                    }

                    var response = base.Execute(driverCommandToExecute, parameters);
                    File.WriteAllText(this.sessionIdPath, response.SessionId);
                    return response;
                }
            }
            else
            {
                var response = base.Execute(driverCommandToExecute, parameters);
                return response;
            }
        }
    }
}