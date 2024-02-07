using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    public class SectionFileManager {
#pragma warning disable IDE0044
        private static string filePath = "sections.dat";
#pragma warning restore IDE0044

        public static void SaveSections(List<SectionSO> sections, Dictionary<string, bool> chooseSections) {
            List<SectionData> sectionDataList = new();

            foreach (var section in sections) {
                SectionData sectionData = new(section.sectionName, section.workTypes, chooseSections[section.sectionName]);
                sectionDataList.Add(sectionData);
            }

            string json = JsonUtility.ToJson(new SectionFileData(sectionDataList));
            System.IO.File.WriteAllText(filePath, json);
        }

        public static SectionFileData LoadSections() {
            if (!System.IO.File.Exists(filePath))
                return null;

            string json = System.IO.File.ReadAllText(filePath);
            return JsonUtility.FromJson<SectionFileData>(json);
        }
    }
}