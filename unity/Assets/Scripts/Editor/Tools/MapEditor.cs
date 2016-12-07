
using Map;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using Object = UnityEngine.Object;

namespace Tools.Map
{
    [CustomEditor(typeof(LevelMap))]
    public class MapEditor : Editor
    {
        private Vector2 mItemsScrollPosition;

        [SerializeField]
        private LevelMapTerrainConfiguration mTerrainConfiguration = new LevelMapTerrainConfiguration();

        private LevelMap mEditedLevelMap;

        [MenuItem("GameObject/Game/LevelMap", priority = 10)]
        public static void CreateLevelMap()
        {
            var name = GameObjectUtility.GetUniqueNameForSibling(null, "LevelMap");
            var newLevel = new GameObject(name, new[] { typeof(LevelMap) });
        }

        private static GameObject CreateTerrain(GameObject aParent, Vector3 aSize, int aHeightMapResolution, int aBaseMapResolution)
        {
            TerrainData terrainData = new TerrainData();
            terrainData.heightmapResolution = 1025;
            terrainData.size = new Vector3(1000f, 600f, 1000f);
            terrainData.heightmapResolution = aHeightMapResolution;
            terrainData.baseMapResolution = aBaseMapResolution;
            terrainData.SetDetailResolution(1024, 16);
            AssetDatabase.CreateAsset(terrainData, AssetDatabase.GenerateUniqueAssetPath("Assets/New Terrain.asset"));
            GameObject gameObject = Terrain.CreateTerrainGameObject(terrainData);
            GameObjectUtility.SetParentAndAlign(gameObject, aParent);
            Selection.activeObject = gameObject;
            Undo.RegisterCreatedObjectUndo(gameObject, "Create terrain");

            return gameObject;
        }

        public override void OnInspectorGUI()
        {
            mEditedLevelMap = target as LevelMap;

            if (mEditedLevelMap == null) return;

            serializedObject.Update();

            using (new EditorGUILayout.HorizontalScope(GUI.skin.box, GUILayout.Height(30F)))
            {
                GUILayout.Label("Level Map Editor", new GUIStyle("label") { fontSize = 12, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter }, GUILayout.ExpandHeight(true));
            }

            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                GUILayout.Label("Items", new GUIStyle("label") { fontSize = 11, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter }, GUILayout.Height(20F));

                using (var scrollView = new EditorGUILayout.ScrollViewScope(mItemsScrollPosition, GUILayout.MaxHeight(150F)))
                {
                    mItemsScrollPosition = scrollView.scrollPosition;

                    //foreach (var item in collection)
                    //{

                    //}

                }










            }

            using (new EditorGUILayout.VerticalScope(GUI.skin.box))
            {
                if (!mEditedLevelMap.LevelTerrain)
                {
                    mTerrainConfiguration.Size = EditorGUILayout.Vector3Field("Terrain Size", mTerrainConfiguration.Size);
                    mTerrainConfiguration.BaseMapResolution = EditorGUILayout.IntField("Base Resolution", mTerrainConfiguration.BaseMapResolution);
                    mTerrainConfiguration.HeightMapResolution = EditorGUILayout.IntField("HeightMap pResolution", mTerrainConfiguration.HeightMapResolution);

                    if (GUILayout.Button("Create Terrain"))
                    {

                    }
                }
                else
                {
                    var levelTerrainProp = serializedObject.FindProperty("LevelTerrain");
                    EditorGUILayout.PropertyField(levelTerrainProp);
                }

                DrawPropertiesExcluding(serializedObject, "m_Script", "LevelTerrain");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    [Serializable]
    public class LevelMapTerrainConfiguration
    {
        public Vector3 Size;
        public int HeightMapResolution;
        public int BaseMapResolution;
    }

    [Serializable]
    public class LevelMapConfiguration
    {
        public List<PropsItem> PropsItems = new List<PropsItem>();
    }

    [Serializable]
    public class PropsItem
    {
        public GameObject Element;
        [Range(0, 1000)]
        public int Quantity = 10;
    }


}
