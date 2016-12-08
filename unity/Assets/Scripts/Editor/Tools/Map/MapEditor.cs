
using Assets.Scripts.Editor.Utils;
using Map;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

using Object = UnityEngine.Object;

namespace Tools.Map
{
    [CustomEditor(typeof(LevelMap))]
    public class MapEditor : Editor
    {
        private Vector2 mItemsScrollPosition;

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

                if (GUILayout.Button("Add New Item"))
                {
                    mEditedLevelMap.MapConfiguration.PropsItems.Add(new PropsItem());
                }

                using (var scrollView = new EditorGUILayout.ScrollViewScope(mItemsScrollPosition, GUILayout.MaxHeight(150F)))
                {
                    mItemsScrollPosition = scrollView.scrollPosition;

                    for (int i = 0; i < mEditedLevelMap.MapConfiguration.PropsItems.Count; i++)
                    {
                        var item = mEditedLevelMap.MapConfiguration.PropsItems[i];
                        DrawPropsItem(item, (propsItem) => { mEditedLevelMap.MapConfiguration.PropsItems.RemoveAt(i); i--; });
                    }
                }

                if (GUILayout.Button("Spawn Items"))
                {

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

                DrawPropertiesExcluding(serializedObject, "m_Script", "LevelTerrain", "m_MapConfiguration");
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPropsItem(PropsItem aItem, Action<PropsItem> aOnRemove)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUILayout.VerticalScope())
                {
                    aItem.Prefab = EditorGUILayout.ObjectField(aItem.Prefab, typeof(GameObject), false) as GameObject;
                }
                using (new EditorGUILayout.VerticalScope())
                {
                    aItem.Quantity = EditorGUILayout.IntField("Quantity", aItem.Quantity);
                }
                if (GUILayout.Button("X"))
                {
                    aOnRemove(aItem);
                }
            }
        }
    }

    public class LevelMapTerrainConfiguration
    {
        public readonly SavedVector3 Size = new SavedVector3("LevelMapTerrainConfiguration.Size", new Vector3(1000f, 600f, 1000f));
        public readonly SavedInt HeightMapResolution = new SavedInt("LevelMapTerrainConfiguration.HeightMapResolution", 512);
        public readonly SavedInt BaseMapResolution = new SavedInt("LevelMapTerrainConfiguration.BaseMapResolution", 1024);
    }
}
