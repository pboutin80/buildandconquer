using System;
using System.Collections.Generic;
using System.Reflection;
using UI.Menu;
using UnityEngine.UI;

namespace UI.Data
{
    [Serializable]
    public class MenuData
    {
        private static readonly Dictionary<Type, Dictionary<string, MethodInfo>> s_MethodCache 
            = new Dictionary<Type, Dictionary<string, MethodInfo>>();

        public Button MenuButton;
        public string MenuActionName;
        public string MenuActionValidationName;

        private static bool GetOrAddCachedMethod(MenuLayout aTarget, string aMethodName, out MethodInfo aMethod)
        {
            var targetType = aTarget.GetType();

            Dictionary<string, MethodInfo> methodMap;
            if (!s_MethodCache.TryGetValue(targetType, out methodMap))
            {
                methodMap = new Dictionary<string, MethodInfo>();
                s_MethodCache.Add(targetType, methodMap);
            }

            if (!methodMap.TryGetValue(aMethodName, out aMethod))
            {
                aMethod = targetType.GetMethod(aMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                methodMap.Add(aMethodName, aMethod);
            }

            return aMethod != null;
        }

        public bool HasEnabledValidation { get { return !string.IsNullOrEmpty(MenuActionValidationName); } }

        public void InvokeAction(MenuLayout aTarget)
        {
            if (string.IsNullOrEmpty(MenuActionName) || aTarget == null)
            {
                return;
            }

            MethodInfo method;
            if (GetOrAddCachedMethod(aTarget, MenuActionName, out method))
            {
                method.Invoke(aTarget, null);
            }
        }

        public bool CheckActionIsEnabled(MenuLayout aTarget)
        {
            if (string.IsNullOrEmpty(MenuActionValidationName) || aTarget == null)
            {
                return true;
            }

            MethodInfo method;
            if (GetOrAddCachedMethod(aTarget, MenuActionValidationName, out method))
            {
                return (bool)method.Invoke(aTarget, null);
            }
            return true;
        }
    }
}
