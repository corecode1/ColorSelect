using UnityEngine;

namespace com.ColorSelect
{
    public static class Utils
    {
        public static void Shuffle<T>(this T[] arr)
        {
            for (int index = 0; index < arr.Length; index++)
            {
                T tmp = arr[index];
                int newIndex = Random.Range(index, arr.Length);
                arr[index] = arr[newIndex];
                arr[newIndex] = tmp;
            }
        }
    }
}