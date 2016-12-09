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

        private T m_Default;
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

        public void Reset()
        {
            m_Value = m_Default;
            SetValue(m_Name, m_Value);
        }

        protected Saved(string name, T value)
        {
            m_Name = name;
            m_Default = value;
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

        public SavedFloat(string name, float value = 0F) : base(name, value) { }
    }

    public class SavedInt : Saved<int>
    {
        static SavedInt()
        {
            GetValue = EditorPrefs.GetInt;
            SetValue = EditorPrefs.SetInt;
        }

        public SavedInt(string name, int value = 0) : base(name, value) { }
    }

    public class SavedString : Saved<string>
    {
        static SavedString()
        {
            GetValue = EditorPrefs.GetString;
            SetValue = EditorPrefs.SetString;
        }

        public SavedString(string name, string value = null) : base(name, value) { }
    }

    public class SavedVector2 : Saved<Vector2>
    {
        static SavedVector2()
        {
            GetValue = GetVector;
            SetValue = SetVector;
        }

        private static Vector2 GetVector(string name, Vector2 aDefault)
        {
            var savedValue = new SavedString(name, null);
            var splitValues = savedValue.Value.Split(';');

            if (splitValues.Length < 2)
            {
                return aDefault;
            }

            return new Vector2(float.Parse(splitValues[0]), float.Parse(splitValues[1]));
        }

        private static void SetVector(string name, Vector2 aValue)
        {
            var savedValue = new SavedString(name, null);
            savedValue.Value = string.Format("{0};{1}", aValue.x, aValue.y);
        }

        public SavedVector2(string name, Vector2 value) : base(name, value) { }
    }

    public class SavedVector3 : Saved<Vector3>
    {
        static SavedVector3()
        {
            GetValue = GetVector;
            SetValue = SetVector;
        }

        private static Vector3 GetVector(string name, Vector3 aDefault)
        {
            var savedValue = new SavedString(name, null);
            var splitValues = savedValue.Value.Split(';');

            if (splitValues.Length < 3)
            {
                return aDefault;
            }

            return new Vector3(float.Parse(splitValues[0]), float.Parse(splitValues[1]), float.Parse(splitValues[2]));
        }

        private static void SetVector(string name, Vector3 aValue)
        {
            var savedValue = new SavedString(name, null);
            savedValue.Value = string.Format("{0};{1};{2}", aValue.x, aValue.y, aValue.z);
        }

        public SavedVector3(string name, Vector3 value) : base(name, value) { }
    }

    public class SavedVector4 : Saved<Vector4>
    {
        static SavedVector4()
        {
            GetValue = GetVector;
            SetValue = SetVector;
        }

        private static Vector4 GetVector(string name, Vector4 aDefault)
        {
            var savedValue = new SavedString(name, null);
            var splitValues = savedValue.Value.Split(';');

            if (splitValues.Length < 4)
            {
                return aDefault;
            }

            return new Vector4(float.Parse(splitValues[0]), float.Parse(splitValues[1]), float.Parse(splitValues[2]), float.Parse(splitValues[3]));
        }

        private static void SetVector(string name, Vector4 aValue)
        {
            var savedValue = new SavedString(name, null);
            savedValue.Value = string.Format("{0};{1};{2};{3}", aValue.x, aValue.y, aValue.z, aValue.w);
        }

        public SavedVector4(string name, Vector4 value) : base(name, value) { }
    }
}
