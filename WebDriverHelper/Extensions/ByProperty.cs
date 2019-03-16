//-------------------------------------------------------------------------------------------------
// <copyright file="ByProperty.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Automation.WebDriverExtensions
{
    /// <summary>
    /// Defines the <see cref="ByProperty"/>.
    /// </summary>
    public class ByProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByProperty"/> class.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        private ByProperty(string propertyValue)
        {
            this.PropertyValue = propertyValue;
        }

        /// <summary>
        /// Gets the InnerHTML.
        /// </summary>
        public static ByProperty InnerHTML => new ByProperty("innerHTML");

        /// <summary>
        /// Gets the OuterHTML.
        /// </summary>
        public static ByProperty OuterHTML => new ByProperty("outerHTML");

        /// <summary>
        /// Gets the ClassName.
        /// </summary>
        public static ByProperty ClassName => new ByProperty("className");

        /// <summary>
        /// Gets the ScrollWidth.
        /// </summary>
        public static ByProperty ScrollWidth => new ByProperty("scrollWidth");

        /// <summary>
        /// Gets the ClientWidth.
        /// </summary>
        public static ByProperty ClientWidth => new ByProperty("clientWidth");

        /// <summary>
        /// Gets the PropertyValue.
        /// </summary>
        public string PropertyValue { get; }
    }
}