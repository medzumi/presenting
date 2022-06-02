using System.Collections.Generic;
using UnityEditor;
using unityPresenting.Unity;

namespace Unity.Editor
{
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
                InitializeEditor();
            }

            private async void InitializeEditor()
            {
                _editor = UnityEditor.Editor.CreateEditor(await PresenterSettings.GetInstanceAsync());
            }

            public override void OnGUI(string searchContext)
            {
                if(_editor)
                    _editor.OnInspectorGUI();
            }
        }
    }
}