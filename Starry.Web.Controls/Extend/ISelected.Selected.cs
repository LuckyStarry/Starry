using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class ISelectedExtend
    {
        public static TControl Selected<TControl>(this TControl control, bool selected) where TControl : Attributes.ISelected
        {
            control.Selected = selected;
            return control;
        }

        public static bool Selected<TControl>(this TControl control) where TControl : Attributes.ISelected
        {
            return control.Selected;
        }
    }
}
