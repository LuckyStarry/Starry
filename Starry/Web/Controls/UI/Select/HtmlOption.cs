using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlOption : HtmlContainerControl, Attributes.IValue, Attributes.ISelected
    {
        public HtmlOption() : base(Controls.Attributes.TagNames.Option) { }

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
            set { this.Attr(Controls.Attributes.AttributeNames.Value, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.Value); }
        }

        public virtual bool Selected
        {
            set
            {
                if (value)
                {
                    this.Attr(Controls.Attributes.AttributeNames.Selected, Controls.Attributes.AttributeNames.Selected);
                }
                else
                {
                    this.RemoveAttr(Controls.Attributes.AttributeNames.Selected);
                }
            }
            get { return !string.IsNullOrEmpty(this.Attr(Controls.Attributes.AttributeNames.Selected).Trim()); }
        }
    }
}
