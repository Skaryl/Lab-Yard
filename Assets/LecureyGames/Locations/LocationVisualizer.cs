using UnityEngine;

namespace LecureyGames {
    internal static class LocationVisualizer {

        internal static GameObject Visualize(Location location) {
            GameObject locationObject = new GameObject(location.Name);
            locationObject.transform.SetParent(location.transform);
            // Visualisierung für jeden Floor
            foreach (Floor floor in location.Floors) {
                GameObject floorObject = CreateFloorMesh(floor, locationObject.transform);
                floorObject.transform.SetParent(locationObject.transform);
                foreach (Room room in floor.Rooms) {
                    GameObject roomObject = new(room.RoomName);
                    roomObject.transform.SetParent(floorObject.transform);
                }
            }

            GameObject gameObject = VisualizeSimplifiedBuilding(location);
            gameObject.transform.SetParent(locationObject.transform);

            return locationObject;
        }

        public static GameObject VisualizeSimplifiedBuilding(Location location) {
            float xDimension = location.LocationSO.buildingDimension.XDimension;
            float yDimension = location.LocationSO.buildingDimension.YDimension;
            float zDimension = location.LocationSO.buildingDimension.ZDimension;

            float yOffset = yDimension / 2 - location.LocationSO.buildingDimension.BuildingHeightUnderground;

            GameObject locationObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            locationObject.transform.SetParent(location.transform);

            locationObject.name = $"{location.Name} simplified";

            locationObject.transform.localScale = new Vector3(xDimension, yDimension, zDimension);
            locationObject.transform.localPosition = new Vector3(0, yOffset, 0);
            locationObject.AddComponent<BoxCollider>();

            locationObject.GetComponent<Renderer>().material = location.LocationSO.simpleVisualisationMaterial;
            return locationObject;
        }

        private static GameObject CreateFloorMesh(Floor floor, Transform parent) {
            float wallHeight = floor.Location.LocationSO.buildingDimension.RoomHeight;
            float wallThickness = floor.Location.LocationSO.buildingDimension.WallThickness;
            float flooringThickness = floor.Location.LocationSO.buildingDimension.FloorThickness;
            float floorHeight = flooringThickness + wallHeight;
            // Implementiere hier die Logik zur Erstellung des Mesh für einen Floor
            // Verwende floor und parent, um das Mesh entsprechend zu platzieren

            GameObject floorObject = new(floor.FloorName);
            floorObject.transform.SetParent(parent);
            floorObject.transform.position = new Vector3(0, floor.FloorNumber * floorHeight, 0);
            // Erstelle ein neues MeshFilter und MeshRenderer für den Floor
            MeshFilter meshFilter = floorObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = floorObject.AddComponent<MeshRenderer>();
            // Füge bei Bedarf weitere Details für die Mesh-Erstellung hinzu
            Mesh mesh = new();

            // Füge hier die Logik hinzu, um das Mesh basierend auf den Floor-Informationen zu erstellen
            // Beispiel: Setze die Vertices, Triangles, Normals, UVs usw.

            // Weise das erstellte Mesh dem MeshFilter zu
            meshFilter.mesh = mesh;

            // Füge bei Bedarf Materialien und Texturen hinzu
            // Beispiel: meshRenderer.material = yourMaterial;
            return floorObject;
        }
    }
}