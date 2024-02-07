using System.Collections.Generic;

namespace LecureyGames {
    public class SectionData {
        public string sectionName;
        public List<string> branchTypes;
        public bool useSection;

        public SectionData(string sectionName, List<string> branchTypes, bool useSection) {
            this.sectionName = sectionName;
            this.branchTypes = branchTypes;
            this.useSection = useSection;
        }
    }
}