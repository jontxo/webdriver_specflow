//-------------------------------------------------------------------------------------------------
// <copyright file="Hooks.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Specflow.GlobalFunctions
{
    using System.Diagnostics;
    using Automation.WebDriverHelper;
    using BoDi;
    using DataFactory.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium.Appium.Service;
    using TechTalk.SpecFlow;

    /// <summary>
    /// The Specflow Hooks.
    /// </summary>
    [Binding]
    [TestClass]
    public class Hooks : Steps
    {
        /// <summary>
        /// The configuration parameters.
        /// </summary>
        private static ConfigurationParameters configurationParameters;

        /// <summary>
        /// The object container.
        /// </summary>
        private IObjectContainer objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hooks"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public Hooks(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        /// <summary>
        /// Gets or sets appium local service.
        /// </summary>
        public static AppiumLocalService AppiumLocalService { get; set; }

        /// <summary>
        /// Gets or sets the webdriver.
        /// </summary>
        public static WebDriverContext WebDriverContextStatic { get; set; }

        /// <summary>
        /// Gets or sets the test context.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Gets the webdrivercontext.
        /// </summary>
        /// <param name="objectContainer">The configuration parameters object.</param>
        /// <param name="configurationParameters">The object container object.</param>
        /// <returns>The webDrvierContext object.</returns>
        public static WebDriverContext GetWebDriver(IObjectContainer objectContainer, ConfigurationParameters configurationParameters)
        {
            if (configurationParameters.BrowsersConfiguration.Browser == Browser.ChromeAndroid9)
            {
                AppiumLocalService = AndroidWebDriver.CreateAppiumLocalService();
                AppiumLocalService.Start();
            }

            if (configurationParameters.BrowsersConfiguration.ReuseBrowser)
            {
                var webDriver = new WebDriverContext(configurationParameters);

                objectContainer.RegisterInstanceAs<WebDriverContext>(webDriver);
                return webDriver;
            }
            else
            {
                var webDriverContext = new WebDriverContext(configurationParameters);
                objectContainer.RegisterInstanceAs<WebDriverContext>(webDriverContext);
                return webDriverContext;
            }
        }

        /// <summary>
        /// Loads the configuration parameters.
        /// </summary>
        [BeforeTestRun]
        public static void LoadConfigurationParameters()
        {
            configurationParameters = new ConfigurationParameters();
        }

        /// <summary>
        /// Stop appium process.
        /// </summary>
        [AfterTestRun]
        public static void StopAppium()
        {
            AppiumLocalService?.Dispose();
        }

        /// <summary>
        /// The execute final steps.
        /// </summary>
        [AfterScenario]
        public void CloseOrResetBrowser()
        {
            try
            {
                var scenarioContext = this.ScenarioContext;
                if (this.objectContainer.IsRegistered<WebDriverContext>() || WebDriverContextStatic != null)
                {
                    var webdriverContext = WebDriverContextStatic == null ? this.objectContainer.Resolve<WebDriverContext>() : WebDriverContextStatic;
                    var configurationParameters = this.objectContainer.Resolve<ConfigurationParameters>();
                    var executionType = configurationParameters.BrowsersConfiguration.ExecutionType;
                    var reuseBrowser = this.objectContainer.Resolve<ConfigurationParameters>().BrowsersConfiguration.ReuseBrowser;

                    if (!reuseBrowser)
                    {
                        webdriverContext?.CloseBrowser();
                        WebDriverContextStatic = null;
                    }

                    if (scenarioContext != null && !webdriverContext.IsWebDriverNull())
                    {
                        var scenarioTestContext = scenarioContext.ScenarioContainer.Resolve<TestContext>() as TestContext;

                        if (scenarioContext.TestError != null)
                        {
                            // Take a screenshot.
                            var screenshotPathFile = webdriverContext.MakeWebScreenshot(scenarioContext.ScenarioInfo.Title, this.TestContext.ResultsDirectory);
                            scenarioTestContext.AddResultFile(screenshotPathFile);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.ToString();
                System.Console.WriteLine(error);
                System.Diagnostics.Trace.WriteLine(error);
                System.Diagnostics.Debug.WriteLine(error);
            }
        }

        /// <summary>
        /// Loads the container.
        /// </summary>
        [BeforeScenario]
        public void LoadContainer()
        {
            var scenarioTestContext = this.ScenarioContext.ScenarioContainer.Resolve<TestContext>() as TestContext;
            this.objectContainer = new SpecflowContainer(this.objectContainer, configurationParameters).ObjectContainer;
            this.objectContainer.Resolve<ConfigurationParameters>().BrowsersConfiguration.ExecutionType = scenarioTestContext.Properties["ExecutionType"].ToString();
            this.objectContainer.Resolve<ConfigurationParameters>().BrowsersConfiguration.ReuseBrowser = bool.Parse(scenarioTestContext.Properties["ReuseBrowser"].ToString());
        }

        /// <summary>
        /// The take screenshot.
        /// </summary>
        [AfterStep]
        public void TakeScreenshot()
        {
            if (this.objectContainer.IsRegistered<WebDriverContext>())
            {
                this.objectContainer.Resolve<WebDriverContext>().TakeScreenshot();
            }
        }
    }
}