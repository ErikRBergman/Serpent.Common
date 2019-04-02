namespace Serpent.Common.BaseTypeExtensions
{
    using System;

    public static class IntExtensions
    {
        /// <summary>
        ///     Returns the other value, when value is default. Otherwise, returns value
        /// </summary>
        /// <param name="value">The value to check if its default</param>
        /// <param name="otherValue">The replacement value</param>
        /// <returns>The value</returns>
        public static int IfDefault(this int value, int otherValue)
        {
            if (value == default(int))
            {
                return otherValue;
            }

            return value;
        }

        /// <summary>
        ///     Returns the largest value - if value is less than otherValue, otherValue is returned
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="otherValue">The other value</param>
        /// <returns>The smallest value</returns>
        public static int Max(this int value, int otherValue)
        {
            return Math.Max(value, otherValue);
        }

        /// <summary>
        ///     When the value is less than minValue, minValue is returned. Otherwise value is returned.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="minValue">The minimum allowed value</param>
        /// <returns>The smallest value</returns>
        public static int AtLeast(this int value, int minValue)
        {
            return Math.Max(value, minValue);
        }

        /// <summary>
        ///     Returns the smallest value
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="otherValue">The other value</param>
        /// <returns>The smallest value</returns>
        public static int Min(this int value, int otherValue)
        {
            return Math.Min(value, otherValue);
        }


        /// <summary>
        ///     When the value is greater than maxValue, maxValue is returned. Otherwise value is returned.
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="maxValue">The maximum allowed value</param>
        /// <returns>The smallest value</returns>
        public static int AtMost(this int value, int maxValue)
        {
            return Math.Min(value, maxValue);
        }


        /// <summary>
        ///     Returns a value within a range. When value is less than minValue, minValue is returned. When value is greater than
        ///     maxValue, maxValue is returned.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="minValue">The minimum value</param>
        /// <param name="maxValue">The maximum value</param>
        /// <returns>The value</returns>
        public static int ToWithinRange(this int value, int minValue, int maxValue)
        {
            return Math.Max(Math.Min(value, minValue), maxValue);
        }
    }
}