
using System;
using System.Collections.Generic;
using System.Text;

namespace Movid.Web.Infrastructure
{
    /// <summary>
    /// csv extensions
    /// </summary>
    public static class CsvExtensions
    {
        #region Public Methods

        /// <summary>Performs an action on each member of an enumerable</summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action to perform.</param>
        /// <typeparam name="T">the type to perform the action on</typeparam>
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        /// <summary>Convert an enumerable to a comma separated value string</summary>
        /// <param name="instance">The instance.</param>
        /// <param name="separator">The separator.</param>
        /// <typeparam name="T">the type to convert to comma separated values</typeparam>
        /// <returns>The csv string</returns>
        public static string ToCsv<T>(this IEnumerable<T> instance, char separator)
        {
            StringBuilder csv;
            if (instance != null)
            {
                csv = new StringBuilder();
                instance.Each(value => csv.AppendFormat("{0}{1}", value, separator));
                return csv.ToString(0, csv.Length - 1);
            }

            return null;
        }

        /// <summary>Convert an enumerable to a comma separated value string</summary>
        /// <param name="instance">The instance.</param>
        /// <typeparam name="T">the type to convert to comma separated values</typeparam>
        /// <returns>The csv string.</returns>
        public static string ToCsv<T>(this IEnumerable<T> instance)
        {
            StringBuilder csv;
            if (instance != null)
            {
                csv = new StringBuilder();
                instance.Each(v => csv.AppendFormat("{0},", v));

                return csv.Length == 0 ? string.Empty : csv.ToString(0, csv.Length - 1);
            }

            return null;
        }

        #endregion
    }
}