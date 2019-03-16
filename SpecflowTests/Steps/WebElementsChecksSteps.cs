// <copyright file="WebElementsChecksSteps.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Specflow.Steps
{
    using System.Globalization;
    using Automation.Pages;
    using BoDi;
    using FluentAssertions;
    using TechTalk.SpecFlow;

    /// <summary>
    /// The WebElement check steps.
    /// </summary>
    [Binding]
    public sealed class WebElementsChecksSteps
    {
        /// <summary>
        /// The object container.
        /// </summary>
        private readonly IObjectContainer objectContainer;

        /// <summary>
        /// The web elements checks page1.
        /// </summary>
        private readonly WebElementsChecksPage1 webElementsChecksPage1;

        /// <summary>
        /// The home page.
        /// </summary>
        private readonly HomePage homePage;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebElementsChecksSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public WebElementsChecksSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
            this.webElementsChecksPage1 = objectContainer.Resolve<WebElementsChecksPage1>();
            this.homePage = objectContainer.Resolve<HomePage>();
        }

        /// <summary>
        /// Then the color of the element element should be red.
        /// </summary>
        /// <param name="color">The color.</param>
        [StepDefinition("The color of the element element should be (.*)")]
        public void ThenTheColorOfTheElementElementShouldBeRed(string color)
        {
            var elementColor = this.webElementsChecksPage1.GetBlueButtonBackgroundColor();
            elementColor.Should().Be(color.ToUpper(new CultureInfo("es-ES", false)));
        }

        /// <summary>
        /// Thens the element has lines.
        /// </summary>
        /// <param name="lineNumber">The line number.</param>
        [Then("The element has '(.*)' lines")]
        public void ThenTheElementHasLines(int lineNumber)
        {
           this.homePage.GetElementLineNumbers().Should().Be(lineNumber);
        }
    }
}