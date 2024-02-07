using System.Collections.Generic;
using UnityEngine;

namespace LecureyGames {
    public class CharacterSheet : MonoBehaviour {
        [SerializeField] protected CharacterName characterName;
        [SerializeField] protected string stat;
        protected List<CharacterStat> statList = new();


    }
}