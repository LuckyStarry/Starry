using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTableCell : HtmlContainerControl
    {
        public HtmlTableCell() : this(Controls.Attributes.TagNames.TD) { }
        public HtmlTableCell(HtmlElement innerControl) : this(Controls.Attributes.TagNames.TD, innerControl) { }
        protected HtmlTableCell(string tag) : this(tag, string.Empty) { }
        protected HtmlTableCell(string tag, string innerHtml) : this(tag, string.IsNullOrEmpty(innerHtml) ? null : new HtmlElement(innerHtml)) { }
        protected HtmlTableCell(string tag, HtmlElement innerControl)
            : base(tag)
        {
            if (!string.IsNullOrEmpty(innerControl.ToString()))
            {
                this.Children.Add(innerControl);
            }
        }
    }
}
