using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlAnchor : HtmlContainerControl, Attributes.IHRef
    {
        public HtmlAnchor() : base("a") { }

        public virtual string HRef
        {
            set { this.HRef(value); }
            get { return this.HRef(); }
        }
    }
}
