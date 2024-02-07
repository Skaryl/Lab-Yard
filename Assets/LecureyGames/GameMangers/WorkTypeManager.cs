using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    public class WorkTypeManager : MonoBehaviour {
        private static WorkTypeManager instance;

        public static WorkTypeManager Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType<WorkTypeManager>();
                    if (instance == null) {
                        GameObject obj = new();
                        instance = obj.AddComponent<WorkTypeManager>();
                    }
                }
                return instance;
            }
        }

        private Dictionary<string, bool> chooseSections;
        private List<SectionSO> sections;

        private void Awake() {
            chooseSections = new();
            sections = new();
        }

        public SectionSO GetSectionByName(string sectionName) => sections.Find(section => section.sectionName == sectionName);

        public void AddNewSection(string sectionName) {
            SectionSO newSection = ScriptableObject.CreateInstance<SectionSO>();
            newSection.sectionName = sectionName;
            sections.Add(newSection);
            chooseSections[sectionName] = true;
        }
    }
}