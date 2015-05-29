using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHtmlControlExtend
    {
        public static TControl Css<TControl>(this TControl control, string name, string value) where TControl : Interface.IHtmlControl
        {
            control.Styles[name] = value;
            return control;
        }

        public static TControl RemoveCss<TControl>(this TControl control, string name) where TControl : Interface.IHtmlControl
        {
            control.Styles.Remove(name);
            return control;
        }
    }
}
