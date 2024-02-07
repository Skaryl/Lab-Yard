using LecureyGames.Utilities;
using System;
using UnityEngine;

namespace LecureyGames {
    [Serializable]
    public class BuildingDimensions {
        [SerializeField] private float xDimension;
        [SerializeField] private float zDimension;
        [SerializeField] private float roomHeight;
        [SerializeField] private int floorCount;
        [SerializeField] private int basementCount;
        [SerializeField] private float wallThickness;
        [SerializeField] private float floorThickness;
        [SerializeField] private float roofThickness;
        [SerializeField, PositiveInt] private int roomsPerFloor;

        public float XDimension { get => xDimension; protected set => xDimension = value; }
        public float ZDimension { get => zDimension; protected set => zDimension = value; }
        public float RoomHeight { get => roomHeight; protected set => roomHeight = value; }
        public int FloorCount { get => floorCount; protected set => floorCount = value; }
        public int BasementCount { get => basementCount; protected set => basementCount = value; }
        public float WallThickness { get => wallThickness; protected set => wallThickness = value; }
        public float FloorThickness { get => floorThickness; protected set => floorThickness = value; }
        public float RoofThickness { get => roofThickness; protected set => roofThickness = value; }
        public int RoomsPerFloor { get => roomsPerFloor; protected set => roomsPerFloor = value; }
        public float BuildingHeightOverground { get => (FloorCount * RoomHeight) + ((FloorCount - 1) * FloorThickness) + RoofThickness; }
        public float BuildingHeightUnderground { get => (BasementCount * (RoomHeight + FloorThickness)) + FloorThickness; }
        public float YDimension { get => BuildingHeightOverground + BuildingHeightUnderground; }

        public BuildingDimensions() : this(10f, 10f, 3f, 0, 0, .43f, .4f, .4f) { }
        public BuildingDimensions(float xDimension, float zDimension, float roomHeight, int floorCount, int basementCount, float wallThickness, float floorThickness, float roofThickness) {
            XDimension = xDimension;
            ZDimension = zDimension;
            RoomHeight = roomHeight;
            FloorCount = floorCount;
            BasementCount = basementCount;
            WallThickness = wallThickness;
            FloorThickness = floorThickness;
            RoofThickness = roofThickness;
        }
    }
}