using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHtmlElementExtend
    {
        public static TControl Wrap<TControl>(this Interface.IHtmlElement control, TControl wrapper) where TControl : Interface.IHtmlContainerControl
        {
            return wrapper.Append(control);
        }
    }
}
