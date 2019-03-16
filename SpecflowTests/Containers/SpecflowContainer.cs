// <copyright file="SpecflowContainer.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Specflow
{
    using Automation.Helpers;
    using Automation.Pages;
    using BoDi;
    using DataFactory.Configuration;

    /// <summary>
    /// The Specflow Container class.
    /// </summary>
    public class SpecflowContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecflowContainer"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        /// <param name="configurationParameters">The configuration parameters.</param>
        public SpecflowContainer(IObjectContainer objectContainer, ConfigurationParameters configurationParameters)
        {
            this.ObjectContainer = objectContainer;
            this.ObjectContainer.RegisterInstanceAs<ConfigurationParameters>(configurationParameters);
            this.ObjectContainer.RegisterTypeAs<DragAndDropPage, DragAndDropPage>();
            this.ObjectContainer.RegisterTypeAs<WebElementsChecksPage1, WebElementsChecksPage1>();
            this.ObjectContainer.RegisterTypeAs<WebTablePage, WebTablePage>();
            this.ObjectContainer = objectContainer;
        }

        /// <summary>
        /// Gets the object container.
        /// </summary>
        /// <value>
        /// The object container.
        /// </value>
        public IObjectContainer ObjectContainer { get; }
    }
}