using System.Collections.Generic;

namespace LecureyGames {
    public class Project {
        public string Name { get; protected set; }
        public int Complexity { get; protected set; }
        public int RequiredTrainingTime { get; protected set; }
        public List<WorkPoints> NeededWorkPoints { get; protected set; }
        public HashSet<Character> AssignedCharacters { get; protected set; }
        public Filling Filling { get; protected set; }
        public HashSet<Work> NeededWorks { get; protected set; }

        public Project(string name, int complexity, int requiredTrainingTime, List<WorkPoints> neededWorkPoints) {
            if (neededWorkPoints.Count == 0) {
                throw new System.ArgumentException("Project must have at least one work point", nameof(neededWorkPoints));
            }
            Name = name;
            Complexity = complexity;
            RequiredTrainingTime = requiredTrainingTime;
            NeededWorkPoints = neededWorkPoints;
            InitializeNeededWorks();
            Filling = new(GetFillingNeededWorkPointsSum());
            AssignedCharacters = new HashSet<Character>();
        }

        public void AddAssignedCharacter(Character character) => AssignedCharacters.Add(character);

        public void RemoveAssignedCharacter(Character character) => AssignedCharacters.Remove(character);

        public void AddWork(Work work) => NeededWorks.Add(work);
        public void RemoveWork(Work work) => NeededWorks.Remove(work);

        public void InitializeNeededWorks() {
            NeededWorks = new();
            foreach (WorkPoints workPoints in NeededWorkPoints) {
                NeededWorks.Add(workPoints.Work);
            }
        }

        public ContributeResult ContributeWork(Work work) {
            if (work.GetContributionValuation(this.NeededWorkPoints[0].Work) == ContributeValuation.Success) {
                return ContributeResult.Success;
            }
            return ContributeResult.Failure;
        }

        public enum ContributeResult {
            WorkPointsFull,
            WrongWorkPoints,
            Success,
            Failure,
        }

        public int GetFillingNeededWorkPointsSum() {
            int sum = 0;
            foreach (WorkPoints workPoints in NeededWorkPoints) {
                sum += workPoints.Filling.NeededAmount;
            }
            return sum;
        }

        public override string ToString() => $"{Name} () ({Filling})";
    }
}