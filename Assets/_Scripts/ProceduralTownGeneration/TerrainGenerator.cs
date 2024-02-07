using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public Transform terrainPrefab;

    public int width = 256;
    public int height = 256;
    public float scale = 20f;
    public float terrainHeightMultiplier = 10f;

    void Start() {
        GenerateTerrain();
    }

    void GenerateTerrain() {
        GameObject terrainObject = new("ProceduralTerrain");
        Terrain terrain = terrainObject.AddComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData) {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new(width, terrainHeightMultiplier, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights() {
        float[,] heights = new float[width, height];
        Vector2 offset = new(Random.Range(0f, 9999f), Random.Range(0f, 9999f));

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                heights[x, y] = Mathf.PerlinNoise((x + offset.x) / scale, (y + offset.y) / scale);
            }
        }

        return heights;
    }
}