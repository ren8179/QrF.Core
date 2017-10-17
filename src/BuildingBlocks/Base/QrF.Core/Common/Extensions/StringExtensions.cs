namespace QrF.Core.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string input) => string.IsNullOrWhiteSpace(input);

        public static bool IsNotEmpty(this string input) => !input.IsEmpty();

        public static string OrEmpty(this string value) => value.IsEmpty() ? string.Empty : value;
    }
}
