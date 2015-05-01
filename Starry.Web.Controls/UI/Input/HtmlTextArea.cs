using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTextArea : HtmlControl
    {
        public HtmlTextArea()
            : base("textarea")
        {
            this.ReadOnly = false;
        }

        public int Rows { set; get; }

        public bool ReadOnly { set; get; }

        protected override void PreRender()
        {
            if (this.Rows > 0)
            {
                this.Attr("rows", this.Rows);
            }
            if (this.ReadOnly)
            {
                this.Attr("readonly", "readonly");
            }
        }
    }
}
