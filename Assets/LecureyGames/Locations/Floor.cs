using System;
using System.Collections.Generic;

namespace LecureyGames {
    [Serializable]
    public class Floor {
        public int FloorNumber { get; protected set; }
        public string FloorName { get; protected set; }
        public List<Room> Rooms { get; protected set; }
        public Location Location { get; protected set; }

        public Floor(int floorNumber, BuildingDimensions buildingDimensions, Location location, bool initNamesWithNumbers = false) {
            FloorName = GetFloorName(floorNumber);
            FloorNumber = floorNumber;
            Location = location;
            Rooms = new();
            if (initNamesWithNumbers) {
                string roomNumberFormat = $"D{Math.Floor(Math.Log10(buildingDimensions.RoomsPerFloor) + 1)}";
                for (int i = 0; i < buildingDimensions.RoomsPerFloor; i++) {
                    Rooms.Add(new Room(this, i, string.Format("Room {0}{1}", FloorNumber, i.ToString(roomNumberFormat))));
                }
            } else
                for (int i = 0; i < buildingDimensions.RoomsPerFloor; i++) {
                    Rooms.Add(new Room(this, i));
                }
        }

        private string GetFloorName(int floorNumber) {
            if (floorNumber == 0)
                return $"GroundLevel";
            else if (floorNumber < 0)
                return $"Basement {Math.Abs(floorNumber)}";
            else
                return $"Floor {floorNumber}";
        }
    }
}