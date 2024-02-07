using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    [CreateAssetMenu(fileName = "Company", menuName = "LecureyGames/Company", order = 1)]
    public class CompanySO : ScriptableObject {
        public new string name = "default company";
        public string description = "this is a preset company";
        public List<Branch> branches = new();
    }
}