using UnityEngine;

namespace LecureyGames {
    internal static class LocationVisualizer {
        public static GameObject VisualizeSimplifiedBuilding(LocationSO locationSO
            , bool createFloorObjects = false, bool createRoomObjects = false) {

            float xDimension = locationSO.BuildingDimension.XDimension;
            float yDimension = locationSO.BuildingDimension.YDimension;
            float zDimension = locationSO.BuildingDimension.ZDimension;

            float yOffset = locationSO.BuildingDimension.YOffsetForPlacement;

            int floorCount = locationSO.BuildingDimension.FloorCount;
            int basementCount = locationSO.BuildingDimension.BasementCount;
            int roomsPerFloor = locationSO.BuildingDimension.RoomsPerFloor;

            GameObject locationObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            locationObject.name = $"{locationSO.LocationName}";

            locationObject.transform.localScale = new Vector3(xDimension, yDimension, zDimension);
            locationObject.transform.localPosition = new Vector3(0, yOffset, 0);
            locationObject.AddComponent<BoxCollider>();

            locationObject.GetComponent<Renderer>().material = locationSO.ColliderBoxMaterial;

            if (!createFloorObjects)
                return locationObject;

            for (int levelNumber = -basementCount; levelNumber <= floorCount; levelNumber++) {
                GameObject floorObject = new(Floor.GetFloorName(levelNumber));
                floorObject.transform.parent = locationObject.transform;
                if (createRoomObjects) {
                    for (int roomNumber = 1; roomNumber <= roomsPerFloor; roomNumber++) {
                        GameObject roomObject = new(Floor.GetRoomName(roomsPerFloor, levelNumber, roomNumber));
                        roomObject.transform.parent = floorObject.transform;
                    }
                }
            }

            return locationObject;
        }
    }
}