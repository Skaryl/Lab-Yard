using System;
using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    [Serializable]
    public class Floor {
        [SerializeField] protected int floorLevel;
        protected string floorName;
        [SerializeField] protected List<Room> rooms;

        public int FloorLevel { get => floorLevel; protected set => floorLevel = value; }
        public string FloorName { get => floorName; protected set => floorName = value; }
        public List<Room> Rooms { get => rooms; protected set => rooms = value; }

        public Floor(int floorNumber, int roomsPerFloor, bool useNamesWithNumbers = false) {
            FloorName = GetFloorName(floorNumber);
            FloorLevel = floorNumber;

            InitializeRooms(roomsPerFloor, useNamesWithNumbers);
        }

        protected void InitializeRooms(int roomsPerFloor, bool useNamesWithNumbers = false) {
            Rooms = new();
            if (!useNamesWithNumbers) {
                for (int i = 0; i < roomsPerFloor; i++) {
                    Rooms.Add(new Room(i));
                }
                return;
            }

            for (int roomNumber = 0; roomNumber < roomsPerFloor; roomNumber++) {
                Rooms.Add(new Room(roomNumber, GetRoomName(roomsPerFloor, FloorLevel, roomNumber)));
            }
        }

        public static string GetFloorName(int floorNumber) {
            if (floorNumber == 0)
                return $"GroundLevel";
            else if (floorNumber < 0)
                return $"Basement {Math.Abs(floorNumber)}";
            else
                return $"Floor {floorNumber}";
        }

        public static string GetRoomName(int roomsPerFloor, int levelNumber, int roomNumber) {
            string roomNumberFormat = $"D{Math.Floor(Math.Log10(roomsPerFloor) + 1)}";
            return string.Format("Room {0}{1}", levelNumber, roomNumber.ToString(roomNumberFormat));
        }
    }
}