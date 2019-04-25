namespace Movid.Web.Infrastructure.Extensions
{
    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtensions
    {

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

            public static string TruncateWithDots(this string value, int maxLength)
            {
                return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
            }


    }
}