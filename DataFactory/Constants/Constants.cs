// <copyright file="Constants.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace DataFactory.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The front end basic context.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Constants
    {
        /// <summary>
        /// The chrome session identifier.
        /// </summary>
        public const string ChromeSessionId = "ChromeSessionId";

        /// <summary>
        /// The chrome driver executable.
        /// </summary>
        public const string ChromeDriverExecutable = "chromedriver.exe";

        /// <summary>
        /// The firefox process name.
        /// </summary>
        public const string FirefoxProcessName = "Firefox";

        /// <summary>
        /// The firefox executable name.
        /// </summary>
        public const string FirefoxExecutableName = "firefox.exe";

        /// <summary>
        /// The animation wait in millis.
        /// </summary>
        public const int AnimationWaitInMillis = 1000;

        /// <summary>
        /// The chrome.
        /// </summary>
        public const string Chrome = "Chrome";

        /// <summary>
        /// The default password.
        /// </summary>
        public const string DefaultPassword = "epayslips123";

        /// <summary>
        /// The internet explorer.
        /// </summary>
        public const string InternetExplorer = "InternetExplorer";

        /// <summary>
        /// The browser context.
        /// </summary>
        public const string BrowserContext = "WebDriverContext";

        /// <summary>
        /// The users data sheet user name column.
        /// </summary>
        public const string UsersDataSheetUserNameColumn = "A";

        /// <summary>
        /// The user data sheet user password column.
        /// </summary>
        public const string UserDataSheetUserPasswordColumn = "B";

        /// <summary>
        /// The user data sheet first name column.
        /// </summary>
        public const string UserDataSheetFirstNameColumn = "C";

        /// <summary>
        /// The user data sheet last name column.
        /// </summary>
        public const string UserDataSheetLastNameColumn = "D";

        /// <summary>
        /// The user data sheet phone column.
        /// </summary>
        public const string UserDataSheetPhoneColumn = "E";

        /// <summary>
        /// The user data sheet email column.
        /// </summary>
        public const string UserDataSheetEmailColumn = "F";

        /// <summary>
        /// The user data sheet address 1 column.
        /// </summary>
        public const string UserDataSheetAddress1Column = "G";

        /// <summary>
        /// The user data sheet address 2 column.
        /// </summary>
        public const string UserDataSheetAddress2Column = "H";

        /// <summary>
        /// The user data sheet city column.
        /// </summary>
        public const string UserDataSheetCityColumn = "I";

        /// <summary>
        /// The user data sheet state province.
        /// </summary>
        public const string UserDataSheetStateProvince = "J";

        /// <summary>
        /// The user data sheet postal code.
        /// </summary>
        public const string UserDataSheetPostalCode = "K";

        /// <summary>
        /// The use data sheet country.
        /// </summary>
        public const string UserDataSheetCountry = "L";

        /// <summary>
        /// The user data sheet name.
        /// </summary>
        public const string UserDataSheetName = "UsersData";

        /// <summary>
        /// The collection name.
        /// </summary>
        public const string ClientsDatabase = "testspecflow";

        /// <summary>
        /// The clients database.
        /// </summary>
        public const string ClientsCollection = "ClientsWithAddress";

        /// <summary>
        /// The existing user.
        /// </summary>
        public const string ExistingUser = "ExistingUser";

        /// <summary>
        /// The non existing user.
        /// </summary>
        public const string NonExistingUser = "NonExistingUser";

        /// <summary>
        /// The new user.
        /// </summary>
        public const string NewUser = "NewUser";

        /// <summary>
        /// The android 7.
        /// </summary>
        public const string Android7 = "android_7";

        /// <summary>
        /// The android 8.
        /// </summary>
        public const string Android8 = "android_8";

        /// <summary>
        /// The android 9.
        /// </summary>
        public const string Android9 = "android_9";

        /// <summary>
        /// The red color.
        /// </summary>
        public const string RedHexValue = "2BA6CB";

        /// <summary>
        /// The red color.
        /// </summary>
        public const string RedColor = "RED";
    }
}