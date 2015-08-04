using System;
using System.Collections.Generic;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlElement : Interface.IHtmlElement
    {
        public HtmlElement() : this(string.Empty) { }

        public HtmlElement(string html)
        {
            this.html = html;
        }

        private string html;

        public Interface.IHtmlElement Parent { set; get; }

        public override sealed string ToString()
        {
            return this.html;
        }
    }
}
