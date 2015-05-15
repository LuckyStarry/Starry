using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHRefExtend
    {
        public static TControl HRef<TControl>(this TControl control, string href) where TControl : Attributes.IHRef
        {
            return control.Attr("href", href);
        }

        public static string HRef<TControl>(this TControl control) where TControl : Attributes.IHRef
        {
            return control.Attr("href");
        }
    }
}
