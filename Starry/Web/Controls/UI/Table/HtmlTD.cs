using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTD : HtmlTableCell
    {
        public HtmlTD() : base(Controls.Attributes.TagNames.TD) { }
        public HtmlTD(string innerText) : base(Controls.Attributes.TagNames.TD, innerText) { }
        public HtmlTD(HtmlElement innerControl) : base(Controls.Attributes.TagNames.TD, innerControl) { }
    }
}
