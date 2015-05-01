using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTH : HtmlTableCell
    {
        public HtmlTH() : base("th") { }
        public HtmlTH(string innerText) : base("th", innerText) { }
        public HtmlTH(HtmlElement innerControl) : base("th", innerControl) { }
    }
}
