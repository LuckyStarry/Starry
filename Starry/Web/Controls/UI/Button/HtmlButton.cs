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
            : base(Controls.Attributes.TagNames.Button)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Must set \"type\" for button", Controls.Attributes.AttributeNames.Type);
            }
            this.Type = type.ToLower();
        }

        public virtual string Type
        {
            set { this.Attr(Controls.Attributes.AttributeNames.Type, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.Type); }
        }
    }
}
