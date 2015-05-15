using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlButton : HtmlContainerControl, Attributes.IType
    {
        public HtmlButton() : this("button") { }

        protected HtmlButton(string type)
            : base("button")
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Must set \"type\" for button", "type");
            }
            this.Type = type.ToLower();
        }

        public virtual string Type
        {
            set { this.Type(value); }
            get { return this.Type(); }
        }
    }
}
