//-------------------------------------------------------------------------------------------------
// <copyright file="FiddlerHelper.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Automation.Helpers
{
    using System;
    using System.Collections.Generic;
    using Fiddler;

    /// <summary>
    /// Find Helper class.
    /// </summary>
    public class FiddlerHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiddlerHelper"/> class.
        /// </summary>
        public FiddlerHelper()
        {
            this.Messages = new List<Session>();
        }

        /// <summary>
        /// Gets the list that holds the messages collected by fiddler.
        /// </summary>
        public List<Session> Messages { get; }

        /// <summary>
        /// Creates the proxy object of the selenium webDriver.
        /// </summary>
        /// <param name="proxyPort">proxy port that will be used by the proxy.</param>
        /// <returns>returns the proxy object.</returns>
        public static OpenQA.Selenium.Proxy CreateProxy(string proxyPort)
        {
            var proxy = new OpenQA.Selenium.Proxy
            {
                SslProxy = $"localhost:{proxyPort}",
                HttpProxy = $"localhost:{proxyPort}",
            };

            return proxy;
        }

        /// <summary>
        /// Starts the fiddler application proxy.
        /// </summary>
        /// <param name="desiredPort">port number that will use fiddler.</param>
        /// <returns>the proxy port used by fiddler.</returns>
        public static int StartFiddlerProxy(int desiredPort)
        {
            FiddlerApplication.Startup(desiredPort, true, true, true);

            var proxyPort = FiddlerApplication.oProxy.ListenPort;
            Console.WriteLine($"Fiddler proxy listening on port {proxyPort}");
            return proxyPort;
        }

        /// <summary>
        /// It starts the fiddler application.
        /// </summary>
        public void StartFiddler()
        {
            // When the event AfterSessionComplete is called then, the lambda is executed, and the
            // labmda adds all the lambda sessions in the Messages List
            FiddlerApplication.AfterSessionComplete += (Session targetSession) =>
            {
                this.Messages.Add(targetSession);

                // responseCode has the response code of the request fullUrl has the url
            };
        }
    }
}