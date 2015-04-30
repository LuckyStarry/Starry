using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class HtmlControlExtend
    {
        public static TControl Attr<TControl>(this TControl control, string name, string value) where TControl : HtmlControl
        {
            control.Attributes[name] = value;
            return control;
        }

        public static TControl RemoveAttr<TControl>(this TControl control, string name) where TControl : HtmlControl
        {
            control.Attributes.Remove(name);
            return control;
        }

        public static TControl Data<TControl>(this TControl control, string name, string value) where TControl : HtmlControl
        {
            name = (name ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(name))
            {
                return control;
            }
            return control.Attr("data-" + name, value);
        }

        public static TControl RemoveData<TControl>(this TControl control, string name) where TControl : HtmlControl
        {
            name = (name ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(name))
            {
                return control;
            }
            return control.RemoveAttr("data-" + name);
        }

        public static TControl Class<TControl>(this TControl control, string name) where TControl : HtmlControl
        {
            if (!control.Classes.Any(@class => @class == name))
            {
                control.Classes.Add(name);
            }
            return control;
        }

        public static TControl RemoveClass<TControl>(this TControl control, string name) where TControl : HtmlControl
        {
            control.Classes.Remove(name);
            return control;
        }

        public static TControl Css<TControl>(this TControl control, string name, string value) where TControl : HtmlControl
        {
            control.Styles[name] = value;
            return control;
        }

        public static TControl RemoveCss<TControl>(this TControl control, string name) where TControl : HtmlControl
        {
            control.Styles.Remove(name);
            return control;
        }

        public static TControl Show<TControl>(this TControl control) where TControl : HtmlControl
        {
            if (control.Styles["display"] == "none")
            {
                control.Styles.Remove("display");
            }
            return control;
        }

        public static TControl Hide<TControl>(this TControl control) where TControl : HtmlControl
        {
            control.Styles["display"] = "none";
            return control;
        }
    }
}
