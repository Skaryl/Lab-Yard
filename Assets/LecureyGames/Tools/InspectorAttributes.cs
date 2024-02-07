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
}
#endif