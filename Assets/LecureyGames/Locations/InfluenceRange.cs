using UnityEngine;

namespace LecureyGames {
    public class InfluenceRadius : MonoBehaviour {
        public float InfluenceRange { get; set; }
        public AnimationCurve InfluenceIntensityCurve { get; set; }

        public float GetInfluenceAtLocation(Vector3 location) {
            Vector2 location2D = new(location.x, location.z);

            float distanceToCenter = Vector2.Distance(location2D, Vector2.zero);

            if (distanceToCenter > InfluenceRange)
                return 0f;

            float normalizedDistance = distanceToCenter / InfluenceRange;

            float intensity = InfluenceIntensityCurve.Evaluate(normalizedDistance);
            return intensity;

        }
    }
}