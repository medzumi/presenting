using System;
using UnityEditor;
using UnityEngine;
using unityPresenting.Unity;

namespace Unity.Editor
{
    [CustomPropertyDrawer(typeof(GuidStringKey))]
    public class GuidStringKeyPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                throw new Exception("property isn't string");
            }

            position = EditorGUI.PrefixLabel(position, label);
            var labelPosition = position;
            labelPosition.Set(labelPosition.x, labelPosition.y, labelPosition.width/2f, labelPosition.height);
            EditorGUI.LabelField(labelPosition, property.stringValue);
            var buttonPosition = labelPosition;
            buttonPosition.Set(buttonPosition.x + buttonPosition.width, buttonPosition.y, buttonPosition.width, buttonPosition.height);
            if (GUI.Button(buttonPosition, "Regenerate"))
            {
                property.stringValue = Guid.NewGuid().ToString();
            }

        }
    }
}