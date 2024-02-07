using UnityEngine;

namespace LecureyGames {
    public class CompanyObject : MonoBehaviour {
        [SerializeField] private new string name;
        [SerializeField] private string description;
        [SerializeField] private Branch[] branches;
    }
}