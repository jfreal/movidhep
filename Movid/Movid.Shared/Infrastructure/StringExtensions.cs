// <copyright file="StringExtensions.cs" company="UCHC">
// Copyright (c) 2010 All Rights Reserved
// </copyright>
// <summary>
// String extensions
// </summary>

namespace UCHC.Core.Extensions
{
    #region

    using System.Text;

    #endregion

    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Inserts spaces before camel cased characters in a string
        /// </summary>
        /// <param name="camelCasedString">The camel cased string.</param>
        /// <returns>a string with spaces inserted before the camel cased characters</returns>
        public static string InsertSpacesIntoCamelCase(this string camelCasedString)
        {
            if (string.IsNullOrEmpty(camelCasedString))
            {
                return camelCasedString;
            }

            int stringLength = camelCasedString.Length;

            if ( stringLength == 1 || stringLength == 2)
            {
                return camelCasedString;                
            }

            return System.Text.RegularExpressions.Regex.Replace(camelCasedString, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }

        public static string NulltoEmpty(this string target )
        {
            if (string.IsNullOrEmpty(target))
                return "";

            return target;
        }

        #endregion
    }
}