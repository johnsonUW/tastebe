using System;

namespace Framework.Utils
{
    public static class EnumExtensions
    {
        public static TEnum? ToEnum<TEnum>(this string s) where TEnum : struct
        {
            TEnum enu;
            var success = Enum.TryParse(s, out enu);
            return success ? enu : (TEnum?) null;
        }
    }
}