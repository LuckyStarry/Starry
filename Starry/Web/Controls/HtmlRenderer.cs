﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlRenderer
    {
        public virtual string Render(Interface.IHtmlElement htmlElement)
        {
            var html = new StringBuilder();
            if (htmlElement is Interface.IHtmlControl)
            {
                var htmlControl = htmlElement as Interface.IHtmlControl;

                html.Append("<");
                html.Append(htmlControl.Tag);
                if (htmlControl.Classes != null && htmlControl.Classes.Count > 0)
                {
                    html.AppendFormat(" class=\"{0}\"", string.Join(" ", htmlControl.Classes.ToArray()));
                }
                if (htmlControl.Attributes != null && htmlControl.Attributes.Count > 0)
                {
                    html.Append(" ");
                    html.Append(string.Join(" ", htmlControl.Attributes.Select(
                        attr => string.Format("{0}=\"{1}\"", attr.Name, attr.Value)).ToArray()));
                }
                if (htmlControl.Styles != null && htmlControl.Styles.Count > 0)
                {
                    html.AppendFormat(" style=\"{0}\"", string.Join(" ", htmlControl.Styles.Select(
                        css => string.Format("{0}=\"{1}\";", css.Name, css.Value)).ToArray()));
                }
                if (htmlControl is Interface.IHtmlContainerControl)
                {
                    var children = (htmlControl as Interface.IHtmlContainerControl).Children;
                    if (children != null && children.Count > 0)
                    {
                        html.AppendLine(">");
                        html.Append(string.Join(System.Environment.NewLine, children.Select(ele => this.Render(ele)).ToArray()));
                        html.AppendLine();
                    }
                    else
                    {
                        html.Append(">");
                    }
                    html.AppendFormat("</{0}>", htmlControl.Tag);
                }
                else
                {
                    html.Append(" />");
                }
            }
            else
            {
                html.Append(htmlElement.ToString());
            }

            return html.ToString();
        }
    }
}
