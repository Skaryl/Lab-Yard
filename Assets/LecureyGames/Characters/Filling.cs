namespace LecureyGames {
    public class Filling {
        public int NeededAmount { get; protected set; }
        public int Amount { get; protected set; }

        public bool IsFull { get; protected set; }

        public Filling(int neededAmount) {
            NeededAmount = neededAmount;
            IsFull = false;
            Amount = 0;
        }

        public void Add(int amount) {
            Amount += amount;
            if (Amount >= NeededAmount)
                IsFull = true;
        }

        public float GetFillPercentage() => (Amount * 100.0f / NeededAmount);

        public override string ToString() => $"{Amount}/{NeededAmount}, {GetFillPercentage():F1}%";
    }
}