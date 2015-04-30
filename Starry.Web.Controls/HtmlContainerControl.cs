using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Starry.Web.Controls
{
    public class HtmlContainerControl : HtmlControl
    {
        public HtmlContainerControl(string tag)
            : base(tag)
        {
            this.Children = new HtmlDocumentCollection();
        }

        public HtmlDocumentCollection Children { private set; get; }

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
            html.AppendLine(">");

            if (this.Children != null && this.Children.Count > 0)
            {
                html.Append(this.Children.ToHtmlString());
                html.AppendLine();
            }
            html.AppendFormat("</{0}>", this.Tag);

            return html.ToString();
        }
    }
}
