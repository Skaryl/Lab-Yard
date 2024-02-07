using System;

namespace LecureyGames {
    [Serializable]
    public class FloorCell {
        public Floor Floor { get; set; }
        public Room Room { get; set; }
    }
}