using UnityEngine;

namespace LecureyGames {
    public class NoiseMapGenerator {
        public float[,] GenerateNoiseMap(int width, int height, float scale, float offsetX, float offsetY) {

            float[,] noiseMap = new float[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    float sampleX = (float)x / width * scale + offsetX;
                    float sampleY = (float)y / height * scale + offsetY;
                    noiseMap[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
                }
            }

            return null;
        }
    }
}