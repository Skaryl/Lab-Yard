namespace LecureyGames {
    public class Work {
        public WorkType WorkType { get; protected set; }
        public ExperienceLevel ExperienceLevel { get; protected set; }

        public Work(WorkType workType, ExperienceLevel experienceLevel) {
            WorkType = workType;
            ExperienceLevel = experienceLevel;
        }

        public ContributeValuation GetContributionValuation(Work otherWork) => ((int)ExperienceLevel - (int)otherWork.ExperienceLevel >= 0) ? ContributeValuation.Success : ContributeValuation.Failure;

        public override string ToString() => $"{WorkType} ({ExperienceLevel})";
    }
}