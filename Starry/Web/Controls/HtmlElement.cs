using System;
using System.Collections.Generic;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlElement : Interface.IHtmlElement
    {
        public Interface.IHtmlElement Parent { set; get; }
    }
}
