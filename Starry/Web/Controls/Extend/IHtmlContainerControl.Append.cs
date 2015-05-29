using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHtmlContainerControlExtend
    {
        public static TControl Append<TControl>(this TControl control, params Interface.IHtmlElement[] elements)
            where TControl : Interface.IHtmlContainerControl
        {
            foreach (var element in elements ?? new HtmlElement[0])
            {
                if (element != null)
                {
                    control.Children.Add(element);
                }
            }
            return control;
        }
    }
}
