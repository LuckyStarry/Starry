using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlControl
    {
        IHtmlElementClassCollection Classes { get; }
        IHtmlElementAttributeCollection Attributes { get; }
        IHtmlElementStyleCollection Styles { get; }
    }
}
