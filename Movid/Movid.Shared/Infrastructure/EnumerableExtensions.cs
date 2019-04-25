using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Movid.Web.Infrastructure
{
    /// <summary>
    /// IEnumerable extensions
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Substitutes an empty collection of type if the source is null
        /// </summary>
        /// <typeparam name="T">the type in the enumerable</typeparam>
        /// <param name="source">the source enumerable</param>
        /// <returns>the original enumerable if not null, a new empty enumerable if source is null</returns>
        public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Performs an action on each value of the enumerable
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="enumerable">Sequence on which to perform action</param>
        /// <param name="action">Action to perform on every item</param>
        /// <exception cref="System.ArgumentNullException">Thrown when given null <paramref name="enumerable"/> or <paramref name="action"/></exception>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
          

            foreach (T value in enumerable)
            {
                action(value);
            }
        }

        /// <summary>
        /// Convenience method for retrieving a specific page of items within a collection.
        /// </summary>
        /// <param name="pageIndex">The index of the page to get.</param>
        /// <param name="pageSize">The size of the pages.</param>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {

            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Converts an enumerable into a readonly collection
        /// </summary>
        public static IEnumerable<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ReadOnlyCollection<T>(enumerable.ToList());
        }

        /// <summary>
        /// Validates that the <paramref name="enumerable"/> is not null and contains items.
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <returns>A string that consists of the members of <paramref name="values"/> delimited by the <paramref name="separator"/> string. If values has no members, the method returns null.</returns>
        public static string JoinOrDefault<T>(this IEnumerable<T> values, string separator)
        {
           
            if (values == null)
                return default(string);

            return string.Join(separator, values);
        }
    }
}
