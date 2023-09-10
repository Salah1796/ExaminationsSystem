using System;
using System.ComponentModel;
using System.Linq;

namespace ExaminationsSystem.Application.Common.Extensions
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            return value.GetDescription("");
        }
        public static string GetDescription(this Enum value, params string?[]? extra)
        {
            DescriptionAttribute[] source = value.GetType().GetField(value.ToString())!.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false) as DescriptionAttribute[];
            if (source.Any())
            {
                if (extra == null || extra!.Length == 0)
                {
                    return source.Single().Description;
                }

                return string.Format(source.Single().Description, extra);
            }

            return value.ToString();
        }
    }
}
