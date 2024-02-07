using UnityEngine;

public class MapTiles : MonoBehaviour {
    public static MapTiles Instance { get; private set; }

    public GameObject
        baseTile,
        none,
        blank,
        roadEnd,
        roadStraight,
        roadCorner,
        road3way,
        road4way,
        tilePlacementError,
        tileNoNeighbours,
        tileTooManyNeighbours,
        tileExistentButNotChanged;

    public static GameObject None => MapTiles.Instance.none;
    public static GameObject Blank => MapTiles.Instance.blank;
    public static GameObject RoadEnd => MapTiles.Instance.roadEnd;
    public static GameObject RoadStraight => MapTiles.Instance.roadStraight;
    public static GameObject RoadCorner => MapTiles.Instance.roadCorner;
    public static GameObject Road3way => MapTiles.Instance.road3way;
    public static GameObject Road4way => MapTiles.Instance.road4way;
    public static GameObject TileTooManyNeighbours => MapTiles.Instance.tileTooManyNeighbours;
    public static GameObject TilePlacementError => MapTiles.Instance.tilePlacementError;
    public static GameObject TileNoNeighbours => MapTiles.Instance.tileNoNeighbours;
    public static GameObject TileExistentButNotChanged => MapTiles.Instance.tileExistentButNotChanged;


    private void Awake() {
        Instance = this;
    }
}
