using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlUL : HtmlContainerControl
    {
        public HtmlUL() : base("ul") { }
        public HtmlUL(string text) : base("ul", text) { }
        public HtmlUL(params HtmlLI[] innerElements) : base("ul", innerElements) { }
    }
}
