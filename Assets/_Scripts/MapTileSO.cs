using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/MapTile")]
public class MapTileSO : ScriptableObject {
    public Transform[] tilePrefab;
    public MapTileType tileType;
    public Quaternion rotation;
    public string tileName;
    public RotationIdentifier rotationIdentifier;
    public Direction[] identifierDirections;
}

[Serializable]
public enum RotationIdentifier {
    existent,
    missing,
}