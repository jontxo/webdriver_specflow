// <copyright file="WebTablePage.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.Pages
{
    using System.Linq;
    using Automation.Helpers;
    using Automation.WebDriverHelper;
    using BoDi;
    using OpenQA.Selenium;
    using SeleniumExtras.PageObjects;

    /// <summary>
    /// The web table page.
    /// </summary>
    public class WebTablePage
    {
        #region webelements
#pragma warning disable 0169, 0649

        /// <summary>
        /// The web table.
        /// </summary>
        [FindsBy(How = How.XPath, Using = "//*[@id='content']/table")]
        private readonly IWebElement webTableWebElement;

#pragma warning restore 0169, 0649
        #endregion webelements

        /// <summary>
        /// The web table.
        /// </summary>
        private readonly WebTableHelper webTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebTablePage"/> class.
        /// </summary>
        /// <param name="webDriverContext">The web driver context.</param>
        public WebTablePage(WebDriverContext webDriverContext)
        {
            PageFactory.InitElements(webDriverContext.WebDriver, this);
            this.webTable = new WebTableHelper(this.webTableWebElement);
        }

        /// <summary>
        /// The get structure country value.
        /// </summary>
        /// <param name="structure">The structure.</param>
        /// <param name="country">The country.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetStructureCountryValue(string structure, string country)
        {
            return this.webTable.GetCell(structure, country);
        }

        /// <summary>
        /// The get value of cell.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetCellValue(string row, string column)
        {
            return this.webTable.GetCell(row, column);
        }

        /// <summary>
        /// The is value present in column.
        /// </summary>
        /// <param name="columnNumber">The column number.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsValuePresentInAllTheColumnCells(int columnNumber, string value)
        {
            var count = this.webTable.GetColumn(columnNumber)
                .Count(elem => !elem.Text.Contains(value));

            return count == 0;
        }
    }
}