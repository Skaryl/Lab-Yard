using System.Collections.Generic;
using UnityEngine;

public class RoadHelper : MonoBehaviour {
    private Dictionary<Vector3Int, MapTile> roadDictionary;
    private HashSet<Vector3Int> roadPositions;
    private HashSet<Vector3Int> roadDoubles;

    private Dictionary<Vector3Int, int> northSouthStreets, westEastStreets;



    private void Awake() {
        Initializing();
    }

    public int RoadCount => roadPositions.Count;


    public int RoadDoublesCount => roadDoubles.Count;


    public void Initializing() {
        roadDictionary = new();
        roadPositions = new();
        roadDoubles = new();
        northSouthStreets = new();
        westEastStreets = new();
    }

    public void AddStreetPositions(Vector3 startPosition, Vector3Int direction, int length) {
        int addedPositionCount = 0;
        int addedDoublesCount = 0;
        for (int i = 0; i < length; i++) {
            var position = Vector3Int.RoundToInt(startPosition + direction * i);
            if (roadPositions.Contains(position)) {
                roadDoubles.Add(position);
                addedDoublesCount++;
                continue;
            }
            roadPositions.Add(position);
            addedPositionCount++;
        }
    }

    public void FillRoadDictionary() {
        Debug.Log("Filling road dictionary");
        foreach (var position in roadPositions) {
            MapTile roadTile;

            roadTile = new(position);
            roadDictionary.Add(position, roadTile);

            RegisterWithExistingNeighbours(roadTile);
        }
        foreach (var roadTile in roadDictionary.Values) {
            SetNeededTile(roadTile);
            SetNeededRotation(roadTile);
        }

        Debug.Log("Done filling road dictionary");
    }

    private void RegisterWithExistingNeighbours(MapTile mapTile) {
        HashSet<Direction> neighbourDirections = PlacementHelper.FindNeighbours(mapTile.Position, roadDictionary.Keys);
        foreach (Direction direction in neighbourDirections) {
            MapTile neighbourRoadTile = roadDictionary[mapTile.Position + PlacementHelper.GetVectorFromDirection(direction)];
            mapTile.AddNeighbour(direction, neighbourRoadTile);
            neighbourRoadTile.AddNeighbour(PlacementHelper.GetOppositeDirection(direction), mapTile);
        }
    }

    private void SetNeededTile(MapTile mapTile) {
        mapTile.SetType(MapTileType.tilePlacementError);

        if (roadPositions.Contains(mapTile.Position)) {
            mapTile.SetType(MapTileType.tileExistentButNotChanged);
        }


        HashSet<Direction> neighbourDirections = PlacementHelper.FindNeighbours(mapTile.Position, roadPositions);

        switch (mapTile.Neighbours.Count) {
            case 0:
                Debug.LogError($"No neighbours: {mapTile.Position} -> {neighbourDirections.Count}");
                mapTile.SetType(MapTileType.tileNoNeighbours);
                break;
            case 1:
                mapTile.SetType(MapTileType.roadEnd);
                break;
            case 2:
                if (mapTile.CheckForStraight()) {
                    mapTile.SetType(MapTileType.roadStraight);
                    break;
                }
                mapTile.SetType(MapTileType.roadCorner);
                break;
            case 3:
                mapTile.SetType(MapTileType.road3way);
                break;
            case 4:
                mapTile.SetType(MapTileType.road4way);
                break;
            default:
                Debug.Log($"Too many neighbours: {mapTile.Position} -> {neighbourDirections.Count}");
                mapTile.SetType(MapTileType.tileTooManyNeighbours);
                break;
        }
    }

    private void SetNeededRotation(MapTile mapTile) {
        switch (mapTile.Type) {
            case MapTileType.roadEnd:
                if (mapTile.Neighbours.ContainsKey(Direction.right)) { mapTile.SetRotation180(); break; }
                if (mapTile.Neighbours.ContainsKey(Direction.down)) { mapTile.SetRotation270(); break; }
                if (mapTile.Neighbours.ContainsKey(Direction.left)) { mapTile.SetRotation0(); break; }
                if (mapTile.Neighbours.ContainsKey(Direction.up)) { mapTile.SetRotation90(); break; }
                Debug.LogError($"Road end with no defined orientation: {mapTile.Position}");
                break;
            case MapTileType.roadStraight:
                if (mapTile.Neighbours.ContainsKey(Direction.up) && mapTile.Neighbours.ContainsKey(Direction.down)) { mapTile.SetRotation90(); break; }
                if (mapTile.Neighbours.ContainsKey(Direction.right) && mapTile.Neighbours.ContainsKey(Direction.left)) { mapTile.SetRotation0(); break; }
                Debug.LogError($"Road straight with no defined orientation: {mapTile.Position}");
                break;
            case MapTileType.roadCorner:
                if (mapTile.Neighbours.ContainsKey(Direction.up) && mapTile.Neighbours.ContainsKey(Direction.right)) { mapTile.SetRotation0(); break; }
                if (mapTile.Neighbours.ContainsKey(Direction.right) && mapTile.Neighbours.ContainsKey(Direction.down)) { mapTile.SetRotation90(); break; }
                if (mapTile.Neighbours.ContainsKey(Direction.down) && mapTile.Neighbours.ContainsKey(Direction.left)) { mapTile.SetRotation180(); break; }
                if (mapTile.Neighbours.ContainsKey(Direction.left) && mapTile.Neighbours.ContainsKey(Direction.up)) { mapTile.SetRotation270(); break; }
                Debug.LogError($"Road corner with no defined orientation: {mapTile.Position}");
                break;
            case MapTileType.road3way:
                if (!mapTile.Neighbours.ContainsKey(Direction.down)) { mapTile.SetRotation0(); break; }
                if (!mapTile.Neighbours.ContainsKey(Direction.left)) { mapTile.SetRotation90(); break; }
                if (!mapTile.Neighbours.ContainsKey(Direction.up)) { mapTile.SetRotation180(); break; }
                if (!mapTile.Neighbours.ContainsKey(Direction.right)) { mapTile.SetRotation270(); break; }
                Debug.LogError($"Road 3way with no defined orientation: {mapTile.Position}");
                break;
            case MapTileType.road4way:
                break;
            default:
                Debug.Log($"Rotation for type {mapTile.Type} not defined");
                break;
        }
    }

    public void InstatiateRoadTiles() {
        Debug.Log("Instantiating road tiles.");
        foreach (var road in roadDictionary) {
            GameObject roadTile = Instantiate(road.Value.Tile, road.Key, Quaternion.Euler(0, road.Value.Rotation, 0), transform);
            roadTile.name = $"{road.Value.Type}: {road.Key}";
        }
        Debug.Log("Done instantiating road tiles.");
    }

    public void VisualizeRoadPositions() {
        FillRoadDictionary();
        InstatiateRoadTiles();
    }

    public void AdjustRoadMap(int minimalSpacingBetweenParallelStreets, int sizeOfAllowedChunks) {
        //AvoidNearParallelLines(minimalSpacingBetweenParallelStreets);
        //GetRidOfChunks(sizeOfAllowedChunks);
        //OptimizeRoadPositions();
    }

    protected void AvoidNearParallelLines(int minimalSpacingBetweenParallelStreets) {

    }

    protected void GetRidOfChunks(int sizeOfAllowedChunks) {

    }

    protected void OptimizeRoadPositions() {
    }
}

