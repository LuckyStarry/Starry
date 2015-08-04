using Starry.Web.Controls.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlElementRender
    {
        string ToHtmlString(IHtmlElement htmlElement);
    }
}
