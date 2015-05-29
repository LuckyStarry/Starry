using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Starry.Web.Controls
{
    public class HtmlContainerControl : HtmlControl, Interface.IHtmlContainerControl
    {
        public HtmlContainerControl(string tag)
            : base(tag)
        {
            this.Children = new HtmlElementCollection();
        }

        public HtmlContainerControl(string tag, string innerHTML)
            : this(tag)
        {
            if (!string.IsNullOrEmpty(innerHTML))
            {
                this.Children.Add(new HtmlElement(innerHTML));
            }
        }

        public HtmlContainerControl(string tag, params Interface.IHtmlElement[] innerElements)
            : this(tag)
        {
            if (innerElements != null && innerElements.Any())
            {
                foreach (var innerDocument in innerElements)
                {
                    this.Children.Add(innerDocument);
                }
            }
        }

        public Interface.IHtmlElementCollection Children { private set; get; }

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
            if (this.Children != null && this.Children.Count > 0)
            {
                html.AppendLine(">");
                html.Append(this.Children.ToHtmlString());
                html.AppendLine();
            }
            html.Append(">");
            html.AppendFormat("</{0}>", this.Tag);

            return html.ToString();
        }
    }
}
