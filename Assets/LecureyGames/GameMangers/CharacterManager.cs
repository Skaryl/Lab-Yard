using System.Collections.Generic;
using UnityEngine;


namespace LecureyGames {
    public class CharacterManager : MonoBehaviour {
        public static CharacterManager Instance { get; private set; }

        public GameObject npcPrefab;
        public GameObject playerPrefab;

        public List<Character> characters = new();

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(this);
            } else {
                Instance = this;
            }
        }
    }
}