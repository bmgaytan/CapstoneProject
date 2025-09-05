using UnityEngine;

public static class NoiseMap
{
    public static float[,] GenerateNoiseMap(int mapX, int mapY, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapX, mapY];

        System.Random pseudo = new System.Random(seed);
        Vector2[] offsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = pseudo.Next(-100000, 100000) + offset.x;
            float offsetY = pseudo.Next(-100000, 100000) + offset.y;
            offsets[i] = new Vector2(offsetX, offsetY);
        }


        if (scale <= 0)
        {
            scale = 0.01f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapX / 2f;
        float halfHeight = mapY / 2f;

        for (int y = 0; y < mapY; y++)
        {
            for (int x = 0; x < mapX; x++)
            {
                float amplitude = 1f;
                float frequency = 1f;
                float noiseHeight = 0f;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x-halfWidth) / scale * frequency + offsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + offsets[i].y;

                    float perlinVal = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinVal * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapY; y++)
        {
            for (int x = 0; x < mapX; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}
