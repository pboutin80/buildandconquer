using UI.Data;
using UnityEditor;
using UnityEngine;

namespace UI.Menu
{
    [CustomEditor(typeof(MenuLayout), true)]
    public class MenuLayoutInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Create layout from GUIActions"))
            {
                ((MenuLayout)target).CreateLayoutFromActions(true);
            }

            DrawDefaultInspector();
        }
    }
    
    [CustomPropertyDrawer(typeof(MenuData))]
    public class MenuDataPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 35f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var menuObjectProperty = property.FindPropertyRelative("MenuButton");
            var pos = new Rect(position);
            pos.height = 16f;
            EditorGUI.ObjectField(pos, menuObjectProperty);
            pos = new Rect(pos);
            pos.x = EditorGUIUtility.labelWidth;
            pos.y += 16f;
            var menuActionProperty = property.FindPropertyRelative("MenuActionName");
            EditorGUI.LabelField(pos, "Action set to: " + menuActionProperty.stringValue, EditorStyles.helpBox);
        }
    }
}
