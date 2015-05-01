using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTableCell : HtmlContainerControl
    {
        public HtmlTableCell() : this("td") { }
        public HtmlTableCell(HtmlElement innerControl) : this("td", innerControl) { }
        protected HtmlTableCell(string tag) : this(tag, string.Empty) { }
        protected HtmlTableCell(string tag, string innerHtml) : this(tag, string.IsNullOrEmpty(innerHtml) ? null : new HtmlElement(innerHtml)) { }
        protected HtmlTableCell(string tag, HtmlElement innerControl)
            : base(tag)
        {
            if (innerControl != null && innerControl != HtmlElement.Empty)
            {
                this.Children.Add(innerControl);
            }
        }
    }
}
