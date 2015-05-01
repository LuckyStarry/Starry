using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry
{
    public static partial class StringBuilderExtend
    {
        public static StringBuilder AppendLine(this StringBuilder sb, string format, params object[] args)
        {
            return sb.AppendLine(string.Format(format, args));
        }
    }
}
