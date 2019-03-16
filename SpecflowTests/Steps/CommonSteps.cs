// <copyright file="CommonSteps.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Specflow.Steps
{
    using System;
    using Automation.WebDriverHelper;
    using BoDi;
    using DataFactory.Configuration;
    using Specflow.GlobalFunctions;
    using TechTalk.SpecFlow;

    /// <summary>
    /// The common steps.
    /// </summary>
    [Binding]
    public sealed class CommonSteps : Steps
    {
        /// <summary>
        /// The configuration parameters.
        /// </summary>
        private readonly ConfigurationParameters configurationParameters;

        /// <summary>
        /// The object container.
        /// </summary>
        private readonly IObjectContainer objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public CommonSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
            this.configurationParameters = objectContainer.Resolve<ConfigurationParameters>();
        }

        /// <summary>
        /// The given i navigate to the page.
        /// </summary>
        /// <param name="appName">describe appName parameter on GivenINavigateToThePage.</param>
        [StepDefinition("I navigate to the page (.*)")]
        public void GivenINavigateToThePage(string appName)
        {
            Hooks.GetWebDriver(this.objectContainer, this.configurationParameters).GoToUrl(this.GetAppUrl(appName));
        }

        /// <summary>
        /// The function gets the value of the url from the app.config class.
        /// </summary>
        /// <param name="app">Name of the app.</param>
        /// <returns>Url of the app.</returns>
        private Uri GetAppUrl(string app)
        {
            Uri returnValue;
            switch (app)
            {
                case "webtables":
                    returnValue = this.configurationParameters.Urls.WebTableAppUrl;
                    break;

                case "FileUploadUrl":
                    returnValue = this.configurationParameters.Urls.FileUploadAppUrl;
                    break;

                case "FileDownloadUrl":
                    returnValue = this.configurationParameters.Urls.FileDownloadAppUrl;
                    break;

                case "FieldLimitationsUrl":
                    returnValue = this.configurationParameters.Urls.FieldLimitationsUrl;
                    break;

                case "RegistrationUsersUrl":
                    returnValue = this.configurationParameters.Urls.RegistrationUsersUrl;
                    break;

                case "DragAndDropUrl":
                    returnValue = this.configurationParameters.Urls.DragAndDropUrl;
                    break;

                case "WebElementsColor":
                    returnValue = this.configurationParameters.Urls.WebElementsColorUrl;
                    break;

                default:
                    throw new Exception("url of the app not defined");
            }

            return returnValue;
        }
    }
}