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
            set { this.ID(value); }
            get { return this.ID(); }
        }

        public virtual string Name
        {
            set { this.Name(value); }
            get { return this.Name(); }
        }

        public Interface.IHtmlElementClassCollection Classes { private set; get; }
        public Interface.IHtmlElementAttributeCollection Attributes { private set; get; }
        public Interface.IHtmlElementStyleCollection Styles { private set; get; }

        protected virtual void PreRender() { }

        public override string ToHtmlString()
        {
            this.PreRender();
            var html = new StringBuilder("<");
            html.Append(this.Tag);
            if (this.Classes != null && this.Classes.Count > 0)
            {
                html.Append(" ");
                html.Append(this.Classes.ToHtmlString());
            }
            if (this.Attributes != null && this.Attributes.Count > 0)
            {
                html.Append(" ");
                html.Append(this.Attributes.ToHtmlString());
            }
            if (this.Styles != null && this.Styles.Count > 0)
            {
                html.Append(" ");
                html.Append(this.Styles.ToHtmlString());
            }
            html.Append(" />");

            return html.ToString();
        }
    }
}
