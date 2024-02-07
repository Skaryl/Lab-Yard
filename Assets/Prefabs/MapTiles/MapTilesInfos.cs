using System.Collections.Generic;
using UnityEngine;

public class MapTilesInfos : MonoBehaviour {
    public MapTile mapTile_North, mapTile_South, mapTile_East, mapTile_west;
    public float walkSpeed = 1.0f;
    public float runSpeed = 2.0f;
    public float bikeSpeed = 5.0f;
    public float driveSpeed = 15.0f;
    public MapTile mapTile;

    public MapTileSO mapTileSO;
    public Dictionary<Direction, GameObject> Neighbours => mapTile.NeighbourTiles();
}