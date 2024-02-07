using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    [CreateAssetMenu(fileName = "New Section", menuName = "LecureyGames/Section")]
    public class SectionSO : ScriptableObject {
        public string sectionName;
        public List<string> workTypes;
    }
}