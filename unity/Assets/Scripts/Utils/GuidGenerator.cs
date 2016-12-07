using System;

namespace Assets.Scripts.Utils
{
    public class GuidGenerator
    {
        static public string New()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
