using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapTile {
    protected MapTileType type;
    protected GameObject tile;
    protected Vector3Int position;
    protected float rotation;
    protected Dictionary<Direction, MapTile> neighbours;

    public MapTile() {
        neighbours = new();
        rotation = 0f;
    }

    public MapTile(Vector3Int position) : this() {
        this.position = position;
    }

    public Vector3Int Position => position;
    public float Rotation => rotation;
    public MapTileType Type => type;
    public GameObject Tile => tile;
    public Dictionary<Direction, MapTile> Neighbours => neighbours;

    public void AddNeighbour(Direction direction, MapTile roadTile) {
        if (!neighbours.ContainsKey(direction))
            neighbours.Add(direction, roadTile);
    }

    public bool CheckForStraight() {
        return (neighbours.Count == 2 && ((neighbours.ContainsKey(Direction.up) && neighbours.ContainsKey(Direction.down)) || ((neighbours.ContainsKey(Direction.left) && neighbours.ContainsKey(Direction.right)))));
    }

    public void RemoveNeighbour(Direction direction) {
        neighbours.Remove(direction);
    }

    public void SetType(MapTileType type) {
        this.type = type;
        tile = PlacementHelper.GetTileFromType(type);
    }

    public void SetRotation(float rotation) {
        this.rotation = rotation;
    }

    public void SetRotation0() {
        rotation = 0f;
    }

    public void SetRotation90() {
        rotation = 90f;
    }

    public void SetRotation180() {
        rotation = 180f;
    }

    public void SetRotation270() {
        rotation = 270f;
    }

    public Dictionary<Direction, GameObject> NeighbourTiles() {
        Dictionary<Direction, GameObject> neighbourTiles = new();
        foreach (var neighbour in neighbours) {
            neighbourTiles.Add(neighbour.Key, neighbour.Value.Tile);
        }
        return neighbourTiles;
    }

    public override string ToString() {
        return $"Position: {position}, Type: {type}, Rotation: {rotation}";
    }
}