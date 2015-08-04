using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Web.Controls
{
    public static partial class HtmlRenderHelper
    {
        public static string ToHtmlString(this Starry.Web.Controls.Interface.IHtmlElement htmlElement)
        {
            return new Starry.Web.Controls.HtmlElementRender().ToHtmlString(htmlElement);
        }
    }
}
