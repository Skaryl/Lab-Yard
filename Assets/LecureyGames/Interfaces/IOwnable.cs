using System.Collections.Generic;

namespace LecureyGames {
    public interface IOwnable {
        #region Properties
        public IOwnableType IOwnableType { get; }
        public List<IOwner> MyOwners { get; set; }
        #endregion

        #region Methods
        public void RemoveOwner(IOwner owner) => MyOwners?.Remove(owner);
        public void AddOwner(IOwner owner) {
            MyOwners?.Add(owner);
            if (!owner.OwnsProperty(this))
                owner.AddOwnedProperty(this);
        }
        public bool HasOwner(IOwner owner) => MyOwners?.Contains(owner) ?? false;
        #endregion
    }

    public enum IOwnableType {
        Company,
        Vehicle,
        Character,
    }
}