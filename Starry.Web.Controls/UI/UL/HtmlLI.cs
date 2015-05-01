using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlLI : HtmlContainerControl
    {
        public HtmlLI() : base("li") { }
        public HtmlLI(string text) : base("li", text) { }
        public HtmlLI(params HtmlElement[] innerElements) : base("li", innerElements) { }
    }
}
