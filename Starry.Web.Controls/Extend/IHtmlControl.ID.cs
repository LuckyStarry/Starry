using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHtmlControlExtend
    {
        public static TControl ID<TControl>(this TControl control, string id) where TControl : Interface.IHtmlControl
        {
            control.ID = id;
            return control;
        }

        public static string ID<TControl>(this TControl control) where TControl : Interface.IHtmlControl
        {
            return control.ID;
        }
    }
}
