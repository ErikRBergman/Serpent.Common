namespace Serpent.Common.BaseTypeExtensions
{
    using System;

    public static class StringExtensions
    {
        public static decimal ToDecimalOrDefault(this string input, decimal defaultValue)
        {
            if (decimal.TryParse(input, out var value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        public static decimal? ToDecimalOrDefaultNullable(this string input, decimal? defaultValue = null)
        {
            if (decimal.TryParse(input, out var value) == false)
            {
                return defaultValue;
            }

            return value;
        }


        public static int ToIntOrDefault(this string input, int defaultValue)
        {
            if (int.TryParse(input, out var value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        public static int? ToIntOrDefaultNullable(this string input, int? defaultValue = null)
        {
            if (int.TryParse(input, out var value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        public static TimeSpan ToTimeSpanOrDefault(this string input, TimeSpan defaultValue)
        {
            if (TimeSpan.TryParse(input, out var result))
            {
                return result;
            }

            return defaultValue;
        }

        public static TimeSpan? ToTimeSpanOrDefaultNullable(this string input, TimeSpan? defaultValue)
        {
            if (TimeSpan.TryParse(input, out var result))
            {
                return result;
            }

            return defaultValue;
        }
    }
}