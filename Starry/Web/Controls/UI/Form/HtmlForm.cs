using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlForm : HtmlContainerControl, Attributes.IMethod, Attributes.IAction
    {
        public HtmlForm() : base(Controls.Attributes.TagNames.Form) { }

        public string Method
        {
            set { this.Attr(Controls.Attributes.AttributeNames.Method, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.Method); }
        }

        public virtual string Action
        {
            set { this.Attr(Controls.Attributes.AttributeNames.Action, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.Action); }
        }
    }
}
