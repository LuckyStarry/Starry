using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlElementAttribute
    {
        string Name { set; get; }
        string Value { set; get; }
    }
}
