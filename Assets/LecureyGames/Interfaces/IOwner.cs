using System.Collections.Generic;

namespace LecureyGames {
    public interface IOwner {
        #region Properties
        public string Name { get; }
        public IOwnerType OwnerType { get; }
        public List<IOwnable> OwnedProperty { get; set; }
        #endregion

        #region Methods
        public void AddOwnedProperty(IOwnable ownable) {
            OwnedProperty?.Add(ownable);
            if (!ownable.HasOwner(this))
                ownable.AddOwner(this);
        }

        public bool OwnsProperty(IOwnable ownable) => OwnedProperty?.Contains(ownable) ?? false;

        public void RemoveOwnedProperty(IOwnable ownable) {
            OwnedProperty?.Remove(ownable);
            ownable.RemoveOwner(this);
        }
        #endregion
    }

    public enum IOwnerType {
        Company,
        Character,
    }
}