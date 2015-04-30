using System;
using System.Collections.Generic;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlDocument
    {
        public HtmlDocument() : this(string.Empty) { }

        public HtmlDocument(string html)
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

        private static readonly HtmlDocument empty = new HtmlDocument() { };
        public static HtmlDocument Empty { get { return empty; } }

        public override bool Equals(object o)
        {
            if (o is HtmlDocument)
            {
                return this.Equals(o as HtmlDocument);
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

        public bool Equals(HtmlDocument htmlDocument)
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

        public static bool operator ==(HtmlDocument left, HtmlDocument right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HtmlDocument left, HtmlDocument right)
        {
            return !(left == right);
        }
    }
}
