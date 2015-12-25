using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public sealed class HtmlString : HtmlElement, ICloneable<HtmlString>
    {
        public HtmlString() : this(string.Empty) { }
        public HtmlString(string text)
        {
            this.Text = text;
        }

        public string Text { set; get; }

        public HtmlString Clone()
        {
            return new HtmlString(this.Text);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
