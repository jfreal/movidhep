using System;
using System.Collections.Generic;
using System.Linq;

namespace Movid.App.Application
{
    public static class ShortUrl
    {
        public static string Create()
        {
            const string baseUrlChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        
            const int numberOfCharsToSelect = 5;
            int maxNumber = baseUrlChars.Length;

            var rnd = new Random();
            var numList = new List<int>();

            for (int i = 0; i < numberOfCharsToSelect; i++)
                numList.Add(rnd.Next(maxNumber));

            return numList.Aggregate(string.Empty, (current, num) => current + baseUrlChars.Substring(num, 1));
            
        }
    }
}