using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlOption : HtmlContainerControl, Attributes.IValue, Attributes.ISelected
    {
        public HtmlOption() : base("option") { }

        public virtual string Text
        {
            set
            {
                this.Children.Clear();
                this.Append(new HtmlElement(value));
            }
            get { return this.Children.ToHtmlString(); }
        }

        public virtual string Value
        {
            set { this.Value(value); }
            get { return this.Value(); }
        }

        public virtual bool Selected
        {
            set { this.Selected(value); }
            get { return this.Selected(); }
        }
    }
}
