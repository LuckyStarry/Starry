using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHtmlControlExtend
    {
        public static TControl Attr<TControl>(this TControl control, string name, object value) where TControl : Interface.IHtmlControl
        {
            return control.Attr(name, value == null ? string.Empty : value.ToString());
        }

        public static TControl Attr<TControl>(this TControl control, string name, string value) where TControl : Interface.IHtmlControl
        {
            control.Attributes[name] = value;
            return control;
        }

        public static string Attr<TControl>(this TControl control, string name) where TControl : Interface.IHtmlControl
        {
            return control.Attributes[name];
        }

        public static TControl RemoveAttr<TControl>(this TControl control, string name) where TControl : Interface.IHtmlControl
        {
            control.Attributes.Remove(name);
            return control;
        }
    }
}
