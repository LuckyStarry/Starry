using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHtmlControlExtend
    {
        public static TControl Data<TControl>(this TControl control, string name, object value) where TControl : Interface.IHtmlControl
        {
            return control.Data(name, value == null ? string.Empty : value.ToString());
        }

        public static TControl Data<TControl>(this TControl control, string name, string value) where TControl : Interface.IHtmlControl
        {
            name = (name ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(name))
            {
                return control;
            }
            return control.Attr("data-" + name, value);
        }

        public static TControl RemoveData<TControl>(this TControl control, string name) where TControl : Interface.IHtmlControl
        {
            name = (name ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(name))
            {
                return control;
            }
            return control.RemoveAttr("data-" + name);
        }
    }
}
