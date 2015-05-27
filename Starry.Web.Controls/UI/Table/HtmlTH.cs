using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTH : HtmlTableCell
    {
        public HtmlTH() : base(Controls.Attributes.TagNames.TH) { }
        public HtmlTH(string innerText) : base(Controls.Attributes.TagNames.TH, innerText) { }
        public HtmlTH(HtmlElement innerControl) : base(Controls.Attributes.TagNames.TH, innerControl) { }
    }
}
