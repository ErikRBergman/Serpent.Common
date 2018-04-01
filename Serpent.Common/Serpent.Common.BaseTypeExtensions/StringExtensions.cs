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

        public static decimal ToDecimalOrDefault(this string input, Func<decimal, bool> predicate, decimal defaultValue)
        {
            if (decimal.TryParse(input, out var value) == false || predicate(value) == false)
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

        public static decimal? ToDecimalOrDefaultNullable(this string input, Func<decimal, bool> predicate, decimal? defaultValue = null)
        {
            if (decimal.TryParse(input, out var value) == false || predicate(value) == false)
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

        public static int ToIntOrDefault(this string input, Func<int, bool> predicate, int defaultValue)
        {
            if (int.TryParse(input, out var value) == false || predicate(value) == false)
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

        public static int? ToIntOrDefaultNullable(this string input, Func<int, bool> predicate, int? defaultValue = null)
        {
            if (int.TryParse(input, out var value) == false || predicate(value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        public static TimeSpan ToTimeSpanOrDefault(this string input, Func<TimeSpan, bool> predicate, TimeSpan defaultValue)
        {
            if (TimeSpan.TryParse(input, out var value) || predicate(value) == false)
            {
                return value;
            }

            return defaultValue;
        }

        public static TimeSpan ToTimeSpanOrDefault(this string input, TimeSpan defaultValue)
        {
            if (TimeSpan.TryParse(input, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        public static TimeSpan? ToTimeSpanOrDefaultNullable(this string input, Func<TimeSpan, bool> predicate, TimeSpan? defaultValue)
        {
            if (TimeSpan.TryParse(input, out var value) || predicate(value) == false)
            {
                return value;
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



        public static double ToDoubleOrDefault(this string input, double defaultValue)
        {
            if (double.TryParse(input, out var value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        public static double ToDoubleOrDefault(this string input, Func<double, bool> predicate, double defaultValue)
        {
            if (double.TryParse(input, out var value) == false || predicate(value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        public static double? ToDoubleOrDefault(this string input, double? defaultValue = null)
        {
            if (double.TryParse(input, out var value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        public static double? ToDoubleOrDefault(this string input, Func<double, bool> predicate, double? defaultValue = null)
        {
            if (double.TryParse(input, out var value) == false || predicate(value) == false)
            {
                return defaultValue;
            }

            return value;
        }

        public static DateTime ToDateTimeOrDefault(this string input, DateTime defaultValue)
        {
            if (!DateTime.TryParse(input, out var value))
            {
                return defaultValue;
            }

            return value;
        }

        public static DateTime? ToDateTimeOrDefaultNullable(this string input, DateTime? defaultValue = null)
        {
            if (!DateTime.TryParse(input, out var value))
            {
                return defaultValue;
            }

            return value;
        }

        public static DateTime ToDateTimeOrDefault(this string input, Func<DateTime, bool> predicate, DateTime defaultValue)
        {
            if (!DateTime.TryParse(input, out var value))
            {
                return defaultValue;
            }

            return predicate(value) ? value : defaultValue;
        }

        public static DateTime? ToDateTimeOrDefaultNullable(this string input, Func<DateTime, bool> predicate, DateTime? defaultValue = null)
        {
            if (!DateTime.TryParse(input, out var value))
            {
                return defaultValue;
            }

            return predicate(value) ? value : defaultValue;
        }
    }
}