using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using unityPresenting.Core;
using unityPresenting.Unity;

namespace Unity.Editor
{
    [CustomPropertyDrawer(typeof(ViewKeyProperty))]
    public class MonoViewModelKeyPropertyDrawer : PropertyDrawer
    {
        private static List<ViewData> _buffer = new List<ViewData>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                throw new UnityException($"Not valid property type : {property.propertyType}. Required : {SerializedPropertyType.String}");
            }

            var content = new GUIContent(property.stringValue);
            var otherRect = EditorGUI.PrefixLabel(position, label);
            if (EditorGUI.DropdownButton(otherRect, content, FocusType.Keyboard))
            {
                var genericMenu = new GenericMenu();
                _buffer.Clear();
                var attr = attribute as ViewKeyProperty;
                foreach (var variable in PresenterSettings.instance.ViewResolver.ReadPresenterData(_buffer))
                {
                    if (attr.ViewType.IsAssignableFrom(variable.view.GetType()))
                    {
                        genericMenu.AddItem(new GUIContent(variable.Key), false, () =>
                        {
                            property.stringValue = variable.Key;
                            property.serializedObject.ApplyModifiedProperties();
                        });
                    }
                }
                genericMenu.DropDown(position);
            }
        }
    }
    
    [CustomPropertyDrawer(typeof(PresenterKeyProperty))]
    public class PresenterKeyPropertyDrawer : PropertyDrawer
    {
        private static List<PresenterData> _buffer = new List<PresenterData>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                throw new UnityException($"Not valid property type : {property.propertyType}. Required : {SerializedPropertyType.String}");
            }

            var attr = attribute as PresenterKeyProperty;
            var modelType = attr.ModelType;
            var viewType = attr.ViewType;
            var content = new GUIContent(property.stringValue);
            var otherRect = EditorGUI.PrefixLabel(position, label);
            if (EditorGUI.DropdownButton(otherRect, content, FocusType.Keyboard))
            {
                var genericMenu = new GenericMenu();
                _buffer.Clear();
                genericMenu.AddItem(new GUIContent("Null"), false, () =>
                {
                    property.stringValue = "Null";
                    property.serializedObject.ApplyModifiedProperties();
                });
                foreach (var variable in PresenterSettings.instance.PresenterResolver.ReadPresenterData(_buffer))
                {
                    if (modelType.IsAssignableFrom(variable.Presenter.GetModelType()) && viewType.IsAssignableFrom(variable.Presenter.GetViewType()))
                    {
                        genericMenu.AddItem(new GUIContent(variable.Key), false, () =>
                        {
                            property.stringValue = variable.Key;
                            property.serializedObject.ApplyModifiedProperties();
                        });
                    }
                }
                genericMenu.DropDown(position);
            }
        }
    }
}