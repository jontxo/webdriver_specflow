// <copyright file="WebTableSteps.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Specflow.Steps
{
    using Automation.Helpers;
    using Automation.Pages;
    using Automation.Reflection;
    using BoDi;
    using DataFactory.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TechTalk.SpecFlow;

    /// <summary>
    /// The web table steps.
    /// </summary>
    [Binding]
    public sealed class WebTableSteps : Steps
    {
        /// <summary>
        /// The web table page.
        /// </summary>
        private readonly WebTablePage webTablePage;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebTableSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public WebTableSteps(IObjectContainer objectContainer)
        {
            this.ConfigurationParameters = objectContainer.Resolve<ConfigurationParameters>();
            this.webTablePage = objectContainer.Resolve<WebTablePage>();
        }

        /// <summary>
        /// Gets the Parameters.
        /// </summary>
        public ConfigurationParameters ConfigurationParameters { get; }

        /// <summary>
        /// Get the information of a cell in a webTable from column and row.
        /// </summary>
        /// <param name="table">the table.</param>
        /// <param name="value">the value.</param>
        /// <param name="structure">the row.</param>
        /// <param name="column">the column.</param>
        [Then("at the webtable (.*) should appear the value (.*) at the structure (.*) in the column (.*)")]
        public void ThenAtTheTableShouldAppearTheValueAtTheStructureInTheColumn(string table, string value, string structure, string column)
        {
            var cellValue = this.webTablePage.GetFieldValue<WebTableHelper>(table).GetCell(structure, column);
            Assert.AreEqual(value, cellValue);
        }

        /// <summary>
        /// at the webTable (.*) all the cells of the column (.*),  contain the value (.*).
        /// </summary>
        /// <param name="webTableName">The webTable name.</param>
        /// <param name="column">the webTable column.</param>
        /// <param name="value">The value.</param>
        [StepDefinition("at the webtable (.*) all the cells of the column (.*),  contain the value (.*)")]
        public void ThenICheckThatTheValuesOfTheColumnContainTheValueM(string webTableName, int column, string value)
        {
            var isvaluePressentInAllCellsOfColumnExceptBlanks = this.webTablePage.GetFieldValue<WebTableHelper>(webTableName)
                                                                    .IsValuePresentInAllTheColumnCellsExceptBlanks(column, value);
            Assert.IsTrue(isvaluePressentInAllCellsOfColumnExceptBlanks);
        }
    }
}