using System.Collections.Generic;
using System.Linq;

namespace Movid.Web.Infrastructure.Extensions
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

       
    }
}
