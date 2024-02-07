namespace LecureyGames {
    public class WorkPoints {
        public Work Work { get; protected set; }

        public Filling Filling { get; protected set; }

        public WorkPoints(Work neededWork, int neededAmount) {
            Work = neededWork;
            Filling = new Filling(neededAmount);
        }

        public override string ToString() => $"{Work} ({Filling})";
    }
}