﻿
using Assets.Scripts.Editor.Utils;
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
            terrainData.size = aSize;
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
                    mTerrainConfiguration.Size.Value = EditorGUILayout.Vector3Field("Terrain Size", mTerrainConfiguration.Size.Value);
                    mTerrainConfiguration.BaseMapResolution.Value = EditorGUILayout.IntField("Base Resolution", mTerrainConfiguration.BaseMapResolution.Value);
                    mTerrainConfiguration.HeightMapResolution.Value = EditorGUILayout.IntField("HeightMap pResolution", mTerrainConfiguration.HeightMapResolution.Value);

                    if (GUILayout.Button("Create Terrain"))
                    {
                        var terrainObject = CreateTerrain(mEditedLevelMap.gameObject, 
                            mTerrainConfiguration.Size.Value, 
                            mTerrainConfiguration.HeightMapResolution.Value, 
                            mTerrainConfiguration.BaseMapResolution.Value);

                        mEditedLevelMap.LevelTerrain = terrainObject.GetComponent<Terrain>();
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

    public class LevelMapTerrainConfiguration
    {
        public readonly SavedVector3 Size = new SavedVector3("LevelMapTerrainConfiguration.Size", new Vector3(1000f, 600f, 1000f));
        public readonly SavedInt HeightMapResolution = new SavedInt("LevelMapTerrainConfiguration.HeightMapResolution", 512);
        public readonly SavedInt BaseMapResolution = new SavedInt("LevelMapTerrainConfiguration.BaseMapResolution", 1024);
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
