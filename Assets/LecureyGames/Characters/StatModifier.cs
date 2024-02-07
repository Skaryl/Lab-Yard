namespace LecureyGames {
    public enum StatModType {
        Flat = 100,
        PercentMult = 200,
        PercentAdd = 300,
        Permanent = 400,
    }

    public enum StatModPurpose {
        Value = 100,
        MinValue = 200,
        MaxValue = 300,
    }

    public class StatModifier {
        public readonly float value;
        public readonly StatModType type;
        public readonly StatModPurpose purpose;
        public int order;
        public object source;

        public StatModifier(float value, StatModType type, int order, object source, StatModPurpose purpose) {
            this.value = value;
            this.type = type;
            this.order = order;
            this.source = source;
            this.purpose = purpose;
        }

        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null, StatModPurpose.Value) { }
        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null, StatModPurpose.Value) { }
        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source, StatModPurpose.Value) { }
        public StatModifier(float value, StatModType type, int order, object source) : this(value, type, order, source, StatModPurpose.Value) { }
        public StatModifier(float value, StatModType type, StatModPurpose purpose) : this(value, type, (int)type, null, purpose) { }
        public StatModifier(float value, StatModType type, int order, StatModPurpose purpose) : this(value, type, order, null, purpose) { }
        public StatModifier(float value, StatModType type, object source, StatModPurpose purpose) : this(value, type, (int)type, source, purpose) { }
    }
}