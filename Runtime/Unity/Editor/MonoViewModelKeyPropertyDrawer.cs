using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using unityPresenting.Core;
using unityPresenting.Unity;
using Utilities.Unity.Extensions;

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
                /*foreach (var variable in PresenterSettings.instance.ViewResolver.ReadData(_buffer))
                {
                    if (attr.ViewType.IsAssignableFrom(variable.view.GetType()))
                    {
                        genericMenu.AddItem(new GUIContent(variable.Key), false, () =>
                        {
                            property.stringValue = variable.Key;
                            property.serializedObject.ApplyModifiedProperties();
                        });
                    }
                }*/
                genericMenu.DropDown(position);
            }
        }
    }
    
    [CustomPropertyDrawer(typeof(PresenterKeyProperty))]
    public class PresenterKeyPropertyDrawer : PropertyDrawer
    {
        private static List<PresenterSource> _buffer = new List<PresenterSource>();
        private readonly Dictionary<string, AbstractPresentersContainer> _keys = new Dictionary<string, AbstractPresentersContainer>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                throw new UnityException($"Not valid property type : {property.propertyType}. Required : {SerializedPropertyType.String}");
            }

            if (!_keys.TryGetValue(property.stringValue, out var source))
            {
                _buffer.Clear();
              //  PresenterSettings.instance.PresentersContainer.ReadPresenterRegistrators(_buffer);
                source = _buffer.FirstOrDefault(data => string.Equals(data.Key, property.stringValue)).Source;

                _keys[property.stringValue] = source;
            }

            var attr = attribute as PresenterKeyProperty;
            var modelType = attr.ModelType;
            var viewType = attr.ViewType;
            
            var content = new GUIContent(source?.GetPresenterNameByKey(property.stringValue));
            var otherRect = EditorGUI.PrefixLabel(position, label);
            var objectRect = new Rect(otherRect.position, otherRect.size - new Vector2(50, 0));
            otherRect = new Rect(otherRect.position + new Vector2(objectRect.size.x, 0),
                new Vector2(50, otherRect.size.y));
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.ObjectField(objectRect, source, typeof(AbstractPresentersContainer));
            EditorGUI.EndDisabledGroup();
            if (EditorGUI.DropdownButton(otherRect, content, FocusType.Keyboard))
            {
                var genericMenu = new GenericMenu();
                _buffer.Clear();
                genericMenu.AddItem(new GUIContent("Null"), false, () =>
                {
                    property.stringValue = String.Empty;
                    property.serializedObject.ApplyModifiedProperties();
                });
             /*   foreach (var variable in PresenterSettings.instance.PresentersContainer.ReadPresenterRegistrators(_buffer))
                {
                    var presenter = variable.Source.GetPresenterByKey(variable.Key);
                    if (presenter.IsNotNullInUnity() && variable.Source && modelType.IsAssignableFrom(presenter.GetModelType()) 
                        && viewType.IsAssignableFrom(presenter.GetViewType()))
                    {
                        genericMenu.AddItem(new GUIContent(variable.Source.GetPresenterNameByKey(variable.Key)), false, () =>
                        {
                            property.stringValue = variable.Key;
                            _keys[variable.Key] = variable.Source;
                            property.serializedObject.ApplyModifiedProperties();
                        });
                    }
                }*/
                genericMenu.DropDown(position);
            }
        }
    }
}