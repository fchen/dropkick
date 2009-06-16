namespace dropkick
{
    public static class Extensions
    {
        public static string FormatWith(this string input, params object[] args)
        {
            return string.Format(input, args);
        }
    }
}