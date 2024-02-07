using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public GameObject[] treePrefabs;
    public Material terrainMaterial;
    public Material edgeMaterial;
    public float waterLevel = .4f;
    public float scale = .1f;
    public float treeNoiseScale = .05f;
    public float treeDensity = .5f;
    public int size = 100;

    public Gradient ColorGradient { get; set; }


    Cell[,] grid;

    void Start() {
        float[,] noiseMap = new float[size, size];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        float[,] falloffMap = new float[size, size];
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                float xv = x / (float)size * 2 - 1;
                float yv = y / (float)size * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                falloffMap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }

        grid = new Cell[size, size];
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                float noiseValue = noiseMap[x, y];
                noiseValue -= falloffMap[x, y];
                bool isWater = noiseValue < waterLevel;
                Cell cell = new(isWater);
                grid[x, y] = cell;
            }
        }

        DrawTerrainMesh(grid);
        DrawEdgeMesh(grid);
        DrawTexture(grid);
        GenerateTrees(grid);
    }

    void DrawTerrainMesh(Cell[,] grid) {
        Mesh mesh = new();
        List<Vector3> vertices = new();
        List<int> triangles = new();
        List<Vector2> uvs = new();
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                Cell cell = grid[x, y];
                if (!cell.IsWater) {
                    Vector3 a = new(x - .5f, 0, y + .5f);
                    Vector3 b = new(x + .5f, 0, y + .5f);
                    Vector3 c = new(x - .5f, 0, y - .5f);
                    Vector3 d = new(x + .5f, 0, y - .5f);
                    Vector2 uvA = new(x / (float)size, y / (float)size);
                    Vector2 uvB = new((x + 1) / (float)size, y / (float)size);
                    Vector2 uvC = new(x / (float)size, (y + 1) / (float)size);
                    Vector2 uvD = new((x + 1) / (float)size, (y + 1) / (float)size);
                    Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                    Vector2[] uv = new Vector2[] { uvA, uvB, uvC, uvB, uvD, uvC };
                    for (int k = 0; k < 6; k++) {
                        vertices.Add(v[k]);
                        triangles.Add(triangles.Count);
                        uvs.Add(uv[k]);
                    }
                }
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        //MeshRenderer meshRenderer = 
        gameObject.AddComponent<MeshRenderer>();
    }

    void DrawEdgeMesh(Cell[,] grid) {
        Mesh mesh = new();
        List<Vector3> vertices = new();
        List<int> triangles = new();
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                Cell cell = grid[x, y];
                if (!cell.IsWater) {
                    if (x > 0) {
                        Cell left = grid[x - 1, y];
                        if (left.IsWater) {
                            Vector3 a = new(x - .5f, 0, y + .5f);
                            Vector3 b = new(x - .5f, 0, y - .5f);
                            Vector3 c = new(x - .5f, -1, y + .5f);
                            Vector3 d = new(x - .5f, -1, y - .5f);
                            Vector3[] v = new[] { a, b, c, b, d, c };
                            for (int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                    if (x < size - 1) {
                        Cell right = grid[x + 1, y];
                        if (right.IsWater) {
                            Vector3 a = new(x + .5f, 0, y - .5f);
                            Vector3 b = new(x + .5f, 0, y + .5f);
                            Vector3 c = new(x + .5f, -1, y - .5f);
                            Vector3 d = new(x + .5f, -1, y + .5f);
                            Vector3[] v = new[] { a, b, c, b, d, c };
                            for (int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                    if (y > 0) {
                        Cell down = grid[x, y - 1];
                        if (down.IsWater) {
                            Vector3 a = new(x - .5f, 0, y - .5f);
                            Vector3 b = new(x + .5f, 0, y - .5f);
                            Vector3 c = new(x - .5f, -1, y - .5f);
                            Vector3 d = new(x + .5f, -1, y - .5f);
                            Vector3[] v = new[] { a, b, c, b, d, c };
                            for (int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                    if (y < size - 1) {
                        Cell up = grid[x, y + 1];
                        if (up.IsWater) {
                            Vector3 a = new(x + .5f, 0, y + .5f);
                            Vector3 b = new(x - .5f, 0, y + .5f);
                            Vector3 c = new(x + .5f, -1, y + .5f);
                            Vector3 d = new(x - .5f, -1, y + .5f);
                            Vector3[] v = new[] { a, b, c, b, d, c };
                            for (int k = 0; k < 6; k++) {
                                vertices.Add(v[k]);
                                triangles.Add(triangles.Count);
                            }
                        }
                    }
                }
            }
        }
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        GameObject edgeObj = new("Edge");
        edgeObj.transform.SetParent(transform);

        MeshFilter meshFilter = edgeObj.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = edgeObj.AddComponent<MeshRenderer>();
        meshRenderer.material = edgeMaterial;
    }

    void DrawTexture(Cell[,] grid) {
        Texture2D texture = new(size, size);
        Color32[] colorMap = new Color32[size * size];
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                Cell cell = grid[x, y];
                if (cell.IsWater)
                    colorMap[y * size + x] = Color.blue;
                else
                    colorMap[y * size + x] = Color.green;
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.SetPixels32(colorMap);
        texture.Apply();

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = terrainMaterial;
        meshRenderer.material.mainTexture = texture;
    }

    void GenerateTrees(Cell[,] grid) {
        float[,] noiseMap = new float[size, size];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                float noiseValue = Mathf.PerlinNoise(x * treeNoiseScale + xOffset, y * treeNoiseScale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                Cell cell = grid[x, y];
                if (!cell.IsWater) {
                    float v = Random.Range(0f, treeDensity);
                    if (noiseMap[x, y] < v) {
                        GameObject prefab = treePrefabs[Random.Range(0, treePrefabs.Length)];
                        GameObject tree = Instantiate(prefab, transform);
                        tree.transform.SetPositionAndRotation(new Vector3(x, 0, y), Quaternion.Euler(0, Random.Range(0, 360f), 0));
                        tree.transform.localScale = Vector3.one * Random.Range(.8f, 1.2f);
                    }
                }
            }
        }
    }
}


//using System.Collections.Generic;
//using UnityEngine;

//public class Grid : MonoBehaviour {

//    [SerializeField] protected int size = 100;
//    [SerializeField] protected float scale = .1f;
//    [SerializeField] protected float waterLevel;
//    [SerializeField] protected int seed = 0;
//    [SerializeField] protected bool useRandomSeed = true;

//    [SerializeField] protected Material terrainMaterial;

//    protected SubGrid subGrid;

//    private void Awake() {
//        if (useRandomSeed) {
//            seed = Random.Range(-100000, 100000);
//        }

//        subGrid = new() {
//            Position = transform.position,
//            grid = new Cell[size, size]
//        };

//        float[,] noiseMap = new float[size, size];
//        for (int x = 0; x < size; x++) {
//            for (int z = 0; z < size; z++) {
//                noiseMap[x, z] = Mathf.PerlinNoise(seed + x * scale, seed + z * scale);
//            }
//        }

//        float[,] falloffMap = new float[size, size];
//        for (int x = 0; x < size; x++) {
//            for (int z = 0; z < size; z++) {
//                float xCoord = x / (float)size * 2 - 1;
//                float zCoord = z / (float)size * 2 - 1;
//                float value = Mathf.Max(Mathf.Abs(xCoord), Mathf.Abs(zCoord));
//                falloffMap[x, z] = Mathf.Pow(value, 3f) / (Mathf.Pow(value, 3f) + Mathf.Pow(2.2f - 2.2f * value, 3f));
//            }
//        }

//        for (int x = 0; x < size; x++) {
//            for (int z = 0; z < size; z++) {
//                Cell cell = new();
//                float noiseValue = noiseMap[x, z];
//                noiseValue -= falloffMap[x, z];
//                if (noiseValue < waterLevel)
//                    cell.Type = CellType.Water;
//                subGrid.grid[x, z] = cell;

//            }
//        }

//        DrawTerrainMesh(subGrid);
//        DrawTexture(subGrid);
//    }



//    protected void DrawTerrainMesh(SubGrid subGrid) {
//        Mesh mesh = new();
//        List<Vector3> vertices = new();
//        List<int> triangles = new();
//        for (int x = 0; x < size; x++) {
//            for (int z = 0; z < size; z++) {
//                if (!(subGrid.grid[x, z].Type == CellType.Water)) {
//                    Vector3 a = subGrid.Position + new Vector3(x - .5f, 0, z + .5f);
//                    Vector3 b = subGrid.Position + new Vector3(x + .5f, 0, z + .5f);
//                    Vector3 c = subGrid.Position + new Vector3(x - .5f, 0, z - .5f);
//                    Vector3 d = subGrid.Position + new Vector3(x + .5f, 0, z - .5f);
//                    Vector3[] verticesArray = new Vector3[] { a, b, c, c, b, d };
//                    for (int i = 0; i < verticesArray.Length; i++) {
//                        vertices.Add(verticesArray[i]);
//                        triangles.Add(triangles.Count);
//                    }
//                }
//            }
//        }
//        mesh.vertices = vertices.ToArray();
//        mesh.triangles = triangles.ToArray();
//        mesh.RecalculateNormals();

//        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
//        meshFilter.mesh = mesh;

//        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
//    }

//    protected void DrawTexture(SubGrid subGrid) {
//        Texture2D texture = new Texture2D(size, size);
//        Color[] colorMap = new Color[size * size];
//        for (int z = 0; z < size; z++) {
//            for (int x = 0; x < size; x++) {
//                Cell cell = subGrid.grid[x, z];
//                if (cell.Type == CellType.Water)
//                    colorMap[z * size + x] = Color.blue;
//                else
//                    colorMap[z * size + x] = Color.green;
//            }
//        }
//        texture.filterMode = FilterMode.Point;
//        texture.SetPixels(colorMap);
//        texture.Apply();

//        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
//        meshRenderer.material = terrainMaterial;
//        meshRenderer.material.mainTexture = texture;
//    }

//    public Cell GetCell(SubGrid subGrid, int x, int z) => subGrid.grid[x, z];

//}

//public class SubGrid {
//    public Vector3 Position { get; set; }
//    public Cell[,] grid;
//}
