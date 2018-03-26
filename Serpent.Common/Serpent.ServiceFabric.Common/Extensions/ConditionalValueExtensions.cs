namespace Serpent.ServiceFabric.Common.Extensions
{
    using Microsoft.ServiceFabric.Data;

    public static class ConditionalValueExtensions
    {
        public static TValue GetValueOrDefault<TValue>(this ConditionalValue<TValue> conditionalValue, TValue defaultValue)
        {
            return conditionalValue.HasValue ? conditionalValue.Value : defaultValue;
        }
    }
}