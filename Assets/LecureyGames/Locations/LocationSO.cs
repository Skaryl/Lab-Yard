using UnityEngine;

namespace LecureyGames {
    [CreateAssetMenu(fileName = "LocationSO", menuName = "LecureyGames/LocationSO", order = 1)]
    public class LocationSO : ScriptableObject {
        public string locationName;
        public LocationType locationType;
        public Vector3 position;
        public Quaternion rotation;
        public BuildingDimensions buildingDimension;
        public Material simpleVisualisationMaterial;
        public int minAmountInWorld;
    }
}