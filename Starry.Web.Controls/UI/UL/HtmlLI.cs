using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlLI : HtmlContainerControl
    {
        public HtmlLI() : base(Controls.Attributes.TagNames.LI) { }
        public HtmlLI(string text) : base(Controls.Attributes.TagNames.LI, text) { }
        public HtmlLI(params HtmlElement[] innerElements) : base(Controls.Attributes.TagNames.LI, innerElements) { }
    }
}
