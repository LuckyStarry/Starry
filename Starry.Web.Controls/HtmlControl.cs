using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Starry.Web.Controls
{
    public class HtmlControl : HtmlDocument
    {
        public HtmlControl(string tag)
        {
            this.Tag = tag.Trim();
            this.Classes = new HtmlDocumentClassCollection();
            this.Attributes = new HtmlDocumentAttributeCollection();
            this.Styles = new HtmlDocumentStyleCollection();
        }

        public string Tag { private set; get; }

        public HtmlDocumentClassCollection Classes { private set; get; }
        public HtmlDocumentAttributeCollection Attributes { private set; get; }
        public HtmlDocumentStyleCollection Styles { private set; get; }

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
