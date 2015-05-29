using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlContainerControl : IHtmlControl
    {
        Interface.IHtmlElementCollection Children { get; }
    }
}
