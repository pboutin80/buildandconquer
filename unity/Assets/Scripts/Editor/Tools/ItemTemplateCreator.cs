using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Utils;
using UnityEditor.Callbacks;
using System.IO;
using Assets.Scripts.Editor.Config;
using Core.Attributes;

namespace Tools
{
    public class ItemTemplateCreator : EditorWindow
    {
        private readonly TemplateCreatorGUI templateCreator = new TemplateCreatorGUI();
        private readonly TypeSelector typeSelector = new TypeSelector();
        private TemplateEditor templateEditor;
        private IEditorConfig editorConfig;
        private GameObject template;

        [MenuItem("Tools/Template Creator...")]
        public static void GetWindow()
        {
            GetWindow<ItemTemplateCreator>();
        }

        [DidReloadScripts]
        static void RefreshTemplateList()
        {

        }

        public ItemTemplateCreator()
        {
            typeSelector.SetTypes(TypeFinder.GetDecoratedTypes<ConfigurationInterfaceAttribute>());
        }

        void OnGUI()
        {
            template = templateCreator.Create();

            Type componenType;
            if (typeSelector.SelectComponentType(out componenType))
            {
                //template.AddComponent(TypeBuilder.CreateFromInterface(componenType));
            }

            if (templateEditor != null)
            {
                templateEditor.OnGUI();
            }
        }
    }

    abstract class CreatorBaseGUI
    {
        public abstract void OnGUI();
    }

    class TemplateCreatorGUI
    {
        private string nameField;
        private string descField;

        public GameObject Create()
        {
            GameObject template = null;
            using (new EditorGUILayout.VerticalScope(GUI.skin.box, GUILayout.Width(400F)))
            {
                nameField = EditorGUILayout.DelayedTextField("Name", nameField, GUILayout.Width(400F));

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Create"))
                    {
                        template = new GameObject(nameField);
                    }
                }
            }
            return template;
        }
    }

    class TypeSelector
    {
        private IList<Type> typeList;
        private string[] typeNames;
        private int selectionIndex;

        public void SetTypes(IList<Type> types)
        {
            typeList = types;
            typeNames = types.Select(t => t.Name).ToArray();
            selectionIndex = Mathf.Clamp(selectionIndex, 0, types.Count - 1);
        }

        public bool SelectComponentType(out Type selectedType)
        {
            selectedType = null;
            using (new EditorGUILayout.HorizontalScope())
            {
                selectionIndex = EditorGUILayout.Popup(selectionIndex, typeNames, GUILayout.Height(25f));
                if (GUILayout.Button("Create"))
                {
                    selectedType = typeList[selectionIndex];
                    return true;
                }
            }
            return false;
        }
    }

    class TemplateEditor : CreatorBaseGUI
    {
        private readonly IEditorConfig editorConfig;
        private readonly EditorWindow editorWindow;
        private Type type;
        private Editor typeEditor;
        private GameObject tempObject;

        public TemplateEditor(IEditorConfig config, EditorWindow window)
        {
            editorConfig = config;
            editorWindow = window;
        }

        public void SetType(Type itemTemplateType)
        {
            if (type == itemTemplateType)
            {
                return;
            }

            type = itemTemplateType;
            var name = GetObjectTemplateName(type);

            var templateFilePath = Path.Combine(editorConfig.ItemTemplatePrefabFolderPath, string.Format("{0}.prefab", name));

            Directory.CreateDirectory(Path.GetDirectoryName(templateFilePath));
            tempObject = PrefabUtility.CreatePrefab(templateFilePath, new GameObject(name, type));
            typeEditor = Editor.CreateEditor(tempObject.GetComponent(type));
        }

        public override void OnGUI()
        {
            if (!typeEditor)
            {
                editorWindow.ShowNotification(new GUIContent("No Item to display, please create one"));
                return;
            }

            using (new EditorGUILayout.VerticalScope())
            {
                typeEditor.OnInspectorGUI();
            }
        }

        private static string GetObjectTemplateName(Type itemTemplateType)
        {
            return string.Format("{0}Prefab", itemTemplateType.Name);
        }
    }

}
