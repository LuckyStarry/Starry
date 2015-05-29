using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlSpan : HtmlContainerControl
    {
        public HtmlSpan() : base(Controls.Attributes.TagNames.Span) { }
        public HtmlSpan(string text) : base(Controls.Attributes.TagNames.Span, text) { }
        public HtmlSpan(params HtmlElement[] innerElements) : base(Controls.Attributes.TagNames.Span, innerElements) { }
    }
}
