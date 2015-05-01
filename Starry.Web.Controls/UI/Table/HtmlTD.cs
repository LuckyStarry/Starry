using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTD : HtmlTableCell
    {
        public HtmlTD() : base("td") { }
        public HtmlTD(string innerText) : base("td", innerText) { }
        public HtmlTD(HtmlElement innerControl) : base("td", innerControl) { }
    }
}
