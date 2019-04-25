using System.Collections.Generic;

namespace Movid.Web.Infrastructure
{
    /// <summary>
    /// The expression based resolver interface
    /// </summary>
    public interface IExpressionBasedResolver
    {

        /// <summary>
        /// Gets the  source property names.
        /// </summary>
        /// <returns>
        /// an enumerable of source property names
        /// </returns>
        IEnumerable<string> GetSourcePropertyNames();

    }
}