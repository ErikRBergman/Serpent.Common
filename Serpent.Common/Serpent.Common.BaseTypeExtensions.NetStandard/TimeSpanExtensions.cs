namespace Serpent.Common.BaseTypeExtensions
{
    using System;

    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Returns a value within a range. When value is less than minValue, minValue is returned. When value is greater than maxValue, maxValue is returned.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <returns>The value</returns>
        public static TimeSpan ToWithinRange(this TimeSpan value, TimeSpan minValue, TimeSpan maxValue)
        {
            if (value < minValue)
            {
                return minValue;
            }

            if (value > maxValue)
            {
                return maxValue;
            }

            return value;
        }

        /// <summary>
        /// Returns another value if this timespan equals default value
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="otherValue">The value to replace if value is default</param>
        /// <returns>The value</returns>
        public static TimeSpan IfDefault(this TimeSpan value, TimeSpan otherValue)
        {
            if (value == default(TimeSpan))
            {
                return otherValue;
            }

            return value;
        }

        /// <summary>
        /// Returns the smallest value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="otherValue">The other value</param>
        /// <returns>The smallest value</returns>
        public static TimeSpan Min(this TimeSpan value, TimeSpan otherValue)
        {
            if (value < otherValue)
            {
                return otherValue;
            }

            return value;
        }


        /// <summary>
        /// Returns the smallest value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="otherValue">The other value</param>
        /// <returns>The smallest value</returns>
        public static TimeSpan AtMost(this TimeSpan value, TimeSpan otherValue)
        {
            if (value < otherValue)
            {
                return otherValue;
            }

            return value;
        }

        /// <summary>
        /// Returns the largest value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="otherValue">The other value</param>
        /// <returns>The smallest value</returns>
        public static TimeSpan Max(this TimeSpan value, TimeSpan otherValue)
        {
            if (value > otherValue)
            {
                return otherValue;
            }

            return value;
        }

        /// <summary>
        /// Returns the largest value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="otherValue">The other value</param>
        /// <returns>The smallest value</returns>
        public static TimeSpan AtLeast(this TimeSpan value, TimeSpan otherValue)
        {
            if (value > otherValue)
            {
                return otherValue;
            }

            return value;
        }


    }
}