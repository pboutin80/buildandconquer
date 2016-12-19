using UnityEngine;

namespace Tools.Utility
{
    public class TerrainUtility
    {

        public static void SetHeightMapFromTexture(TerrainData aTerrainData, Texture2D aTexture, float aAmplitude = 1F)
        {
            var width = aTexture.width;
            var height = aTexture.height;

            float[,] heights = new float[width,height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var mapHeight = aTexture.GetPixel(x, y).grayscale;

                    heights[x, y] = mapHeight * aAmplitude;
                }
            }

            aTerrainData.SetHeights(0, 0, heights);
        }

    }
}
