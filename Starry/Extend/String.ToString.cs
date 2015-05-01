using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry
{
    public static partial class StringExtend
    {
        public static string ToString(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
