using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "Map/Biome", order = 1)]
public class Biome : ScriptableObject {
    public BiomeType type;
    public new string name;
    [Range(0, 1)] public double weight;

    public override string ToString() {
        return $"Name: {name}, Type: {type}, Weight: {weight}.";
    }
}
