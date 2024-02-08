using System;
using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    [Serializable]
    public class Room {
        protected RoomType roomType;
        [SerializeField] protected string roomName;
        [SerializeField] protected int roomNumber;
        protected List<WorkStation> workStations;

        public RoomType RoomType { get => roomType; protected set => roomType = value; }
        public string RoomName { get => roomName; protected set => roomName = value; }
        public int RoomNumber { get => roomNumber; protected set => roomNumber = value; }
        public List<WorkStation> WorkStations { get => workStations; protected set => workStations = value; }


        public Room(int roomNumber, string roomName) : this(roomNumber, RoomType.NONE, roomName) { }

        public Room(int roomNumber, RoomType roomType = RoomType.NONE, string roomName = "Empty Room") {
            this.roomName = roomName;
            this.roomType = roomType;
            this.roomNumber = roomNumber;
            workStations = new();
        }

        public void AddWorkStation(WorkStation workStation) {
            WorkStations.Add(workStation);
        }

        public void SetWorkStations(List<WorkStation> workStations) {
            WorkStations = workStations;
        }

        public void RemoveWorkStation(WorkStation workStation) {
            WorkStations.Remove(workStation);
        }
    }
}