
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class LevelMap : MonoBehaviour
    {
        [SerializeField]
        private LevelMapConfiguration m_MapConfiguration;
        public Terrain LevelTerrain;

        public LevelMapConfiguration MapConfiguration { get { return m_MapConfiguration; } }

        void Awake()
        {
            if (m_MapConfiguration == null)
            {
                m_MapConfiguration = ScriptableObject.CreateInstance<LevelMapConfiguration>();
            }
        }
    }

    public class LevelMapConfiguration : ScriptableObject
    {
        [SerializeField]
        private List<PropsItem> mPropsItems = new List<PropsItem>();

        public List<PropsItem> PropsItems
        {
            get { return mPropsItems; }
        }
    }

    [Serializable]
    public class PropsItem
    {
        public GameObject Prefab;
        [Range(0, 1000)]
        public int Quantity = 10;

        public string Name { get { return Prefab ? Prefab.name : "[No Prefab]"; } }
    }


}
