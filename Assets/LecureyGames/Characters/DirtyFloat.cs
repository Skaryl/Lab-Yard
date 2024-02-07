using System;

namespace LecureyGames {
    public class DirtyFloat {
        #region Properties
        private float actualValue;
        private float minValue;
        private float maxValue;


        public float Value {
            get => actualValue;
            set => actualValue = ClampValue ? Math.Clamp(value, Min, Max) : value;
        }
        public float Min {
            get => minValue;
            set {
                minValue = value;
                Value = ClampValue ? Math.Clamp(Value, Min, Max) : Value;
            }
        }
        public float Max {
            get => maxValue;
            set {
                maxValue = value;
                Value = ClampValue ? Math.Clamp(Value, Min, Max) : Value;
            }
        }
        public bool NeedsRecalculation { get; set; }
        public bool ClampValue { get; protected set; }
        #endregion

        #region Constructors
        public DirtyFloat() : this(1f, 0f, 10f, false, true) { }
        public DirtyFloat(bool clampValue) : this(1f, 0f, 10f, false, clampValue) { }
        public DirtyFloat(float value = 1f, float minValue = 0f, float maxValue = 10f, bool isDirty = false, bool clampValue = true) {
            Value = value;
            Min = minValue;
            Max = maxValue;
            NeedsRecalculation = isDirty;
            ClampValue = clampValue;
        }
        #endregion

        #region Operators
        public static implicit operator float(DirtyFloat floatDirty) {
            return floatDirty.Value;
        }
        public static implicit operator bool(DirtyFloat floatDirty) {
            return floatDirty.NeedsRecalculation;
        }
        public static DirtyFloat operator +(DirtyFloat floatDirty, float value) {
            floatDirty.Value += value;
            return floatDirty;
        }
        public static implicit operator DirtyFloat(float value) {
            return new DirtyFloat { Value = value };
        }
        public static DirtyFloat operator +(DirtyFloat floatDirty, bool isDirty) {
            floatDirty.NeedsRecalculation = isDirty;
            return floatDirty;
        }
        public static DirtyFloat operator +(DirtyFloat floatDirty, DirtyFloat other) {
            floatDirty.Value = other.Value;
            floatDirty.Min = other.Min;
            floatDirty.Max = other.Max;
            floatDirty.NeedsRecalculation = other.NeedsRecalculation;
            return floatDirty;
        }
        #endregion

        #region Methods
        public void IsNotUpToDate() => NeedsRecalculation = true;
        public void IsUpToDate() => NeedsRecalculation = false;
        #endregion
    }
}