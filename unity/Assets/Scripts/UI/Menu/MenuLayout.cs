using System;
using System.Collections.Generic;
using System.Reflection;
using UI.Attributes;
using UI.Data;
using UnityEngine;

namespace UI.Menu
{
    public class MenuLayout : MonoBehaviour
    {
        public List<MenuData> Menus;
        [HideInInspector]
        [SerializeField]
        private List<string> m_MenuNames;

        private readonly List<MenuData> m_RegisteredMenuValidation = new List<MenuData>();

        private void OnEnable()
        {
            CreateLayoutFromActions();

            for (int i = 0; i < Menus.Count; i++)
            {
                var menu = Menus[i];
                var menuOnClick = menu.MenuButton.onClick;
                menuOnClick.RemoveAllListeners();
                menuOnClick.AddListener(() => menu.InvokeAction(this));

                if (menu.HasEnabledValidation)
                {
                    m_RegisteredMenuValidation.Add(menu);
                }
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < m_RegisteredMenuValidation.Count; i++)
            {
                var menu = m_RegisteredMenuValidation[i];
                menu.MenuButton.interactable = menu.CheckActionIsEnabled(this);
            }
        }

        public void CreateLayoutFromActions(bool aReset = false)
        {
            var methods = GetGUIActionMethods(GetType());

            if (aReset)
            {
                Menus.Clear();
                m_MenuNames.Clear();
            }

            foreach (var method in methods)
            {
                AddMenuData(Menus, method.Key, method.Value.Name, method.Value.IsValidation);
            }
        }

        private void AddMenuData(List<MenuData> aMenus, MethodInfo aMethodInfo, string aActionName, bool aIsValidation)
        {
            MenuData menu;
            var index = m_MenuNames.IndexOf(aActionName);
            if (index == -1)
            {
                menu = new MenuData();
                m_MenuNames.Add(aActionName);

                aMenus.Add(menu);
            }
            else
            {
                menu = aMenus[index];
            }

            if (!aIsValidation)
            {
                menu.MenuActionName = aMethodInfo.Name;
            }
            else
            {
                menu.MenuActionValidationName = aMethodInfo.Name;
            }
        }

        private Dictionary<MethodInfo, GUIActionAttribute> GetGUIActionMethods(Type aType)
        {
            Dictionary<MethodInfo, GUIActionAttribute> methods = new Dictionary<MethodInfo, GUIActionAttribute>();
            foreach (var method in aType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                var attribute = Attribute.GetCustomAttribute(method, typeof(GUIActionAttribute)) as GUIActionAttribute;
                if (attribute != null)
                {
                    methods.Add(method, attribute);
                }
            }
            return methods;
        }
    }
}
