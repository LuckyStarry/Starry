using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlForm : HtmlContainerControl, Attributes.IMethod, Attributes.IAction
    {
        public HtmlForm() : base("form") { }

        public string Method { set; get; }
        public string Action { set; get; }

        protected override void PreRender()
        {
            if (!string.IsNullOrEmpty(this.Method))
            {
                this.Attr("method", this.Method);
            }
            if (!string.IsNullOrEmpty(this.Action))
            {
                this.Attr("action", this.Action);
            }
        }
    }
}
