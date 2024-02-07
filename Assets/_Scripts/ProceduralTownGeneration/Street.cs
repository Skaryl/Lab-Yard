using System.Collections.Generic;
using UnityEngine;

public class Street {
    protected Vector3Int startPosition;
    protected Vector3Int endPosition;
    protected string streetName;
    protected HashSet<int> evenHouseNumbers;
    protected HashSet<int> oddHouseNumbers;
    protected Dictionary<int, Vector3Int> houseNumbersToPosition;

    public Vector3Int StartPosition => startPosition;
    public Vector3Int EndPosition => endPosition;
    public string StreetName => streetName;
    public HashSet<int> EvenHouseNumbers => evenHouseNumbers;
    public HashSet<int> OddHouseNumbers => oddHouseNumbers;
    public Dictionary<int, Vector3Int> HouseNumbersToPosition => houseNumbersToPosition;
}