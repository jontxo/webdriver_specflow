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

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public static ByProperty BackgroundColor => new ByProperty("background-color");

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public static ByProperty Type => new ByProperty("type");

        /// <summary>
        /// Gets the ng reflect SVG icon.
        /// </summary>
        /// <value>
        /// The ng reflect SVG icon.
        /// </value>
        public static ByProperty NgReflectSvgIcon => new ByProperty("getAttribute('ng-reflect-svg-icon')");

        /// <summary>
        /// Gets the width of the offset.
        /// </summary>
        /// <value>
        /// The width of the offset.
        /// </value>
        public static ByProperty OffsetWidth => new ByProperty("offsetWidth");

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public static ByProperty Color => new ByProperty("color");

        /// <summary>
        /// Gets the color of the border left.
        /// </summary>
        /// <value>
        /// The color of the border left.
        /// </value>
        public static ByProperty BorderLeftColor => new ByProperty("border-left-color");

        /// <summary>
        /// Gets the color of the border right.
        /// </summary>
        /// <value>
        /// The color of the border right.
        /// </value>
        public static ByProperty BorderRightColor => new ByProperty("border-right-color");

        /// <summary>
        /// Gets the color of the border bottom.
        /// </summary>
        /// <value>
        /// The color of the border bottom.
        /// </value>
        public static ByProperty BorderBottomColor => new ByProperty("border-bottom-color");

        /// <summary>
        /// Gets the color of the border top.
        /// </summary>
        /// <value>
        /// The color of the border top.
        /// </value>
        public static ByProperty BorderTopColor => new ByProperty("border-top-color");

        /// <summary>
        /// Gets the color of the style background.
        /// </summary>
        /// <value>
        /// The color of the style background.
        /// </value>
        public static ByProperty StyleBackgroundColor => new ByProperty("backgroundColor");

        /// <summary>
        /// Gets the height of the line.
        /// </summary>
        /// <value>
        /// The height of the line.
        /// </value>
        public static ByProperty LineHeight => new ByProperty("line-height");

        /// <summary>
        /// Gets the height of the style line.
        /// </summary>
        /// <value>
        /// The height of the style line.
        /// </value>
        public static ByProperty StyleLineHeight => new ByProperty("lineHeight");

        /// <summary>
        /// Gets the height of the offset.
        /// </summary>
        /// <value>
        /// The height of the offset.
        /// </value>
        public static ByProperty OffsetHeight => new ByProperty("offsetHeight");

        /// <summary>
        /// Gets the margin top.
        /// </summary>
        /// <value>
        /// The margin top.
        /// </value>
        public static ByProperty MarginTop => new ByProperty("margin-top");

        /// <summary>
        /// Gets the style margin top.
        /// </summary>
        /// <value>
        /// The style margin top.
        /// </value>
        public static ByProperty StyleMarginTop => new ByProperty("marginTop");

        /// <summary>
        /// Gets the margin bottom.
        /// </summary>
        /// <value>
        /// The margin bottom.
        /// </value>
        public static ByProperty MarginBottom => new ByProperty("margin-bottom");

        /// <summary>
        /// Gets the style margin bottom.
        /// </summary>
        /// <value>
        /// The style margin bottom.
        /// </value>
        public static ByProperty StyleMarginBottom => new ByProperty("marginBottom");

        /// <summary>
        /// Gets the maximum length.
        /// </summary>
        /// <value>
        /// The maximum length.
        /// </value>
        public static ByProperty MaxLength => new ByProperty("maxLength");

        /// <summary>
        /// Gets the margin right.
        /// </summary>
        /// <value>
        /// The margin right.
        /// </value>
        public static ByProperty MarginRight => new ByProperty("margin-right");
    }
}