using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LecureyGames {
    [Serializable]
    public class Placement {
        public Vector3 position;
        public Quaternion rotation;

        /// <summary>
        /// Creates the default placement at (0, 0, 0).
        /// If randomRotation is false, the rotation will Quaternion.identity.
        /// </summary>
        /// <param name="randomRotation"></param>
        public Placement(bool randomRotation = false) : this(Vector3.zero, Quaternion.identity) {
            if (randomRotation)
                rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }

        /// <summary>
        /// Creates the placement at the given position.
        /// If randomRotation is false, the rotation will Quaternion.identity.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="randomRotation"></param>
        public Placement(Vector3 position, bool randomRotation = false) {
            this.position = position;
            if (randomRotation)
                rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }

        /// <summary>
        /// Creates the placement at the given position and rotation.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public Placement(Vector3 position, Quaternion rotation) {
            this.position = position;
            this.rotation = rotation;
        }

        public float Distance(Placement other) {
            return Vector3.Distance(position, other.position);
        }

        public static float Distance(Placement a, Placement b) {
            return Vector3.Distance(a.position, b.position);
        }

        public override string ToString() {
            return string.Format("Position: {0}, Rotation: {1}", position, rotation);
        }
    }
}