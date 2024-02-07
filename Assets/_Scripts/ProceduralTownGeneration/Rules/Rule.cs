using UnityEngine;

[CreateAssetMenu(menuName = "ProceduralTown/Rule")]
public class Rule : ScriptableObject {
    public string letter;
    [SerializeField] private string[] results = null;
    [SerializeField] private bool randomResult = false;

    public string GetResult() {
        if (randomResult)
            return results[Random.Range(0, results.Length)];

        return results[0];
    }
}
