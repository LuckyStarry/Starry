using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Starry.Web.Controls
{
    public class HtmlControl : HtmlElement, Interface.IHtmlControl
    {
        public HtmlControl(string tag)
        {
            this.Tag = tag.Trim();
            this.Classes = new HtmlElementClassCollection();
            this.Attributes = new HtmlElementAttributeCollection();
            this.Styles = new HtmlElementStyleCollection();
        }

        public string Tag { private set; get; }

        public virtual string ID
        {
            set { this.Attr(Controls.Attributes.AttributeNames.ID, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.ID); }
        }

        public virtual string Name
        {
            set { this.Attr(Controls.Attributes.AttributeNames.Name, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.Name); }
        }

        public Interface.IHtmlElementClassCollection Classes { private set; get; }
        public Interface.IHtmlElementAttributeCollection Attributes { private set; get; }
        public Interface.IHtmlElementStyleCollection Styles { private set; get; }
    }
}
