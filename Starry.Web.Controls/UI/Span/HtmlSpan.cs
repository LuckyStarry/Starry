using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlSpan : HtmlContainerControl
    {
        public HtmlSpan() : base("span") { }
        public HtmlSpan(string text) : base("span", text) { }
        public HtmlSpan(params HtmlElement[] innerElements) : base("span", innerElements) { }
    }
}
