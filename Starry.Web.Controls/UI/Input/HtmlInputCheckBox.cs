using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlInputCheckBox : HtmlInput, Attributes.IChecked
    {
        public HtmlInputCheckBox() : base("checkbox") { }

        public bool Checked
        {
            set
            {
                if (value)
                {
                    this.Attr(Controls.Attributes.AttributeNames.Checked, Controls.Attributes.AttributeNames.Checked);
                }
                else
                {
                    this.RemoveAttr(Controls.Attributes.AttributeNames.Checked);
                }
            }
            get { return !string.IsNullOrEmpty(this.Attr(Controls.Attributes.AttributeNames.Checked).Trim()); }
        }
    }
}
