using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry
{
    public static partial class StringExtend
    {
        public static bool IsInt16(this string numeric)
        {
            Int16 o;
            return Int16.TryParse(numeric, out o);
        }

        public static Int16 ToInt16(this string numeric)
        {
            return Int16.Parse(numeric);
        }

        public static Int16 TryToInt16(this string numeric, Int16 defaultValue = 0)
        {
            Int16 o;
            if (Int16.TryParse(numeric, out o))
            {
                return o;
            }
            return defaultValue;
        }

        public static bool IsInt32(this string numeric)
        {
            Int32 o;
            return Int32.TryParse(numeric, out o);
        }

        public static Int32 ToInt32(this string numeric)
        {
            return Int32.Parse(numeric);
        }

        public static Int32 TryToInt32(this string numeric, Int32 defaultValue = 0)
        {
            Int32 o;
            if (Int32.TryParse(numeric, out o))
            {
                return o;
            }
            return defaultValue;
        }

        public static bool IsInt64(this string numeric)
        {
            Int64 o;
            return Int64.TryParse(numeric, out o);
        }

        public static Int64 ToInt64(this string numeric)
        {
            return Int64.Parse(numeric);
        }

        public static Int64 TryToInt64(this string numeric, Int64 defaultValue = 0)
        {
            Int64 o;
            if (Int64.TryParse(numeric, out o))
            {
                return o;
            }
            return defaultValue;
        }
    }
}
