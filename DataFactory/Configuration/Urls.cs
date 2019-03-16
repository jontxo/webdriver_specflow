// <copyright file="Urls.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace DataFactory.Configuration
{
    using System;
    using System.Configuration;

    /// <summary>
    /// The Urls class.
    /// </summary>
    public class Urls
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Urls"/> class.
        /// </summary>
        public Urls()
        {
            this.WebTableAppUrl = new Uri(ConfigurationManager.AppSettings[nameof(this.WebTableAppUrl)]);
            this.ApiUrl = new Uri(ConfigurationManager.AppSettings[nameof(this.ApiUrl)]);
            this.FileUploadAppUrl = new Uri(ConfigurationManager.AppSettings[nameof(this.FileUploadAppUrl)]);
            this.FileDownloadAppUrl = new Uri(ConfigurationManager.AppSettings[nameof(this.FileDownloadAppUrl)]);
            this.FieldLimitationsUrl = new Uri(ConfigurationManager.AppSettings[nameof(this.FieldLimitationsUrl)]);
            this.RegistrationUsersUrl = new Uri(ConfigurationManager.AppSettings[nameof(this.RegistrationUsersUrl)]);
            this.DragAndDropUrl = new Uri(ConfigurationManager.AppSettings[nameof(this.DragAndDropUrl)]);
            this.WebElementsColorUrl = new Uri(ConfigurationManager.AppSettings[nameof(this.WebElementsColorUrl)]);
            this.AppUrl = new Uri(ConfigurationManager.AppSettings[nameof(this.AppUrl)]);
        }

        /// <summary>
        /// Gets or sets the web table application URL.
        /// </summary>
        /// <value>
        /// The web table application URL.
        /// </value>
        public Uri WebTableAppUrl { get; set; }

        /// <summary>
        /// Gets or sets the API URL.
        /// </summary>
        /// <value>
        /// The API URL.
        /// </value>
        public Uri ApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the file upload URL.
        /// </summary>
        /// <value>
        /// The file upload URL.
        /// </value>
        public Uri FileUploadAppUrl { get; set; }

        /// <summary>
        /// Gets or sets the file download URL.
        /// </summary>
        /// <value>
        /// The file download URL.
        /// </value>
        public Uri FileDownloadAppUrl { get; set; }

        /// <summary>
        /// Gets or sets the field limitations URL.
        /// </summary>
        /// <value>
        /// The field limitations URL.
        /// </value>
        public Uri FieldLimitationsUrl { get; set; }

        /// <summary>
        /// Gets or sets the registration users URL.
        /// </summary>
        /// <value>
        /// The registration users URL.
        /// </value>
        public Uri RegistrationUsersUrl { get; set; }

        /// <summary>
        /// Gets or sets the drag and drop URL.
        /// </summary>
        /// <value>
        /// The drag and drop URL.
        /// </value>
        public Uri DragAndDropUrl { get; set; }

        /// <summary>
        /// Gets or sets the web elements color URL.
        /// </summary>
        /// <value>
        /// The web elements color URL.
        /// </value>
        public Uri WebElementsColorUrl { get; set; }

        /// <summary>
        /// Gets or sets the application URL.
        /// </summary>
        /// <value>
        /// The application URL.
        /// </value>
        public Uri AppUrl { get; set; }
    }
}