using System;
using System.Collections.Generic;
using System.Reflection;

namespace Assets.Scripts.Utils
{
    public class TypeFinder
    {
        public static IList<Type> GetDerivedTypes<T>()
        {
            var matchType = typeof(T);
            var foundTypes = new List<Type>();

            LookupAllAssemblyTypes((assembly, type, exception) =>
            {
                if (type != null && matchType != type && matchType.IsAssignableFrom(type))
                {
                    foundTypes.Add(type);
                }
                return true;
            });
            
            return foundTypes;
        }

        public static IList<Type> GetDecoratedTypes<T>() where T : Attribute
        {
            var matchType = typeof(T);
            var foundTypes = new List<Type>();

            LookupAllAssemblyTypes((assembly, type, exception) =>
            {
                if (type != null && type.GetCustomAttributes(matchType, true).Length > 0)
                {
                    foundTypes.Add(type);
                }
                return true;
            });

            return foundTypes;
        }

        public static void LookupAllAssemblyTypes(Func<Assembly, Type, Exception, bool> EnumeratorAction)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                var assembly = assemblies[i];
                Type[] types;
                Exception exception = null;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    types = ex.Types;
                    exception = ex;
                }

                for (int j = 0; j < types.Length; j++)
                {
                    if (!EnumeratorAction(assembly, types[j], exception))
                    {
                        return;
                    }
                }
            }

        }
    }
}
