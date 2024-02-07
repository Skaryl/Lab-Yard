using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField]

    protected virtual void Awake() {

    }

    public virtual void GenerateMap() {


    }

    protected virtual double PerlinNoise(int x, int z, int seed) {
        return Mathf.PerlinNoise(seed + x * 0.1f, seed + z * 0.1f);
    }

    protected virtual Biome DetermineBiomeType(double noiseValue, HashSet<Biome> biomeList) {
        if (biomeList.Count == 0) {
            Debug.LogError("Biome list is empty");
            return null;
        }
        double[] thresholds = GetNormalizedThresholds(GetThresholds(biomeList));
        for (int i = 0; i < biomeList.Count; i++) {
            if (noiseValue < thresholds[i]) {
                return biomeList.ElementAt(i);
            }
        }
        return biomeList.ElementAt(biomeList.Count - 1);
    }

    protected virtual double[] GetThresholds(HashSet<Biome> biomeList) {
        if (biomeList.Count == 0) {
            Debug.LogError("Biome list is empty");
            return null;
        }
        double[] thresholds = new double[biomeList.Count];
        thresholds[0] = biomeList.ElementAt(0).weight;
        if (biomeList.Count == 1) {
            thresholds[1] = 1;
            return thresholds;
        }

        for (int i = 1; i < biomeList.Count; i++) {
            thresholds[i] = thresholds[i - 1] + biomeList.ElementAt(i).weight;
        }
        return thresholds;
    }

    protected virtual double[] GetNormalizedThresholds(double[] thresholds) {
        double minValue = thresholds[0];
        double maxValue = thresholds[thresholds.Length - 1];

        double range = maxValue - minValue;

        for (int i = 0; i < thresholds.Length; i++) {
            thresholds[i] = (thresholds[i] - minValue) / range;
        }

        return thresholds;
    }
}

