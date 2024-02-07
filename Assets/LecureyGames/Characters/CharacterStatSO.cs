using UnityEngine;

namespace LecureyGames {
    [CreateAssetMenu(fileName = "CharacterStatSO", menuName = "LecureyGames/Characters/CharacterStatSO", order = 1)]
    public class CharacterStatSO : ScriptableObject {
        public string statName = "New Character Stat SO";

        public float defaultValue = 1f;
        public float minValue = 0f;
        public float maxValue = 10f;
    }
}