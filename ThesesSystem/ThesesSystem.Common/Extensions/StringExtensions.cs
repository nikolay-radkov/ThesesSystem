namespace ThesesSystem.Common.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static string TruncateLongString(this string str, int maxLength)
        {
            if (str.Length > maxLength)
            {
                return str.Substring(0, maxLength) + "...";
            }

            return str.Substring(0, str.Length);
        }
    }
}
