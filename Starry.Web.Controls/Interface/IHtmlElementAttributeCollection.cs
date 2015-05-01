using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlElementAttributeCollection : IEnumerable<Interface.IHtmlElementAttribute>
    {
        int Count { get; }
        void Clear();
        string this[string name] { set; get; }
        bool Remove(string name);
        string ToHtmlString();
    }
}
