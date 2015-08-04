﻿using System;
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
            this.Children = new HtmlElementCollection(this);
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
    }
}
