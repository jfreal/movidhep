using System.Linq;

namespace Movid.Shared.Infrastructure
{
    public static class ViewExtensions
    {
        public static string Words(this string content, int count)
        {
            if (string.IsNullOrEmpty(content))
                return "";

            var words = content.Split(' ').Take(count);

            return string.Join(" ", words) + "...";
        }

        public static string ToRouteFriendlyString(this string original)
        {
            return original.Trim().Replace(" ", "-").ToLower();
        }
    }
}