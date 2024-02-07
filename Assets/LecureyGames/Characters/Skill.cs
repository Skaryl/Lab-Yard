using UnityEngine;

namespace LecureyGames {
    public class Skill {
        private int experiencePoints;

        public WorkType WorkType { get; protected set; }
        public ExperienceLevel CurrentLevel { get; protected set; }
        public bool SkillLevelLimitReached { get; protected set; }

        public ExperienceLevel NextLevel { get; protected set; }


        public int ExperiencePoints {
            get { return experiencePoints; }
            set {
                if (SkillLevelLimitReached)
                    return;
                experiencePoints = value;
                if (value >= (int)NextLevel) {
                    SetNextLevel();
                }
                if (CurrentLevel == NextLevel) {
                    experiencePoints = (int)CurrentLevel;
                    SkillLevelLimitReached = true;
                }
            }
        }

        public Skill(WorkType workType) {
            WorkType = workType;
            CurrentLevel = ExperienceLevel.None;
            NextLevel = ExperienceLevelExtensions.GetNext(CurrentLevel);
            SkillLevelLimitReached = false;
            ExperiencePoints = 0;
        }

        private void SetNextLevel() {
            CurrentLevel = NextLevel;
            NextLevel = ExperienceLevelExtensions.GetNext(CurrentLevel);
        }

        public void GainExperience(int experiencePoints) {
            if (SkillLevelLimitReached) {
                Debug.Log("Skill level limit reached");
                return;
            }
            ExperiencePoints += experiencePoints;
        }

        public int MissingPointsTillNextLevel => (int)CurrentLevel - ExperiencePoints;
    }
}