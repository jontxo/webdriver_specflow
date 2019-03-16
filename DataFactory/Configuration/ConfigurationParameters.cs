// <copyright file="ConfigurationParameters.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace DataFactory.Configuration
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using AutomationHelpers.Enums;

    /// <summary>
    /// The configuration parameters.
    /// </summary>
    public class ConfigurationParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationParameters"/> class.
        /// </summary>
        public ConfigurationParameters()
        {
            this.Urls = new Urls();
            this.BrowsersConfiguration = new BrowsersConfiguration();
            this.FiddlerPort = ConfigurationManager.AppSettings[nameof(this.FiddlerPort)];
            this.FileOperationsTimeout = int.Parse(ConfigurationManager.AppSettings[nameof(this.FileOperationsTimeout)], new CultureInfo("es-ES"));
            this.ProjectRootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.MongoDbConnectionString = ConfigurationManager.AppSettings[nameof(this.MongoDbConnectionString)];
            this.MongoDbDatabaseName = ConfigurationManager.AppSettings[nameof(this.MongoDbDatabaseName)];
            this.MongoDbCollectionName = ConfigurationManager.AppSettings[nameof(this.MongoDbCollectionName)];
            this.ExcelParametersFile = ConfigurationManager.AppSettings[nameof(this.ExcelParametersFile)];
        }

        /// <summary>
        /// Gets or sets the browsers configuration.
        /// </summary>
        /// <value>
        /// The browsers configuration.
        /// </value>
        public BrowsersConfiguration BrowsersConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the fiddler port.
        /// </summary>
        /// <value>
        /// The fiddler port.
        /// </value>
        public string FiddlerPort { get; set; }

        /// <summary>
        /// Gets or sets the file operations timeout.
        /// </summary>
        /// <value>
        /// The file operations timeout.
        /// </value>
        public int FileOperationsTimeout { get; set; }

        /// <summary>
        /// Gets or sets the project root folder.
        /// </summary>
        /// <value>
        /// The project root folder.
        /// </value>
        public string ProjectRootFolder { get; set; }

        /// <summary>
        /// Gets or sets the mongo database connection string.
        /// </summary>
        /// <value>
        /// The mongo database connection string.
        /// </value>
        public string MongoDbConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the mongo database database.
        /// </summary>
        /// <value>
        /// The name of the mongo database database.
        /// </value>
        public string MongoDbDatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the name of the mongo database collection.
        /// </summary>
        /// <value>
        /// The name of the mongo database collection.
        /// </value>
        public string MongoDbCollectionName { get; set; }

        /// <summary>
        /// Gets or sets the excel parameters file.
        /// </summary>
        /// <value>
        /// The excel parameters file.
        /// </value>
        public string ExcelParametersFile { get; set; }

        /// <summary>
        /// Gets or sets the Urls.
        /// </summary>
        /// <value>
        /// The Urls.
        /// </value>
        public Urls Urls { get; set; }
    }
}