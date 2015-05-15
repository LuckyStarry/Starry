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
            set { this.Checked(value); }
            get { return this.Checked(); }
        }
    }
}
