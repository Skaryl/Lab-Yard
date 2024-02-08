#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace LecureyGames.Utilities {
    [CustomPropertyDrawer(typeof(PositiveIntAttribute))]
    public class PositiveIntDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.propertyType == SerializedPropertyType.Integer) {
                int value = EditorGUI.IntField(position, label, Mathf.Max(property.intValue, 1));
                property.intValue = Mathf.Max(value, 1);
            } else {
                EditorGUI.LabelField(position, label.text, "Use PositiveInt with integer fields only.");
            }
        }
    }
    public class PositiveIntAttribute : PropertyAttribute { }

    [CustomPropertyDrawer(typeof(PositiveIntWithZeroAttribute))]
    public class PositiveIntWithZeroDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.propertyType == SerializedPropertyType.Integer) {
                int value = EditorGUI.IntField(position, label, Mathf.Max(property.intValue, 0));
                property.intValue = Mathf.Max(value, 0);
            } else {
                EditorGUI.LabelField(position, label.text, "Use PositiveIntWithZero with integer fields only.");
            }
        }
    }
    public class PositiveIntWithZeroAttribute : PropertyAttribute { }

    [CustomPropertyDrawer(typeof(PositiveFloatAttribute))]
    public class PositiveFloatDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (property.propertyType == SerializedPropertyType.Float) {
                float value = EditorGUI.FloatField(position, label, Mathf.Max(property.floatValue, 0f));
                property.floatValue = Mathf.Max(value, 0f);
            } else {
                EditorGUI.LabelField(position, label.text, "Use PositiveFloat with float fields only.");
            }
        }
    }
    public class PositiveFloatAttribute : PropertyAttribute { }
}
#endif