using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTableRow : HtmlContainerControl
    {
        public HtmlTableRow() : this(Controls.Attributes.TagNames.TR) { }
        protected HtmlTableRow(string tag) : base(tag) { }
    }
}
