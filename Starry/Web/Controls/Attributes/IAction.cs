using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Attributes
{
    public interface IAction : Interface.IHtmlControl
    {
        string Action { set; get; }
    }
}
