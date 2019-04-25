using System;

namespace Movid.App.Application
{
    public static class JsonHelpers
    {
        public static long ToJavascriptTimestamp(this DateTime input)
        {
            TimeSpan span = new TimeSpan(new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
            DateTime time = input.Subtract(span);
            return (long)(time.Ticks / 10000);
        }
    }
}