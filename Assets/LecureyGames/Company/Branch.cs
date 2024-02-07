using System;
using UnityEngine;

namespace LecureyGames {
    [Serializable]
    public class Branch {
        [SerializeField] private BranchType type;
        public BranchType Type { get => type; protected set => type = value; }
        [SerializeField] private bool commonlyKnown;
        public bool CommonlyKnown { get => commonlyKnown; protected set => commonlyKnown = value; }

        [SerializeField][Range(-100f, 100f)] private float reputation;
        public float Reputation { get => reputation; protected set => reputation = value; }

        public Branch(BranchType type, bool commonlyKnown, float ruputation) {
            Type = type;
            CommonlyKnown = commonlyKnown;
            Reputation = ruputation;
        }


    }
}