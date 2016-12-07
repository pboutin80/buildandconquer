using UnityEditor;
using UnityEngine;

namespace CustomDrawers
{
    [CustomPropertyDrawer(typeof(Transform))]
    public class InspectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = "(-) " + label.text;
            base.OnGUI(position, property, label);
        }
    }
}