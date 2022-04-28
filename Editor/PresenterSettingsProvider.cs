using System.Collections.Generic;
using Unity;
using UnityEditor;

public class PresenterSettingsProvider
{
    public class PresentSettingsProvider : SettingsProvider
    {
        private UnityEditor.Editor _editor;

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            
            var provider = new PresentSettingsProvider("Project/PresentSettings", SettingsScope.Project);
            return provider;
        }

        public PresentSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
            //_editor = UnityEditor.Editor.CreateEditor(PresenterSettings.instance);
        }

        public override void OnGUI(string searchContext)
        {
            _editor.OnInspectorGUI();
        }
    }
}