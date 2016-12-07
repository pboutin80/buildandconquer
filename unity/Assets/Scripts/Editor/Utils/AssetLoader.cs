using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.Utils
{
    public class AssetLoader
    {
        public static T FindByGuid<T>(string guid) where T : Object
        {
            if (!string.IsNullOrEmpty(guid))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (!string.IsNullOrEmpty(path))
                {
                    return AssetDatabase.LoadAssetAtPath<T>(path);
                }
            }

            return null;
        }

    }
}
