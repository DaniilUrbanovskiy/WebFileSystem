using System;
using System.ComponentModel;

namespace WebFileSystem.Presentation.Helpers
{
    public static class StringConvert
    {
        public static Nullable<int> ToNullable<T>(this string s) where T : struct
        {
            Nullable<int> result = new Nullable<int>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(int));
                    result = (int)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result;
        }
    }
}
