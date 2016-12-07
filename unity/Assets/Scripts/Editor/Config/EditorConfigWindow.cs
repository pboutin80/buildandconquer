using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Assets.Scripts.Editor.Config
{
    public class EditorConfigWindow : EditorWindow
    {
        struct OptionSection
        {
            public Type editorConfigType;
            public string section;
            public List<FieldInfo> options;
            public bool active;
        }

        private static List<OptionSection> optionSections;

        [MenuItem("Tools/Options...")]
        public static void ShowWindow()
        {
            LoadEditorConfig();
            GetWindow<EditorConfigWindow>();
        }

        private static void LoadEditorConfig()
        {
            var editorConfigs = TypeFinder.GetDerivedTypes<IEditorConfig>();

            optionSections = new List<OptionSection>();

            foreach (var editorConfig in editorConfigs)
            {
                foreach (var field in ReflectionUtility.GetSerializedFields(editorConfig))
                {
                    
                } 
            }
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            if (Selection.activeObject is IEditorConfig)
            {
                ShowWindow();
                return true;
            }
            return false;
        }

        private static 

        void OnGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope())
                {
                    
                }
            }
        }
    }
}
