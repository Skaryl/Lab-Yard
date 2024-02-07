using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LecureyGames {
    [Serializable]
    public class CharacterStat {
        #region Constants
        public const bool IS_CLAMPED = true;
        public const bool IS_NOT_CLAMPED = false;
        #endregion

        #region Internal Helper Variables
        internal float finalMinValue;
        internal float finalMaxValue;
        internal float finalValue;
        internal float sumPercentAdd;
        #endregion

        public readonly ReadOnlyCollection<StatModifier> statModifiers;

        #region Properties
        private DirtyFloat currentValue;
        public DirtyFloat BaseValue { get; protected set; }
        public DirtyFloat CurrentValue {
            get {
                if (CurrentValue.NeedsRecalculation) {
                    CalculateAndSetCurrentValues();
                }
                return currentValue;
            }
            protected set { currentValue = value; }
        }

        protected readonly List<StatModifier> modifiers;
        #endregion  

        #region Constructors
        public CharacterStat() {
            BaseValue = new(IS_NOT_CLAMPED);
            CurrentValue = new(IS_CLAMPED);

            modifiers = new();
            statModifiers = modifiers.AsReadOnly();
        }
        #endregion

        #region Interfaces
        public virtual bool AddModifier(StatModifier modifier) {
            if (modifier == null)
                return false;
            if (modifier.type == StatModType.Permanent)
                return AddPermanentModifier(modifier);
            modifiers.Add(modifier);
            modifiers.Sort(CompareModifier_Order_Purpose);
            CurrentValue.IsNotUpToDate();
            return true;
        }
        public virtual bool RemoveModifier(StatModifier modifier) {
            if (modifier == null)
                return false;
            if (modifier.type == StatModType.Permanent)
                return RemovePermanentModifier(modifier);
            if (!modifiers.Contains(modifier))
                return false;
            if (modifiers.Remove(modifier)) {
                CurrentValue.IsNotUpToDate();
                return true;
            }
            return false;
        }
        public virtual bool AddPermanentModifier(StatModifier modifier) {
            switch (modifier.purpose) {
                case StatModPurpose.Value:
                    ChangeBaseValue(modifier.value);
                    return true;
                case StatModPurpose.MinValue:
                    ChangeBaseMinValue(modifier.value);
                    return true;
                case StatModPurpose.MaxValue:
                    ChangeBaseMaxValue(modifier.value);
                    return true;
                default:
                    return false;
            }
        }
        public virtual bool RemovePermanentModifier(StatModifier modifier) {
            switch (modifier.purpose) {
                case StatModPurpose.Value:
                    ChangeBaseValue(-modifier.value);
                    return true;
                case StatModPurpose.MinValue:
                    ChangeBaseMinValue(-modifier.value);
                    return true;
                case StatModPurpose.MaxValue:
                    ChangeBaseMaxValue(-modifier.value);
                    return true;
                default:
                    return false;
            }
        }
        public virtual bool RemoveAllModifiersFromSource(object source) {
            bool didRemove = false;
            for (int index = modifiers.Count - 1; index >= 0; index--) {
                StatModifier modifier = modifiers[index];
                if (modifier.source == source) {
                    didRemove = true;
                    CurrentValue.IsNotUpToDate();
                    modifiers.RemoveAt(index);
                }
            }
            return didRemove;
        }
        #endregion

        #region Internal Methods
        internal void ChangeBaseValue(float value) {
            BaseValue = value;
            CurrentValue.IsNotUpToDate();
        }
        internal void ChangeBaseMinValue(float value) {
            BaseValue.Min = value;
            CurrentValue.IsNotUpToDate();
        }
        internal void ChangeBaseMaxValue(float value) {
            BaseValue.Max = value;
            CurrentValue.IsNotUpToDate();
        }
        internal int CompareModifier_Order_Purpose(StatModifier a, StatModifier b) {
            if (a.order < b.order)
                return -1;
            if (a.order > b.order)
                return 1;
            if (a.purpose < b.purpose)
                return -1;
            if (a.purpose > b.purpose)
                return 1;
            return 0;
        }
        internal void CalculateAndSetCurrentValues() {

            finalMinValue = BaseValue.Min;
            finalMaxValue = BaseValue.Max;
            finalValue = BaseValue;

            sumPercentAdd = 0f;

            if (modifiers.Count > 0) {
                HandleModifiers();
            }

            CurrentValue.Min = (float)Math.Round(finalMinValue, 4);
            CurrentValue.Max = (float)Math.Round(finalMaxValue, 4);
            CurrentValue = (float)Math.Round(finalValue, 4);

            CurrentValue.IsUpToDate();
        }
        internal void HandleModifiers() {
            for (int index = 0; index < modifiers.Count; index++) {
                ProcessModifierByType(index);
            }
        }
        internal void ProcessModifierByType(int index) {
            StatModifier modifier = modifiers[index];
            switch (modifier.type) {
                case StatModType.Flat:
                    AddToFinalValue(modifier.purpose, modifier.value);
                    break;
                case StatModType.PercentMult:
                    MultiplyWithFinalValue(modifier.purpose, modifier.value);
                    break;
                case StatModType.PercentAdd:
                    AddToSumPercentAdd(index, modifier);
                    break;
                default:
                    break;
            }
        }
        internal void AddToFinalValue(StatModPurpose purpose, float value) {
            switch (purpose) {
                case StatModPurpose.Value:
                    finalValue += value;
                    break;
                case StatModPurpose.MinValue:
                    finalMinValue += value;
                    break;
                case StatModPurpose.MaxValue:
                    finalMaxValue += value;
                    break;
                default:
                    break;
            }
        }
        internal void MultiplyWithFinalValue(StatModPurpose purpose, float value) {
            switch (purpose) {
                case StatModPurpose.Value:
                    finalValue *= (1f + value);
                    break;
                case StatModPurpose.MinValue:
                    finalMinValue *= (1f + value);
                    break;
                case StatModPurpose.MaxValue:
                    finalMaxValue *= (1f + value);
                    break;
                default:
                    break;

            }
        }
        internal void AddToSumPercentAdd(int index, StatModifier modifier) {
            sumPercentAdd += modifier.value;
            if (IsLastElement(index) || DifferentNextType(index) || DifferentNextPurpose(index)) {
                MultiplyWithFinalValue(modifier.purpose, sumPercentAdd);
                sumPercentAdd = 0f;
            }
        }
        internal bool IsLastElement(int index) => index == modifiers.Count - 1;
        internal bool DifferentNextType(int index) => !(modifiers[index].type == modifiers[index + 1].type);
        internal bool DifferentNextPurpose(int index) => !(modifiers[index].purpose == modifiers[index + 1].purpose);
        #endregion
    }
}