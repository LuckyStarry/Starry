using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IActionExtend
    {
        public static TControl Action<TControl>(this TControl control, string action) where TControl : Attributes.IAction
        {
            return control.Attr("action", action);
        }

        public static string Action<TControl>(this TControl control) where TControl : Attributes.IAction
        {
            return control.Attr("action");
        }
    }
}
