using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Attributes
{
    public interface IText : Interface.IHtmlControl
    {
        string Text { set; get; }
    }
}
