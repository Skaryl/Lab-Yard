using LecureyGames.Utilities;
using System;
using UnityEngine;

namespace LecureyGames {
    [Serializable]
    public class BuildingDimensions {
        #region Location and LocationObject
        [SerializeField, PositiveIntWithZero] private int floorCount;
        [SerializeField, PositiveIntWithZero] private int basementCount;
        [SerializeField, PositiveInt] private int roomsPerFloor;
        #endregion
        #region LocationObject
        [SerializeField, PositiveFloat] private float xDimension;
        [SerializeField, PositiveFloat] private float zDimension;
        [SerializeField, PositiveFloat] private float roomHeight;
        [SerializeField, PositiveFloat] private float wallThickness;
        [SerializeField, PositiveFloat] private float floorThickness;
        [SerializeField, PositiveFloat] private float roofThickness;
        #endregion

        public float XDimension { get => xDimension; protected set => xDimension = value; }
        public float YDimension { get => BuildingHeightOverground + BuildingHeightUnderground; }
        public float ZDimension { get => zDimension; protected set => zDimension = value; }
        public float YOffsetForPlacement { get => (YDimension / 2) - BuildingHeightUnderground; }
        public float RoomHeight { get => roomHeight; protected set => roomHeight = value; }
        public int FloorCount { get => floorCount; protected set => floorCount = value; }
        public int BasementCount { get => basementCount; protected set => basementCount = value; }
        public float WallThickness { get => wallThickness; protected set => wallThickness = value; }
        public float FloorThickness { get => floorThickness; protected set => floorThickness = value; }
        public float RoofThickness { get => roofThickness; protected set => roofThickness = value; }
        public int RoomsPerFloor { get => roomsPerFloor; protected set => roomsPerFloor = value; }
        public float BuildingHeightOverground { get => (FloorCount * RoomHeight) + ((FloorCount - 1f) * FloorThickness) + RoofThickness; }
        public float BuildingHeightUnderground { get => (BasementCount * (RoomHeight + FloorThickness)) + FloorThickness; }

        /// <summary>
        /// Creates a new BuildingDimensions with default values for testing purposes
        /// </summary>
        public BuildingDimensions() : this(7f, 10f, 3f, 0, 0, .43f, .4f, .4f, 4) { }
        public BuildingDimensions(float xDimension, float zDimension, float roomHeight, int floorCount, int basementCount, float wallThickness, float floorThickness, float roofThickness, int roomsPerFloor) {
            XDimension = xDimension;
            ZDimension = zDimension;
            RoomHeight = roomHeight;
            FloorCount = floorCount;
            BasementCount = basementCount;
            WallThickness = wallThickness;
            FloorThickness = floorThickness;
            RoofThickness = roofThickness;
            RoomsPerFloor = roomsPerFloor;
        }
    }
}