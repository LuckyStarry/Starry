using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlInput : HtmlControl
    {
        public HtmlInput() : this("text") { }

        protected HtmlInput(string type)
            : base("input")
        {
            this.Type = type;
        }

        public string Type { private set; get; }

        protected override void PreRender()
        {
            this.Attr("type", this.Type.ToLower());
        }
    }
}
