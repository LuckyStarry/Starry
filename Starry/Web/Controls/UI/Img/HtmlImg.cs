using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlImg : HtmlControl, Attributes.ISrc
    {
        public HtmlImg() : base(Controls.Attributes.TagNames.Image) { }

        public string Src
        {
            set { this.Attr(Controls.Attributes.AttributeNames.Src, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.Src); }
        }
    }
}
