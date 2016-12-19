using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyGame.Extensions
{
    public static class ArrayExtensions
    {
        public static void Initialize<T>(this T[] array, T defaultValue) where T : new()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = defaultValue;
            }
        }
    }
}
