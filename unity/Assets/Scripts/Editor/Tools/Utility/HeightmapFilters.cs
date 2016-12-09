using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Tools.Utility
{
    public class HeightmapFilters
    {
        private static readonly Type heightmapFiltersType = typeof(Editor).Assembly.GetType("UnityEditor.HeightmapFilters");
        private static readonly MethodInfo wobbleStuff = heightmapFiltersType.GetMethod("WobbleStuff", BindingFlags.Static);
        private static readonly MethodInfo noise = heightmapFiltersType.GetMethod("Noise", new Type[] { typeof(float[,]), typeof(TerrainData) });
        private static readonly MethodInfo smooth = heightmapFiltersType.GetMethod("Smooth", new Type[] { typeof(float[,]), typeof(TerrainData) });
        private static readonly MethodInfo smoothFull = heightmapFiltersType.GetMethod("Smooth", new Type[] { typeof(TerrainData) });
        private static readonly MethodInfo flatten = heightmapFiltersType.GetMethod("Flatten", new Type[] { typeof(TerrainData), typeof(float) });

        public static void WobbleStuff(float[,] heights, TerrainData terrain)
        {
            wobbleStuff.Invoke(null, new object[] { heights, terrain });
        }

        public static void Noise(float[,] heights, TerrainData terrain)
        {
            noise.Invoke(null, new object[] { heights, terrain });
        }

        public static void Smooth(float[,] heights, TerrainData terrain)
        {
            smooth.Invoke(null, new object[] { heights, terrain });
        }

        public static void Smooth(TerrainData terrain)
        {
            smoothFull.Invoke(null, new object[] { terrain });
        }

        public static void Flatten(TerrainData terrain, float height)
        {
            flatten.Invoke(null, new object[] { terrain, height });
        }
    }
}
