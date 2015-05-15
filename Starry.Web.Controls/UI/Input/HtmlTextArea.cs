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
            set { this.Rows(value); }
            get { return this.Rows(); }
        }

        public virtual bool ReadOnly
        {
            set { this.ReadOnly(value); }
            get { return this.ReadOnly(); }
        }
    }
}
