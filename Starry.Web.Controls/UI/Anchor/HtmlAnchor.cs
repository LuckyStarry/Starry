using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlAnchor : HtmlContainerControl, Attributes.IHRef
    {
        public HtmlAnchor() : base(Controls.Attributes.TagNames.Anchor) { }

        public virtual string HRef
        {
            set { this.Attr(Controls.Attributes.AttributeNames.HRef, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.HRef); }
        }
    }
}
