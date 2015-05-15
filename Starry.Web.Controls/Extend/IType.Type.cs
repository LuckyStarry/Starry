using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class ITypeExtend
    {
        public static TControl Type<TControl>(this TControl control, string type) where TControl : Attributes.IType
        {
            return control.Attr("type", type);
        }

        public static string Type<TControl>(this TControl control) where TControl : Attributes.IType
        {
            return control.Attr("type");
        }
    }
}
