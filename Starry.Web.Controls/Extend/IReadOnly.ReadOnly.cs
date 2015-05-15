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
            if (readOnly)
            {
                return control.Attr("readonly", "readonly");
            }
            else
            {
                return control.RemoveAttr("readonly");
            }
        }

        public static bool ReadOnly<TControl>(this TControl control) where TControl : Attributes.IReadOnly
        {
            return !string.IsNullOrEmpty(control.Attr("readonly").Trim());
        }
    }
}
