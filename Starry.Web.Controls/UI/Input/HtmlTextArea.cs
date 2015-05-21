using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlTextArea : HtmlControl, Attributes.IRows, Attributes.IReadOnly
    {
        public HtmlTextArea()
            : base("textarea")
        {
            this.ReadOnly = false;
        }

        public virtual int Rows
        {
            set { this.Attr(Controls.Attributes.AttributeNames.Rows, value); }
            get { return this.Attr(Controls.Attributes.AttributeNames.Rows).TryToInt32(); }
        }

        public virtual bool ReadOnly
        {
            set
            {
                if (value)
                {
                    this.Attr(Controls.Attributes.AttributeNames.ReadOnly, Controls.Attributes.AttributeNames.ReadOnly);
                }
                else
                {
                    this.RemoveAttr(Controls.Attributes.AttributeNames.ReadOnly);
                }
            }
            get { return !string.IsNullOrEmpty(this.Attr(Controls.Attributes.AttributeNames.ReadOnly).Trim()); }
        }
    }
}
