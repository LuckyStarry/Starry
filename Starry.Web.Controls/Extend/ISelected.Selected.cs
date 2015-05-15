using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class ISelectedExtend
    {
        public static TControl Selected<TControl>(this TControl control, bool readOnly) where TControl : Attributes.ISelected
        {
            if (readOnly)
            {
                return control.Attr("selected", "selected");
            }
            else
            {
                return control.RemoveAttr("selected");
            }
        }

        public static bool Selected<TControl>(this TControl control) where TControl : Attributes.ISelected
        {
            return !string.IsNullOrEmpty(control.Attr("selected").Trim());
        }
    }
}
