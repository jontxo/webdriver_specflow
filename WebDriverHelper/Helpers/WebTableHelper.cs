//-------------------------------------------------------------------------------------------------
// <copyright file="WebTableHelper.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Automation.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using OpenQA.Selenium;

    /// <summary>
    /// The web table helper.
    /// </summary>
    public class WebTableHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebTableHelper"/> class.
        /// </summary>
        /// <param name="webTable">The webTable.</param>
        public WebTableHelper(IWebElement webTable)
        {
            this.WebTable = webTable;
        }

        /// <summary>
        /// Gets the web table.
        /// </summary>
        private IWebElement WebTable { get; }

        /// <summary>
        /// The get row.
        /// </summary>
        /// <param name="rowNumber">The row number.</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        public IWebElement GetRow(int rowNumber)
        {
            return this.WebTable.FindElement(By.XPath("tr[" + rowNumber + "]"));
        }

        /// <summary>
        /// The get column.
        /// </summary>
        /// <param name="columnNumber">The column number.</param>
        /// <returns>The <see><cref>IList</cref></see> .</returns>
        public IList<IWebElement> GetColumn(int columnNumber)
        {
            return this.WebTable.FindElements(By.XPath("//*/td[" + columnNumber + "]"));
        }

        /// <summary>
        /// The get cell.
        /// </summary>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetCell(int rowNumber, int columnNumber)
        {
            return this.WebTable.FindElement(By.XPath("//*/tr[" + rowNumber + "]/td[" + columnNumber + "]")).Text;
        }

        /// <summary>
        /// The get cell.
        /// </summary>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetCell(int rowNumber, string columnName)
        {
            var columnNumber =
                this.WebTable.FindElements(By.XPath("//th[text() = '" + columnName + "']/preceding-sibling::th")).Count;
            return this.WebTable.FindElement(By.XPath("//tr[" + rowNumber + "]/td[" + columnNumber + "]")).Text;
        }

        /// <summary>
        /// The get cell.
        /// </summary>
        /// <param name="rowName">The row name.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetCell(string rowName, string columnName)
        {
            var columnNumber =
                this.WebTable.FindElements(By.XPath("//th[text() = '" + columnName + "']/preceding-sibling::th")).Count;
            var rowNumber =
                this.WebTable.FindElements(By.XPath("//th[text() = '" + rowName + "']/../preceding-sibling::tr")).Count
                + 1;
            var returnValue =
                this.WebTable.FindElement(By.XPath("//tr[" + rowNumber + "]/td[" + columnNumber + "]")).Text;

            if (columnNumber < 1 || rowNumber < 1)
            {
                throw new NoSuchElementException("Element not found");
            }

            return returnValue;
        }

        /// <summary>
        /// The is value present in all the column cells.
        /// </summary>
        /// <param name="columnNumber">The column number.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsValuePresentInAllTheColumnCells(int columnNumber, string value)
        {
            var count = this.GetColumn(columnNumber).Count(elem => !elem.Text.Contains(value));

            return count == 0;
        }

        /// <summary>
        /// The is value present in all the column cells except blanks.
        /// </summary>
        /// <param name="columnNumber">The column number.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsValuePresentInAllTheColumnCellsExceptBlanks(int columnNumber, string value)
        {
            var count = this.GetColumn(columnNumber).Count(elem => !elem.Text.Contains(value) && !string.IsNullOrEmpty(elem.Text));

            return count == 0;
        }
    }
}