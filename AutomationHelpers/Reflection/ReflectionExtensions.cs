// <copyright file="ReflectionExtensions.cs" company="demian INC.">
// Copyright (c) demian INC. All Rights Reserved.Licensed under the Apache License, Version 2.0.
// See LICENSE in the project root for license information.
// </copyright>

namespace Automation.Reflection
{
    using System.Reflection;

    /// <summary>
    /// The reflection extensions class.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the field value.
        /// </summary>
        /// <typeparam name="T">It can by any type.</typeparam>
        /// <param name="baseObject">The object.</param>
        /// <param name="name">The name.</param>
        /// <returns>The value of a field.</returns>
        public static T GetFieldValue<T>(this object baseObject, string name)
        {
            // Set the flags so that private and public fields from instances will be found
            const BindingFlags BindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var field = baseObject.GetType().GetField(name, BindingFlags);
            return (T)field?.GetValue(baseObject);
        }
    }
}