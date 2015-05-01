using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    internal class HtmlElementStyle : Interface.IHtmlElementStyle
    {
        public string Name { set; get; }
        public string Value { set; get; }
    }
}
