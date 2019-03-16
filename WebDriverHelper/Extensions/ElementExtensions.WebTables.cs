// <copyright file="ElementExtensions.WebTables.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.WebDriverExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using OpenQA.Selenium;

    /// <summary>
    /// The Element extensions.
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="rowNumber">The row number.</param>
        /// <returns>A webelement row.</returns>
        public static IWebElement GetRow(this IWebElement element, int rowNumber)
        {
            return element.FindElement(By.XPath(".//*/tr[" + rowNumber + "]"));
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <returns>A webelmeent column.</returns>
        public static List<IWebElement> GetColumn(this IWebElement element, int columnNumber)
        {
            return element.FindElements(By.XPath(".//*/td[" + columnNumber + "]")).ToList();
        }

        /// <summary>
        /// Gets the cell icon.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <returns>The webelement cell column.</returns>
        public static string GetCellIcon(this IWebElement element, int rowNumber, int columnNumber)
        {
            return element.FindElement(By.XPath(".//*/tr[" + rowNumber + "]/td[" + columnNumber + "]")).Text;
        }

        /// <summary>
        /// Gets the cell text.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <returns>The cell content.</returns>
        public static string GetCellText(this IWebElement element, int rowNumber, int columnNumber)
        {
            return element.FindElement(By.XPath(".//*/tr[" + rowNumber + "]/td[" + columnNumber + "]")).Text;
        }

        /// <summary>
        /// Gets the cell.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <returns>The webelement cell.</returns>
        public static IWebElement GetCell(this IWebElement element, int rowNumber, int columnNumber)
        {
            return element.FindElement(By.XPath(".//*/tr[" + rowNumber + "]/td[" + columnNumber + "]"));
        }

        /// <summary>
        /// Gets the cell.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="rowName">Name of the row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The webelement cell.</returns>
        public static string GetCell(this IWebElement element, string rowName, string columnName)
        {
            var columnNumber = element.GetColumnNumber(columnName);
            var rowNumber = element.GetRowNumber(rowName);

            return element.GetCellText(rowNumber, columnNumber);
        }

        /// <summary>
        /// Gets the header column.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <returns>The header column.</returns>
        public static string GetHeaderColumn(this IWebElement element, int columnNumber)
        {
            return element.FindElement(By.XPath(".//*/tr[" + columnNumber + "]/th[" + columnNumber + "]")).Text;
        }

        /// <summary>
        /// Gets the column number.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>THe column number.</returns>
        /// <exception cref="NoSuchElementException">Element {columnName}.</exception>
        public static int GetColumnNumber(this IWebElement element, string columnName)
        {
            var columnNumber = element.FindElements(By.XPath(".//th[contains(text(), '" + columnName + "')]/preceding-sibling::th")).Count + 1;

            if (columnNumber == 0)
            {
                throw new NoSuchElementException($"Element {columnName} not found");
            }

            return columnNumber;
        }

        /// <summary>
        /// Gets the row number.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="rowName">Name of the row.</param>
        /// <returns>The rownumber.</returns>
        /// <exception cref="NoSuchElementException">Element {rowNumber}.</exception>
        public static int GetRowNumber(this IWebElement element, string rowName)
        {
            var rowNumber = element.FindElements(By.XPath(".//th[text() = '" + rowName + "']/../preceding-sibling::tr")).Count + 1;

            if (rowNumber == 0)
            {
                throw new NoSuchElementException($"Element {rowNumber} not found");
            }

            return rowNumber;
        }

        /// <summary>
        /// Gets the row number.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The row number.</returns>
        public static int GetRowNumber(this IWebElement element)
        {
            return element.FindElements(By.XPath(".//*/tr")).Count;
        }

        /// <summary>
        /// Scrolls to end of table.
        /// </summary>
        /// <param name="element">The element.</param>
        public static void ScrollToEndOfTable(this IWebElement element)
        {
            var rowCount = 50;
            while (rowCount != element.GetRowNumber())
            {
                rowCount = element.GetRowNumber();
                element.GetRow(rowCount - 1).ScrollToElement();
                WaitThread(250);
            }
        }
    }
}
