using System;
using System.Linq;

namespace Main.CodeBase.Tools
{
    public static class EnumHelper
    {
        public static T[] GetValues<T>() where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }
    
        public static int GetCount<T>() where T : Enum
        {
            return GetValues<T>().Length;
        }
        
        public static T GetMaxEnumValue<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Max();
        }
    }
}