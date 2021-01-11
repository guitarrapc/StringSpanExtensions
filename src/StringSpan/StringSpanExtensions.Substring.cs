using System;

namespace StringSpan
{
    public static class StringSpanSubstringExtensions
    {
        public static ReadOnlySpan<char> SubstringSpan(this string value, int startIndex)
        {
            return value.SubstringSpan(startIndex, value.Length);
        }

        public static ReadOnlySpan<char> SubstringSpan(this string value, int startIndex, int length)
        {
            var span = value.AsSpan();
            var chars = span[startIndex..length];
            return chars;
        }
    }
}
