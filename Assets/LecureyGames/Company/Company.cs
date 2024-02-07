using System;
using System.Collections.Generic;

namespace LecureyGames {
    [Serializable]
    public class Company : IOwner, IOwnable {
        #region fields
        protected string name;
        protected List<IOwner> owners;
        protected List<Branch> branches;
        protected bool commonlyKnown;
        protected List<Character> employees;
        protected List<IOwnable> ownedProperty;
        protected List<IOwner> myOwners;
        #endregion

        #region Properties
        public string Name => name;
        public List<IOwner> Owners => owners;
        public List<Branch> Branches => branches;
        public bool IsCommonlyKnown => commonlyKnown;
        public List<Character> Employees => employees;
        public List<IOwnable> OwnedProperty { get => ownedProperty; set => ownedProperty = value; }

        public IOwnerType OwnerType => IOwnerType.Company;
        public IOwnableType IOwnableType => IOwnableType.Company;

        public List<IOwner> MyOwners { get => myOwners; set => myOwners = value; }
        #endregion

        #region Constructors
        public Company() : this("new default company", new(), new List<Branch>(), false, new(), new()) { }
        public Company(CompanySO companySO) : this(companySO.name, new(), companySO.branches, false, new(), new()) { }
        public Company(string name, List<IOwner> owners, List<Branch> branches, bool commonlyKnown, List<Character> employees, List<IOwnable> ownerships) {
            this.name = name;
            this.owners = owners;
            this.branches = branches;
            this.commonlyKnown = commonlyKnown;
            this.employees = employees;
            this.ownedProperty = ownerships;
        }
        #endregion

        #region public methods
        public void AddBranch(Branch branch) => branches.Add(branch);
        public void RemoveBranch(Branch branch) => branches.Remove(branch);
        public void AddEmployee(Character character) => employees.Add(character);
        public void RemoveEmployee(Character character) => employees.Remove(character);
        public void SetKnown() => SetKnownState(true);
        public void SetUnKnown() => SetKnownState(false);
        #endregion

        #region internal methods
        internal void SetKnownState(bool known) => commonlyKnown = known;
        #endregion
    }
}