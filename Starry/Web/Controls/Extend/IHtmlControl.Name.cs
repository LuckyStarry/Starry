using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHtmlControlExtend
    {
        public static TControl Name<TControl>(this TControl control, string name) where TControl : Interface.IHtmlControl
        {
            control.Name = name;
            return control;
        }

        public static string Name<TControl>(this TControl control) where TControl : Interface.IHtmlControl
        {
            return control.Name;
        }
    }
}
