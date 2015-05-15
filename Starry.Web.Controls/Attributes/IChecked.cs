using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Attributes
{
    public interface IChecked : Interface.IHtmlControl
    {
        bool Checked { set; get; }
    }
}
