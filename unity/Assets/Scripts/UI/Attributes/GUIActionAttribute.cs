using System;

namespace UI.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GUIActionAttribute : Attribute
    {
        public string Name { get; private set; }
        public bool IsValidation { get; private set; }
        public int Order { get; set; }

        public GUIActionAttribute() 
            : this(null, false)
        {
        }

        public GUIActionAttribute(string aName) 
            : this(aName, false)
        {
        }

        public GUIActionAttribute(string aName, bool aIsValidation)
        {
            Name = aName;
            IsValidation = aIsValidation;
        }
    }
}
