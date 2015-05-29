using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlLabel : HtmlContainerControl
    {
        public HtmlLabel() : base(Controls.Attributes.TagNames.Label) { }
        public HtmlLabel(string text) : base(Controls.Attributes.TagNames.Label, text) { }
        public HtmlLabel(params HtmlElement[] innerElements) : base(Controls.Attributes.TagNames.Label, innerElements) { }
    }
}
