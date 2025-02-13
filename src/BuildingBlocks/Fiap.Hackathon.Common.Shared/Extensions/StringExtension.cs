﻿namespace Fiap.Hackathon.Common.Shared.Extensions
{
    public static class StringExtension
    {
        public static string? Truncate(this string text, int maxWord)
        {
            if (text == null) return null;
            if (text.Length > maxWord)
                return text.Substring(0, maxWord);
            else
                return text;
        }
    }
}
