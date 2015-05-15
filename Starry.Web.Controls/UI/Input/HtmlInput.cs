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
            : base("input")
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Must set \"type\" for input", "type");
            }
            this.Type = type.ToLower();
        }

        public virtual string Type
        {
            set { this.Type(value); }
            get { return this.Type(); }
        }

        public virtual string Value
        {
            set { this.Value(value); }
            get { return this.Value(); }
        }
    }
}
