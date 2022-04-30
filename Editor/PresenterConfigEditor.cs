using Game.CoreLogic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(AbstractEcsConfigs), true)]
    public class PresenterConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Validate"))
            {
                var config = target as AbstractEcsConfigs;
                Debug.LogError("Doesn't work");
            }
        }
    }
}