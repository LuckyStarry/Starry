using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlUL : HtmlContainerControl
    {
        public HtmlUL() : base(Controls.Attributes.TagNames.UL) { }
        public HtmlUL(string text) : base(Controls.Attributes.TagNames.UL, text) { }
        public HtmlUL(params HtmlLI[] innerElements) : base(Controls.Attributes.TagNames.UL, innerElements) { }
    }
}
