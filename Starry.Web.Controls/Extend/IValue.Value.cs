using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IValueExtend
    {
        public static TControl Value<TControl>(this TControl control, string value) where TControl : Attributes.IValue
        {
            return control.Attr("value", value);
        }

        public static string Value<TControl>(this TControl control) where TControl : Attributes.IValue
        {
            return control.Attr("value");
        }
    }
}
