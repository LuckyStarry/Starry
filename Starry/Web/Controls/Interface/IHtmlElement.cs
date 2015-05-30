using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlElement
    {
        IHtmlElement Parent { set; get; }
        string ToHtmlString();
    }
}
