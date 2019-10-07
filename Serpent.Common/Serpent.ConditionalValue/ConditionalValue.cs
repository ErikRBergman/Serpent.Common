using System;

namespace Serpent.ConditionalValue
{
    /// <summary>
    /// Provides a way to return a conditional value (most often in async methods, for example <code>Task&lt;ConditionalValue&lt;string&gt;&gt;</code>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct ConditionalValue<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConditionalValue{T}"/>
        /// </summary>
        /// <param name="value"></param>
        public ConditionalValue(T value)
        {
            this.Value = value;
            this.HasValue = true;
        }

        /// <summary>
        /// Returns an instance with no value
        /// </summary>
        public static ConditionalValue<T> NoValue { get; } = new ConditionalValue<T>();

        /// <summary>
        /// Returns an instance containing the specified value, or if the value is the types default value (or null) the default value is returned
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="defaultValue">The default value used if value is null or equals the types default value</param>
        /// <returns>A <see cref="ConditionalValue{T}"/></returns>
        public static ConditionalValue<T> ValueOrDefault(T value, T defaultValue = default(T))
        {
            if (value == null || value.Equals(default(T)))
            {
                return defaultValue;
            }
            
            return new ConditionalValue<T>();
        }

        
        /// <summary>
        /// Returns an instance containing the specified value, or if the value is the types default value (or null) the default value is returned
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>A <see cref="ConditionalValue{T}"/></returns>
        public static ConditionalValue<T> ValueOrDefaultNoValue(T value)
        {
            if (value == null || value.Equals(default(T)))
            {
                return NoValue;
            }
            
            return new ConditionalValue<T>();
        }

        /// <summary>
        /// Creates and returns an instance with the specified value
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>A <see cref="ConditionalValue{T}"/></returns>
        public static ConditionalValue<T> FromValue(T value)
        {
            return new ConditionalValue<T>(value);
        }

        /// <summary>
        /// Gets the value
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has a value or not
        /// </summary>
        public bool HasValue { get; }

        /// <summary>
        /// Converts a <see cref="ConditionalValue{T}"/> to <see cref="T"/>
        /// </summary>
        /// <param name="v">The conditional value</param>
        public static implicit operator T(ConditionalValue<T> v)
        {
            return v.Value;
        }
        
        /// <summary>
        /// Converts a <see cref="T"/> to <see cref="ConditionalValue{T}"/>
        /// </summary>
        /// <param name="v">The conditional value</param>
        public static implicit operator ConditionalValue<T>(T v)
        {
            return new ConditionalValue<T>(v);
        }

        /// <summary>
        /// Returns the value as a string, or 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Value != null)
            {
                return this.Value.ToString();
            }

            return "{HasValue=false}";
        }
    }
}
