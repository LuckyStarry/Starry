using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlElementStyleCollection : IEnumerable<Interface.IHtmlElementStyle>
    {
        int Count { get; }
        void Clear();
        string this[string name] { set; get; }
        bool Remove(string name);
        string ToHtmlString();
    }
}
