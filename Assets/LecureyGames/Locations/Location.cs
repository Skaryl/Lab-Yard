using System;
using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    [Serializable]
    public class Location : MonoBehaviour {
        [SerializeField] protected new string name = "Unnamed Location";
        [SerializeField] protected string address = "Unknown";
        [SerializeField] protected string description = "No description";
        [SerializeField] protected LocationType type = LocationType.RoboticsWorkshop;
        [SerializeField] protected List<string> ownerNamesCharacters;
        [SerializeField] protected List<string> ownerNamesCompanies;

        public string Name { get => name; protected set => name = value; }
        public string Address { get => address; protected set => address = value; }
        public string Description { get => description; protected set => description = value; }
        public LocationType Type { get => type; protected set => type = value; }
        public List<IOwner> Owners { get; protected set; }
        public List<Floor> Floors { get; protected set; }
        public List<Company> LocatedCompanies { get; protected set; }
        public LocationSO LocationSO { get; protected set; }
        public Location() {
        }

        public void SetValuesFromSO(LocationSO locationSO, Transform world) {
            transform.parent = world;
            LocationSO = locationSO;
            Name = locationSO.locationName;
            transform.position = locationSO.position;
            Address = locationSO.position.ToString();
            Description = "No description";
            Type = locationSO.locationType;
            InitializeOwners();
            InitializeFloors(locationSO.buildingDimension);
        }

        private void InitializeFloors(BuildingDimensions buildingDimensions) {
            Floors = new() {
                new(0, buildingDimensions, this)
            };
            if (buildingDimensions.FloorCount > 0)
                for (int i = 1; i <= buildingDimensions.FloorCount; i++) {
                    Floors.Add(new(i, buildingDimensions, this));
                }
            if (buildingDimensions.BasementCount > 0)
                for (int i = 1; i <= buildingDimensions.BasementCount; i++) {
                    Floors.Add(new(-i, buildingDimensions, this));
                }
        }

        private void InitializeOwners(List<IOwner> owners = null) {
            Owners = new();
            if (owners == null)
                return;
            foreach (var owner in owners) {
                Owners.Add(owner);
            }
        }

        public List<string> GetOwnerNamesCharacters() {
            List<string> names = new();
            foreach (IOwner owner in Owners) {
                if (owner is Character)
                    names.Add(owner.Name);
            }
            return names;
        }

        public List<string> GetOwnerNamesCompanies() {
            List<string> names = new();
            foreach (IOwner owner in Owners) {
                if (owner is Company)
                    names.Add(owner.Name);
            }
            return names;
        }
    }
}