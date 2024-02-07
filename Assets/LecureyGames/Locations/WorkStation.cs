using System.Collections.Generic;

namespace LecureyGames {
    public class WorkStation {
        public string Name { get; set; }
        public HashSet<WorkType> PossibleWorkTypes { get; set; }

        public WorkStation(string name, HashSet<WorkType> possibleWorkTypes) {
            Name = name;
            PossibleWorkTypes = possibleWorkTypes;
        }

        public void AddPossibleWorkType(WorkType workType) {
            PossibleWorkTypes.Add(workType);
        }

        public void RemovePossibleWorkType(WorkType workType) {
            PossibleWorkTypes.Remove(workType);
        }

        public void InteractWithProject(Character character, Project project) {
            if (!project.AssignedCharacters.Contains(character)) {
                project.AddAssignedCharacter(character);
            }

            foreach (Work work in project.NeededWorks) {
                if (!PossibleWorkTypes.Contains(work.WorkType)) {


                }
            }
        }
    }
}