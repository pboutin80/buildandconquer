using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Editor.Config
{
    public class EditorConfig : ScriptableObject, IEditorConfig
    {
        [SerializeField]
        [Header("Directories")]
        private string m_ItemTemplatePrefabFolderPath;

        public string ItemTemplatePrefabFolderPath { get { return m_ItemTemplatePrefabFolderPath; } }
    }

    internal interface IEditorConfig
    {
        string ItemTemplatePrefabFolderPath { get; }
    }
}
