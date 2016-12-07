using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.Utils
{
    public delegate T ValueGetAction<T>(string name, T defaultValue);
    public delegate void ValueSetAction<T>(string name, T value);

    public abstract class Saved<T>
    {
        protected static ValueGetAction<T> GetValue;
        protected static ValueSetAction<T> SetValue;

        private T m_Value;
        private string m_Name;

        public T Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                if (m_Value.Equals(value))
                {
                    return;
                }

                m_Value = value;
                SetValue(m_Name, value);
            }
        }

        protected Saved(string name, T value)
        {
            m_Name = name;
            m_Value = GetValue(name, value);
        }

        public static implicit operator T(Saved<T> s)
        {
            return s.Value;
        }
    }

    public class SavedFloat : Saved<float>
    {
        static SavedFloat()
        {
            GetValue = EditorPrefs.GetFloat;
            SetValue = EditorPrefs.SetFloat;
        }

        public SavedFloat(string name, float value) : base(name, value) { }
    }

    public class SavedInt : Saved<int>
    {
        static SavedInt()
        {
            GetValue = EditorPrefs.GetInt;
            SetValue = EditorPrefs.SetInt;
        }

        public SavedInt(string name, int value) : base(name, value) { }
    }

    public class SavedString : Saved<string>
    {
        static SavedString()
        {
            GetValue = EditorPrefs.GetString;
            SetValue = EditorPrefs.SetString;
        }

        public SavedString(string name, string value) : base(name, value) { }
    }

    public class SavedVector3 : Saved<Vector3>
    {
        static SavedVector3()
        {
            GetValue = GetVector3;
            SetValue = SetVector3;
        }

        private static Vector3 GetVector3(string name, Vector3 aDefault)
        {
            var savedValue = new SavedString(name, null);
            var splitValues = savedValue.Value.Split(';');

            return new Vector3(float.Parse(splitValues[0]), float.Parse(splitValues[1]), float.Parse(splitValues[2]));
        }

        private static void SetVector3(string name, Vector3 aValue)
        {
            var savedValue = new SavedString(name, null);
            savedValue.Value = string.Format("{0};{1};{2}", aValue.x, aValue.y, aValue.z);
        }

        public SavedVector3(string name, Vector3 value) : base(name, value) { }
    }
}
