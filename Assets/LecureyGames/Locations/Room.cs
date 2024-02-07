using System;
using System.Collections.Generic;

namespace LecureyGames {
    [Serializable]
    public class Room {
        private RoomType roomType;
        private string roomName;
        private int roomNumber;
        [NonSerialized] private Floor floor;
        private List<WorkStation> workStations;
        public RoomType RoomType { get => roomType; protected set => roomType = value; }
        public string RoomName { get => roomName; protected set => roomName = value; }
        public int RoomNumber { get => roomNumber; protected set => roomNumber = value; }
        public Floor Floor { get => floor; protected set => floor = value; }
        public List<WorkStation> WorkStations { get => workStations; protected set => workStations = value; }


        public Room(Floor floor, int roomNumber, string roomName) : this(floor, roomNumber, RoomType.NONE, roomName) { }

        public Room(Floor floor, int roomNumber, RoomType roomType = RoomType.NONE, string roomName = "Empty Room") {
            this.roomName = roomName;
            this.roomType = roomType;
            this.roomNumber = roomNumber;
            this.floor = floor;
            this.workStations = new();
        }

        public void AddWorkStation(WorkStation workStation) {
            WorkStations.Add(workStation);
        }

        public void RemoveWorkStation(WorkStation workStation) {
            WorkStations.Remove(workStation);
        }
    }

    public enum RoomType {
        NONE,
        #region residential
        LivingRoom,
        Kitchen,
        Bedroom,
        Bathroom,
        Hallway,
        Hall,
        Garden,
        #endregion

        OfficeRoom,
        OfficeSpace,
        GuestRoom,
        Study,
        Garage,
        EntranceHall,
        Workshop,
        WaitingRoom,
        WaitingArea,
        Stairwell,

        Laboratory,
        ConferenceRoom,
        Lounge,
        Storage,
        Production,
    }
}