using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHtmlControlExtend
    {
        public static TControl Show<TControl>(this TControl control) where TControl : Interface.IHtmlControl
        {
            if (control.Styles["display"] == "none")
            {
                control.Styles.Remove("display");
            }
            return control;
        }

        public static TControl Hide<TControl>(this TControl control) where TControl : Interface.IHtmlControl
        {
            control.Styles["display"] = "none";
            return control;
        }
    }
}
