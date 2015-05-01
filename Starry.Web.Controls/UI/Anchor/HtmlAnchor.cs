using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlAnchor : HtmlContainerControl, Attributes.IHRef
    {
        public HtmlAnchor() : base("a") { }

        public string HRef { set; get; }

        protected override void PreRender()
        {
            if (!string.IsNullOrEmpty(this.HRef))
            {
                this.Attr("href", this.HRef);
            }
        }
    }
}
