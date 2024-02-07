using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public static Map Instance { get; private set; }
    [SerializeField] protected bool useDefaultMapGenerator = true;
    [SerializeField] protected MapGenerator[] mapGenerators;
    protected MapGenerator currentMapGenerator;

    [SerializeField] protected int numberOfTowns = 10;

    protected HashSet<Street> streets;


    protected virtual void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        InitializeFields();

    }

    protected virtual void InitializeFields() {
        streets = new HashSet<Street>();
    }

    protected virtual void Start() {

    }

    // Update is called once per frame
    protected virtual void Update() {

    }
}
