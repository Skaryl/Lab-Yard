using System;
using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    [Serializable]
    public class Location {
        [SerializeField] protected string locationName;
        protected string locationAddress;
        protected string locationDescription;
        protected LocationType locationType;
        [SerializeField] protected List<Floor> floors;

        protected List<IOwner> owners;
        protected List<string> ownerNamesCharacters;
        protected List<string> ownerNamesCompanies;
        protected List<Company> locatedCompanies;

        public string LocationName { get => locationName; protected set => locationName = value; }
        public string LocationAddress { get => locationAddress; protected set => locationAddress = value; }
        public string LocationDescription { get => locationDescription; protected set => locationDescription = value; }
        public LocationType LocationType { get => locationType; protected set => locationType = value; }
        public List<IOwner> Owners { get => owners; protected set => owners = value; }
        public List<Floor> Floors { get => floors; protected set => floors = value; }
        public List<Company> LocatedCompanies { get => locatedCompanies; protected set => locatedCompanies = value; }

        public Location(LocationSO locationSO) {
            LocationName = locationSO.LocationName;
            LocationAddress = $"{locationSO.Placement}"; // TODO: implement GetAdress after RoadGeneration is implemented
            LocationDescription = locationSO.Description;
            LocationType = locationSO.LocationType;
            Owners = new();
            Floors = new();
            LocatedCompanies = new();
            InitializeFloors(locationSO.BuildingDimension);
        }

        private void InitializeFloors(BuildingDimensions buildingDimensions) {
            int floorCount = buildingDimensions.FloorCount;
            int basementCount = buildingDimensions.BasementCount;
            int roomsPerFloor = buildingDimensions.RoomsPerFloor;

            Debug.Log($"{LocationName}: Floor Count: {floorCount}, Basement Count: {basementCount}, Total: {floorCount + basementCount + 1}, Rooms Per Floor: {roomsPerFloor}");

            for (int levelNumber = -basementCount; levelNumber <= floorCount; levelNumber++) {
                Floors.Add(new(levelNumber, roomsPerFloor, true));
            }
        }
    }
}