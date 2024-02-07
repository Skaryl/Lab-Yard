using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlacementHelper {
    public static HashSet<Direction> FindNeighbours(Vector3Int position, ICollection<Vector3Int> collection) {
        HashSet<Direction> neighbourDirections = new();
        if (collection.Contains(position + new Vector3Int(1, 0, 0))) {
            neighbourDirections.Add(Direction.right);
        }
        if (collection.Contains(position + new Vector3Int(-1, 0, 0))) {
            neighbourDirections.Add(Direction.left);
        }
        if (collection.Contains(position + new Vector3Int(0, 0, 1))) {
            neighbourDirections.Add(Direction.up);
        }
        if (collection.Contains(position + new Vector3Int(0, 0, -1))) {
            neighbourDirections.Add(Direction.down);
        }
        return neighbourDirections;
    }

    internal static Vector3Int GetVectorFromDirection(Direction direction) {
        return direction switch {
            Direction.up => new Vector3Int(0, 0, 1),
            Direction.down => new Vector3Int(0, 0, -1),
            Direction.left => new Vector3Int(-1, 0, 0),
            Direction.right => new Vector3Int(1, 0, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };
    }

    internal static Direction GetOppositeDirection(Direction direction) {
        return direction switch {
            Direction.up => Direction.down,
            Direction.down => Direction.up,
            Direction.left => Direction.right,
            Direction.right => Direction.left,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };
    }

    internal static GameObject GetTileFromType(MapTileType type) {
        return type switch {
            MapTileType.none => MapTiles.None,
            MapTileType.blank => MapTiles.Blank,
            MapTileType.roadEnd => MapTiles.RoadEnd,
            MapTileType.roadStraight => MapTiles.RoadStraight,
            MapTileType.roadCorner => MapTiles.RoadCorner,
            MapTileType.road3way => MapTiles.Road3way,
            MapTileType.road4way => MapTiles.Road4way,
            MapTileType.tileTooManyNeighbours => MapTiles.TileTooManyNeighbours,
            MapTileType.tilePlacementError => MapTiles.TilePlacementError,
            MapTileType.tileNoNeighbours => MapTiles.TileNoNeighbours,
            MapTileType.tileExistentButNotChanged => MapTiles.TileExistentButNotChanged,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }
}