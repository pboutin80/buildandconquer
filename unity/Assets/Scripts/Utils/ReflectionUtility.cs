using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class ReflectionUtility
    {
        public static IEnumerable<FieldInfo> GetSerializedFields(Type type)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return fields.Where(t => Attribute.IsDefined(t, typeof(SerializeField)) || t.IsPublic);
        }
    }
}
