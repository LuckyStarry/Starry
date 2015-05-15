using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IMethodExtend
    {
        public static TControl Method<TControl>(this TControl control, string method) where TControl : Attributes.IMethod
        {
            return control.Attr("method", method);
        }

        public static string Method<TControl>(this TControl control) where TControl : Attributes.IMethod
        {
            return control.Attr("method");
        }
    }
}
