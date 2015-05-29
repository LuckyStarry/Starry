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

        public virtual string ToHtmlString()
        {
            return this.html;
        }

        public override sealed string ToString()
        {
            return this.ToHtmlString();
        }

        private static readonly HtmlElement empty = new HtmlElement() { };
        public static HtmlElement Empty { get { return empty; } }

        public override bool Equals(object o)
        {
            if (o is HtmlElement)
            {
                return this.Equals(o as HtmlElement);
            }
            return object.ReferenceEquals(this, o);
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(this.html))
            {
                return 0;
            }
            return base.GetHashCode();
        }

        public bool Equals(HtmlElement htmlDocument)
        {
            if (object.ReferenceEquals(htmlDocument, null))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.html))
            {
                return string.IsNullOrEmpty(htmlDocument.ToHtmlString());
            }
            return object.ReferenceEquals(this, htmlDocument);
        }

        public static bool operator ==(HtmlElement left, HtmlElement right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HtmlElement left, HtmlElement right)
        {
            return !(left == right);
        }
    }
}
