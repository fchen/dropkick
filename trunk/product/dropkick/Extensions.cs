namespace dropkick
{
    using System;

    public static class Extensions
    {
        public static string FormatWith(this string input, params object[] args)
        {
            return string.Format(input, args);
        }
        public static T ToEnum<T>(this string input)
        {
            return (T) Enum.Parse(typeof(T), input, true);
        }
    }
}