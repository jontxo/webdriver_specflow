// <copyright file="ReuseRemoteWebDriver.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace WebDriverHelper.Setup
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using DataFactory.Configuration;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    /// <summary>
    /// The Reuse Remote Driver.
    /// </summary>
    public class ReuseRemoteWebDriver : RemoteWebDriver
    {
        /// <summary>
        /// The session identifier.
        /// </summary>
        private readonly string sessionId;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReuseRemoteWebDriver"/> class.
        /// </summary>
        /// <param name="remoteAddress">The remote address.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="driverOptions">The driver options.</param>
        public ReuseRemoteWebDriver(Uri remoteAddress, string sessionId, DriverOptions driverOptions)
    : base(remoteAddress, driverOptions)
        {
            this.sessionId = sessionId;
            var sessionIdBase = this.GetType()
                .BaseType
                .GetField(
                    nameof(sessionId),
                    BindingFlags.Instance | BindingFlags.NonPublic);
            sessionIdBase.SetValue(this, new OpenQA.Selenium.Remote.SessionId(sessionId));
        }

        /// <summary>
        /// Gets the executor URL from driver.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <returns>The uri of the webdriver.</returns>
        public static Uri GetExecutorURLFromDriver(RemoteWebDriver driver)
        {
            Contract.Ensures(Contract.Result<Uri>() != null);
            var executorField = typeof(RemoteWebDriver)
                .GetField(
                    "executor",
                    BindingFlags.NonPublic | BindingFlags.Instance);

            var executor = executorField.GetValue(driver);

            var internalExecutorField = executor.GetType()
                .GetField(
                        "internalExecutor",
                        BindingFlags.NonPublic | BindingFlags.Instance);
            var internalExecutor = internalExecutorField.GetValue(executor);

            // executor.CommandInfoRepository
            var remoteServerUriField = internalExecutor.GetType()
                .GetField(
                    "remoteServerUri",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            var remoteServerUri = remoteServerUriField.GetValue(internalExecutor) as Uri;

            return remoteServerUri;
        }

        /// <summary>
        /// Executes a command with this driver .
        /// </summary>
        /// <param name="driverCommandToExecute">A <see cref="DriverCommand" /> value representing the command to execute.</param>
        /// <param name="parameters">A <see cref="IDictionary" /> containing the names and values of the parameters of the command.</param>
        /// <returns>
        /// A <see cref="OpenQA.Selenium.Remote.Response" /> containing information about the success or failure of the command and any data returned by the command.
        /// </returns>
        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters)
        {
            if (driverCommandToExecute == DriverCommand.NewSession)
            {
                var resp = new Response
                {
                    Status = WebDriverResult.Success,
                    SessionId = this.sessionId,
                    Value = new Dictionary<string, object>(),
                };
                return resp;
            }

            var respBase = base.Execute(driverCommandToExecute, parameters);
            return respBase;
        }
    }
}
