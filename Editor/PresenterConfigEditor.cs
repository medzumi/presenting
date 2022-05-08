using Game.CoreLogic;
using Unity;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(AbstractConfigs), true)]
    public class PresenterConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Validate"))
            {
                var config = target as AbstractConfigs;
                Debug.LogError("Doesn't work");
            }
        }
    }
}