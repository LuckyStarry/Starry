using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class ISrcExtend
    {
        public static TControl Src<TControl>(this TControl control, string src) where TControl : Attributes.ISrc
        {
            return control.Attr("src", src);
        }

        public static string Src<TControl>(this TControl control) where TControl : Attributes.ISrc
        {
            return control.Attr("src");
        }
    }
}
