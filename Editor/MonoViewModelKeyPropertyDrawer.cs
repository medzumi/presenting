﻿using System;
using System.Collections.Generic;
using Unity;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ViewKeyProperty))]
    public class MonoViewModelKeyPropertyDrawer : PropertyDrawer
    {
        private static List<string> _buffer = new List<string>();

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
                foreach (var variable in PresenterSettings.instance.ViewResolver.GetViewModelsKeys(_buffer, attr.ViewType))
                {
                    genericMenu.AddItem(new GUIContent(variable), false, () =>
                    {
                        property.stringValue = variable;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }
                genericMenu.DropDown(position);
            }
        }
    }
    
    [CustomPropertyDrawer(typeof(PresenterKeyProperty))]
    public class PresenterKeyPropertyDrawer : PropertyDrawer
    {
        private static List<string> _buffer = new List<string>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                throw new UnityException($"Not valid property type : {property.propertyType}. Required : {SerializedPropertyType.String}");
            }

            var attr = attribute as PresenterKeyProperty;
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
                foreach (var variable in PresenterSettings.instance.PresenterResolver.GetPresentersKeys(_buffer, attr.ModelType, attr.ViewType))
                {
                    genericMenu.AddItem(new GUIContent(variable), false, () =>
                    {
                        property.stringValue = variable;
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }
                genericMenu.DropDown(position);
            }
        }
    }
}