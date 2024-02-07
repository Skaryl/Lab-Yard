using System;

namespace LecureyGames {
    [Serializable]
    public class CharacterName {
        protected string firstName;
        protected string middleName;
        protected string lastName;

        public string FirstName => firstName;
        public string MiddleName => middleName;
        public string LastName => lastName;


        public string FullName => $"{firstName} {middleName} {lastName}";

        public override string ToString() => FullName;
    }
}