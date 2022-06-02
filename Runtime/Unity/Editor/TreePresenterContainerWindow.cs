using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Unity.Editor
{
    public class TreePresenterContainerWindow : EditorWindow
    {
        private GraphView _graphView;
        
        [MenuItem("Test/Test")]
        private static void CreateWindow()
        {
            CreateWindow<TreePresenterContainerWindow>();
        }

        private void OnEnable()
        {
            _graphView = new TreePresenterGraphView();
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }
    }

    public class TreePresenterGraphView : GraphView
    {
    }
}