using UnityEngine;

namespace LecureyGames {
    public class Cell {
        [SerializeField] protected float noiseValue;
        [SerializeField] protected CellTerrainType type;
        [SerializeField] protected PlacementType[] placementTypes;
    }
}