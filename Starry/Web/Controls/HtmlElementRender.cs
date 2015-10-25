using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlElementRender : Interface.IHtmlElementRender
    {
        public string ToHtmlString(Interface.IHtmlElement htmlElement)
        {
            if (htmlElement is Interface.IHtmlControl)
            {
                var htmlControl = htmlElement as Interface.IHtmlControl;
                var html = new StringBuilder("<");
                html.Append(htmlControl.Tag);
                if (htmlControl.Classes != null && htmlControl.Classes.Count > 0)
                {
                    html.Append(" class=\"");
                    html.Append(string.Join(" ", htmlControl.Classes.ToArray()));
                    html.Append("\"");
                }
                if (htmlControl.Attributes != null && htmlControl.Attributes.Count > 0)
                {
                    html.Append(" ");
                    var attrHtml = new List<string>();
                    foreach (var attr in htmlControl.Attributes)
                    {
                        attrHtml.Add(string.Format("{0}=\"{1}\"", attr.Name, attr.Value));
                    }
                    html.Append(string.Join(" ", attrHtml.ToArray()));
                }
                if (htmlControl.Styles != null && htmlControl.Styles.Count > 0)
                {
                    html.Append(" style=\"");
                    var stylesString = new List<string>();
                    foreach (var style in htmlControl.Styles)
                    {
                        stylesString.Add(string.Format("{0}: {1}", style.Name, style.Value));
                    }
                    html.Append(string.Join("; ", stylesString.ToArray()));
                    html.Append("\"");
                }
                if (htmlControl is Interface.IHtmlContainerControl)
                {
                    var htmlContainerControl = htmlControl as Interface.IHtmlContainerControl;
                    if (htmlContainerControl.Children != null && htmlContainerControl.Children.Count > 0)
                    {
                        html.AppendLine(">");
                        html.Append(string.Join(System.Environment.NewLine, htmlContainerControl.Children.Select(ele => this.ToHtmlString(ele)).ToArray()));
                        html.AppendLine();
                    }
                    else
                    {
                        html.Append(">");
                    }
                    html.AppendFormat("</{0}>", htmlContainerControl.Tag);
                }
                else
                {
                    html.Append(" />");
                }
                return html.ToString();
            }
            else
            {
                return htmlElement.ToString();
            }
        }
    }
}
