using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IReadOnlyExtend
    {
        public static TControl ReadOnly<TControl>(this TControl control, bool readOnly) where TControl : Attributes.IReadOnly
        {
            control.ReadOnly = readOnly;
            return control;
        }

        public static bool ReadOnly<TControl>(this TControl control) where TControl : Attributes.IReadOnly
        {
            return control.ReadOnly;
        }
    }
}
