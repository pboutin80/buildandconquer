
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Managers
{
    public class Managers : MonoBehaviour
    {
        private static readonly Dictionary<Type, MonoBehaviour> s_ManagerMap = new Dictionary<Type, MonoBehaviour>();
        private static GameObject s_GameObject;

        public static T Get<T>() where T : MonoBehaviour
        {
            MonoBehaviour manager = null;
            if (!s_ManagerMap.TryGetValue(typeof(T), out manager))
            {
                manager = s_GameObject.GetComponentInChildren<T>();
                if (manager != null)
                {
                    s_ManagerMap.Add(typeof(T), manager);
                }
            }
            return (T)manager;
        }

        private void Awake()
        {
            s_GameObject = gameObject;
            DontDestroyOnLoad(gameObject);
        }
    }
}
