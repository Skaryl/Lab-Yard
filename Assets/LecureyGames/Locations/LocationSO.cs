using System;
using UnityEngine;

namespace LecureyGames {
    [Serializable, CreateAssetMenu(fileName = "LocationSO", menuName = "LecureyGames/LocationSO", order = 1)]
    public class LocationSO : ScriptableObject {
        [SerializeField] private int minAmountInWorld;
        [SerializeField] private string locationName;
        [SerializeField] private string description;
        [SerializeField] private LocationType locationType;
        [SerializeField] private BuildingDimensions buildingDimension;
        [SerializeField] private Placement placement;
        [SerializeField] private Material colliderBoxMaterial;


        #region LocationManager
        public int MinAmountInWorld { get => minAmountInWorld; }
        #endregion
        #region Location
        public string LocationName { get => locationName; }
        public string Description { get => description; }
        public LocationType LocationType { get => locationType; }
        #endregion
        #region Location and LocationObject
        public BuildingDimensions BuildingDimension { get => buildingDimension; }
        #endregion
        #region LocationObject
        public Placement Placement { get => placement; }
        public Material ColliderBoxMaterial { get => colliderBoxMaterial; }
        #endregion
    }
}