namespace Serpent.Common.BaseTypeExtensions.Collections
{
    public static class ArrayExtensions
    {
        public static T[] Swap<T>(this T[] array, int firstIndex, int secondIndex)
        {
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
            return array;
        }
    }
}