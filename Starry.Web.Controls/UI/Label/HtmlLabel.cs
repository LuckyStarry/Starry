using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlLabel : HtmlContainerControl
    {
        public HtmlLabel() : base("label") { }
        public HtmlLabel(string text) : base("label", text) { }
        public HtmlLabel(params HtmlElement[] innerElements) : base("label", innerElements) { }
    }
}
