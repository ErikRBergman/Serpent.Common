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


        /// <summary>
        /// Converts the value to an integer or default value, if conversion is not possible
        /// </summary>
        /// <param name="value">The string to convert to integer</param>
        /// <returns>The value</returns>
        public static int ToInt(this string value)
        {
            if (int.TryParse(value, out var intValue))
            {
                return intValue;
            }

            return default(int);
        }

        /// <summary>
        /// Converts the value to an integer or defaultValue, if conversion is not possible
        /// </summary>
        /// <param name="value">The string to convert to integer</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The value</returns>
        public static int ToInt(this string value, int defaultValue)
        {
            if (int.TryParse(value, out var intValue))
            {
                return intValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Converts the value to an integer or defaultValue, if conversion is not possible. The value must be at least minValue, or minValue is returned
        /// </summary>
        /// <param name="value">The string to convert to integer</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="minValue">The minimum allowed value</param>
        /// <returns>The value</returns>
        public static int ToInt(this string value, int defaultValue, int minValue)
        {
            return value.ToIntOrDefault(defaultValue).Max(minValue);
        }

        /// <summary>
        /// Converts the value to an integer or defaultValue, if conversion is not possible.
        /// The value must be at least minValue or minValue is returned.
        /// The value must be at most maxValue or maxValue is returned. 
        /// </summary>
        /// <param name="value">The string to convert to integer</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="minValue">The minimum allowed value</param>
        /// <param name="maxValue">The maximum allowed value</param>
        /// <returns>The value</returns>
        public static int ToInt(this string value, int defaultValue, int minValue, int maxValue)
        {
            return value.ToIntOrDefault(defaultValue).AtMost(maxValue).AtLeast(minValue);
        }

        /// <summary>
        /// Converts the string to a time span, or default if conversion is not possible
        /// </summary>
        /// <param name="timeSpanText">The text to convert to timespan</param>
        /// <returns>The timespan or default value</returns>
        public static TimeSpan ToTimeSpan(this string timeSpanText)
        {
            if (TimeSpan.TryParse(timeSpanText, out var timeSpanValue))
            {
                return timeSpanValue;
            }

            return default(TimeSpan);
        }

        /// <summary>
        /// Converts the string to a time span, or default if conversion is not possible. Value is adjusted to be between minValue and maxValue.
        /// For example, if min=10 sec, max=40 sec and value = 400 sec, 40 sec is returned.
        /// </summary>
        /// <param name="timeSpanText">The text to convert to timespan</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <returns>The timespan or default value</returns>
        public static TimeSpan ToTimeSpan(this string timeSpanText, TimeSpan defaultValue, TimeSpan minValue, TimeSpan maxValue)
        {
            return timeSpanText.ToTimeSpanOrDefault(defaultValue).Max(minValue).Min(maxValue);
        }

        /// <summary>
        /// Converts the string to a time span, or default if conversion is not possible. Value is adjusted to be at least minValue.
        /// For example, if min=10 sec, and value = 1 sec, 10 sec is returned.
        /// </summary>
        /// <param name="timeSpanText">The text to convert to timespan</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="minValue">The minimum value</param>
        /// <returns>The timespan or default value</returns>
        public static TimeSpan ToTimeSpan(this string timeSpanText, TimeSpan defaultValue, TimeSpan minValue)
        {
            return timeSpanText.ToTimeSpanOrDefault(defaultValue).Max(minValue);
        }


        /// <summary>
        /// Converts the string to a time span, or default if conversion is not possible.
        /// </summary>
        /// <param name="timeSpanText">The text to convert to timespan</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The timespan or default value</returns>
        public static TimeSpan ToTimeSpan(this string timeSpanText, TimeSpan defaultValue)
        {
            if (TimeSpan.TryParse(timeSpanText, out var timeSpanValue))
            {
                return timeSpanValue;
            }

            return defaultValue;
        }
    }
}