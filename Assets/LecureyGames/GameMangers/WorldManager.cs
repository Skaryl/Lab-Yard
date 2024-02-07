using LecureyGames.Utilities;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LecureyGames {
    public class WorldManager : MonoBehaviour {
        public static WorldManager Instance { get; private set; }

        public bool mapPlaneVisible;

        private LocationManager LocationManager { get => locationManager; set => locationManager = value; }
        [SerializeField] private LocationManager locationManager;

        public GameObject World { get; private set; } = null;
        [SerializeField] private Material mapBackground;
        public Transform mapPrefab;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
                return;
            }
            Instance = this;

            if (LocationManager == null)
                LocationManager = GameObject.Find("LocationManager").GetComponent<LocationManager>();
            //ValidationHelper.CheckNull(LocationManager);

            InitializeWorld();
        }

        private void InitializeWorld() {
            World = new("World");
            World.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            Statistics.MeasureTime(GenerateMap);

            LocationManager.InitializeLocations(this.transform);

        }

        private void GenerateMap() {
            Debug.Log("Generating Map...");
            GameObject mapPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            mapPlane.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
            mapPlane.transform.localScale = new Vector3(100, 1, 100);
            mapPlane.name = "Background";
            mapPlane.GetComponent<Renderer>().material = mapBackground;
            mapPlane.transform.SetParent(World.transform);
            mapPlane.SetActive(mapPlaneVisible);
        }
    }
}