using UnityEditor;
using UnityEngine;
using System.IO;
using Core.Extensions;

namespace Assets.Scripts.Editor
{

    public class EditorTest
    {

        [MenuItem("Tests/Compare Asset Hashes...")]
        public static void CompareHashes()
        {
            var texture = new Texture2D(100, 100);
            Color[] colors = new Color[100 * 100];
            colors.Initialize(Color.blue);
            texture.SetPixels(colors);
            texture.Apply();

            var hash = UnityEditorInternal.InternalEditorUtility.CalculateHashForObjectsAndDependencies(new Object[] { texture });

            Directory.CreateDirectory("Assets/Art/Textures");
            File.WriteAllBytes("Assets/Art/Textures/Test.png", texture.EncodeToPNG());

            AssetDatabase.ImportAsset("Assets/Art/Textures/Test.png");

            var texture2 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Art/Textures/Test.png");

            var hash2 = UnityEditorInternal.InternalEditorUtility.CalculateHashForObjectsAndDependencies(new Object[] { texture2 });

            Debug.Log("Hashes are equal: " + (hash == hash2));
        }

    }
}
