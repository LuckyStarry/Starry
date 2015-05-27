using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlInput : HtmlControl, Attributes.IType, Attributes.IValue
    {
        public HtmlInput() : this("text") { }

        protected HtmlInput(string type)
            : base(Controls.Attributes.TagNames.Input)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Must set \"type\" for input", Controls.Attributes.AttributeNames.Type);
            }
            this.Type = type.ToLower();
        }

        public virtual string Type
        {
            set { this.Attr(Controls.Attributes.AttributeNames.Type, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.Type); }
        }

        public virtual string Value
        {
            set { this.Attr(Controls.Attributes.AttributeNames.Value, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.Value); }
        }
    }
}
