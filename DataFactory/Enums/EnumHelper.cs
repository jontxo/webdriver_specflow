// <copyright file="EnumHelper.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace AutomationHelpers.Enums
{
    using System;

    /// <summary>
    /// The enum helper class.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Converts to enum.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The enum value of the string.</returns>
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}