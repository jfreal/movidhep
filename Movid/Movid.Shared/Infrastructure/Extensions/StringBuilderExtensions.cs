using System.Text;

namespace Movid.Web.Infrastructure.Extensions
{
    /// <summary>
    /// String builder extensions
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Append and format with a line break
        /// </summary>
        /// <param name="stringBuilder">extended object</param>
        /// <param name="source">string which contains formatting information</param>
        /// <param name="args">values to put into the fortmat string</param>
        public static void AppendLineFormat(this StringBuilder stringBuilder, string source, params object[] args)
        {
            stringBuilder.AppendFormat(source + "\r\n", args);
        }
    }
}
