using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTableRow : HtmlContainerControl
    {
        public HtmlTableRow() : this("tr") { }
        protected HtmlTableRow(string tag) : base(tag) { }
    }
}
