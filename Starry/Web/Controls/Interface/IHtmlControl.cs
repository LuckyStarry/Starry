using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlControl : IHtmlElement
    {
        string ID { set; get; }
        string Name { set; get; }
        IHtmlElementClassCollection Classes { get; }
        IHtmlElementAttributeCollection Attributes { get; }
        IHtmlElementStyleCollection Styles { get; }
    }
}
