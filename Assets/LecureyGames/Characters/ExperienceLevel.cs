using System;
using System.Linq;

namespace LecureyGames {
    public enum ExperienceLevel {
        None = 0,
        Novice = 100,
        Beginner = 200,
        Basic = 400,
        Intermediate = 750,
        Advanced = 1250,
        Expert = 2000,
        Master = 3000,
        Legendary = 5000,
        Mythical = 7500,
        Godly = 15000,
        Transcendent = 25000,
        Infinite = 50000
    }

    public static class ExperienceLevelExtensions {
        public static ExperienceLevel GetNext(this ExperienceLevel currentLevel) {
            // Hier wird angenommen, dass die Enum-Werte in aufsteigender Reihenfolge stehen
            var allLevels = Enum.GetValues(typeof(ExperienceLevel)).Cast<ExperienceLevel>().ToList();

            // Finde den Index des aktuellen Werts
            var currentIndex = allLevels.IndexOf(currentLevel);

            // Wenn der Index gültig ist und nicht am Ende der Liste steht, gib den nächsten Wert zurück
            if (currentIndex >= 0 && currentIndex < allLevels.Count - 1) {
                return allLevels[currentIndex + 1];
            }

            // Ansonsten, wenn am Ende der Liste, gib den aktuellen Wert zurück
            return currentLevel;
        }
    }
}