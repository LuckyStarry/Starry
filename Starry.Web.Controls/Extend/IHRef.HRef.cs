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
            control.HRef = href;
            return control;
        }

        public static string HRef<TControl>(this TControl control) where TControl : Attributes.IHRef
        {
            return control.HRef;
        }
    }
}
