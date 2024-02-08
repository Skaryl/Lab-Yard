using LecureyGames.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    public class LocationManager : MonoBehaviour {
        public static LocationManager Instance { get; protected set; }

        [SerializeField, PositiveInt] protected int minNumberOfLocations;
        [SerializeField, PositiveInt] protected int maxNumberOfLocation;

        [SerializeField] protected List<LocationSO> locationsToUse;
        [SerializeField] protected List<Location> locationsInWorld;

        protected Dictionary<Location, GameObject> locationObjects;
        //protected Dictionary<Location, LocationSO> locationsToVisualize;

        protected void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
                return;
            }
            Instance = this;

            locationsToUse ??= new();
            locationsInWorld ??= new();
            locationObjects ??= new();
            //locationsToVisualize ??= new();
        }

        public void InitializeLocations() {
            locationsInWorld ??= new();
            locationObjects ??= new();
            Statistics.MeasureTime(GenerateLocations);
            //Statistics.MeasureTime(GenerateLocations);
            //Statistics.MeasureTime(VisualizeLocations);
        }

        protected void GenerateLocations() {
            if (locationsToUse == null || locationsToUse.Count == 0) {
                Debug.Log("No locations to generate");
                return;
            }
            Debug.Log("Generating Locations...");

            for (int i = 0; i < Random.Range(minNumberOfLocations, maxNumberOfLocation); i++) {
                LocationSO locationSO = locationsToUse[Random.Range(0, locationsToUse.Count)];
                Debug.Log($"{locationSO.LocationName} {i} {locationSO.BuildingDimension.FloorCount}");
                Location location = new(locationSO);
                locationsInWorld.Add(location);

                GameObject locationObject = LocationVisualizer.VisualizeSimplifiedBuilding(locationSO, true, true);
                locationObject.transform.SetParent(WorldManager.Instance.World.transform);
                locationObjects.Add(location, locationObject);
            }
        }

        //protected void GenerateLocations() {
        //    if (locationsToUse == null || locationsToUse.Count == 0) {
        //        Debug.Log("No locations to generate");
        //        return;
        //    }
        //    Debug.Log("Generating Locations...");
        //    locationsInWorld = new();
        //    for (int i = 0; i < Random.Range(minNumberOfLocations, maxNumberOfLocation); i++) {
        //        LocationSO locationSO = locationsToUse[Random.Range(0, locationsToUse.Count)];
        //        Debug.Log($"{locationSO.LocationName} {i} {locationSO.BuildingDimension.FloorCount}");
        //        CreateLocationAndAddToLists(locationSO);
        //    }
        //}

        //protected void CreateLocationAndAddToLists(LocationSO locationSO) {
        //    Location location = new(locationSO);
        //    locationsInWorld.Add(location);
        //    locationsToVisualize.Add(location, locationSO);
        //}

        //public void VisualizeLocations() {
        //    if (locationsInWorld == null || locationsInWorld.Count == 0) {
        //        Debug.Log("No locations to visualize");
        //        return;
        //    }
        //    Debug.Log("Visualizing Locations...");
        //    locationObjects = new();

        //    foreach (KeyValuePair<Location, LocationSO> kvp in locationsToVisualize) {
        //        GameObject locationObject = LocationVisualizer.VisualizeSimplifiedBuilding(kvp.Value, true, true);
        //        locationObject.transform.SetParent(WorldManager.Instance.World.transform);
        //        locationObjects.Add(kvp.Key, locationObject);
        //    }
        //    locationsToVisualize.Clear();
        //}
    }
}