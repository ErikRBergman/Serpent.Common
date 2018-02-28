namespace Serpent.Common.BaseTypeExtensions
{
    public static class StringExtensions
    {
        public static decimal ToDecimalOrDefault(this string text, decimal defaultValue)
        {
            if (string.IsNullOrWhiteSpace(text) || decimal.TryParse(text, out var value) == false)
            {
                return defaultValue;
            }

            return value;
        }
    }
}