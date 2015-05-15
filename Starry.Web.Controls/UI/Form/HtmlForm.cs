using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlForm : HtmlContainerControl, Attributes.IMethod, Attributes.IAction
    {
        public HtmlForm() : base("form") { }

        public string Method
        {
            set { this.Method(value); }
            get { return this.Method(); }
        }

        public virtual string Action
        {
            set { this.Action(value); }
            get { return this.Action(); }
        }
    }
}
