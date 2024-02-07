using LecureyGames.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    public class LocationManager : MonoBehaviour {
        public static LocationManager Instance { get; private set; }

        [SerializeField] private bool useRandomLocations = true;
        [SerializeField, PositiveInt] private int minNumberOfLocations;
        [SerializeField, PositiveInt] private int maxNumberOfLocation;

        [SerializeField] private List<LocationSO> usedLocations;
        [SerializeField] private List<Location> locationsInWorld;

        private Dictionary<Location, GameObject> locationObjects;

        public List<LocationSO> UsedLocations => usedLocations;


        private Transform world;

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        public void InitializeLocations(Transform world) {
            this.world = world;
            Statistics.MeasureTime(GenerateLocations);
            Statistics.MeasureTime(VisualizeLocations);
        }

        public void GenerateLocations() {
            Debug.Log("Generating Locations...");
            locationsInWorld = new();
            if (useRandomLocations) {
                CreateRandomLocationsFromSO();
            } else
                foreach (LocationSO usedLocation in usedLocations) {
                    CreateLocationFromSO(usedLocation, world);
                }
        }

        private void CreateRandomLocationsFromSO() {
            for (int i = 0; i < Random.Range(minNumberOfLocations, maxNumberOfLocation); i++) {
                CreateLocationFromSO(usedLocations[Random.Range(0, usedLocations.Count)], world);
            }
        }

        private Vector3 GetRandomWorldPosition(Vector3 centerPoint = default) {
            if (centerPoint == default) {
                return Vector3.zero;
            }
            return new Vector3(Random.Range(-100f, 100f) + centerPoint.x, 0f, Random.Range(-100f, 100f) + centerPoint.z);
        }

        public void VisualizeLocations() {
            Debug.Log("Visualizing Locations...");
            locationObjects = new();
            if (locationsInWorld == null || locationsInWorld.Count == 0)
                return;
            foreach (Location location in locationsInWorld) {
                GameObject locationObject = LocationVisualizer.Visualize(location);
                locationObjects.Add(location, locationObject);
            }
        }


        private void CreateLocationFromSO(LocationSO locationSO, Transform world) {
            GameObject locationObject = new(locationSO.locationName);
            Location locationComponent = locationObject.AddComponent<Location>();

            locationComponent.SetValuesFromSO(locationSO, world);




            locationsInWorld.Add(locationComponent);
        }

        public void AddUsedLocation(LocationSO usedLocation) => usedLocations.Add(usedLocation);
        internal void SetUsedLocations(List<LocationSO> usedLocations) => this.usedLocations = usedLocations;
    }
}