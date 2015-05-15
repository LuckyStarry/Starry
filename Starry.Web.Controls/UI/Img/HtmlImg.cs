using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlImg : HtmlControl, Attributes.ISrc
    {
        public HtmlImg() : base("img") { }

        public string Src
        {
            set { this.Src(value); }
            get { return this.Src(); }
        }
    }
}
