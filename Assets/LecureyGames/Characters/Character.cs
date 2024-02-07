using System;
using System.Collections.Generic;

namespace LecureyGames {
    [Serializable]
    public class Character : IOwner, IOwnable {

        public CharacterName CharacterName { get; protected set; }
        public string Name => CharacterName.FullName;
        public Gender Gender { get; protected set; }
        protected CharacterSheet characterSheet = new();
        protected List<IOwnable> ownedProperty = new();
        protected List<IOwner> myOwners = new();

        public List<Branch> Branches { get; protected set; } = new List<Branch>();
        public List<Project> Projects { get; set; }

        #region Properties for Interfaces
        public IOwnerType OwnerType => IOwnerType.Character;
        public List<IOwnable> OwnedProperty { get => ownedProperty; set => ownedProperty = value; }
        public List<IOwner> MyOwners { get => myOwners; set => myOwners = value; }

        public IOwnableType IOwnableType => IOwnableType.Character;
        #endregion

        public Character() {

        }

        public override string ToString() => $"{CharacterName.FullName} ({Gender})";

        internal void AddAssignedProject(Project project) {
            Projects.Add(project);
            project.AddAssignedCharacter(this);
        }
        internal void RemoveAssignedProject(Project project) {
            Projects.Remove(project);
            project.RemoveAssignedCharacter(this);
        }
    }
}