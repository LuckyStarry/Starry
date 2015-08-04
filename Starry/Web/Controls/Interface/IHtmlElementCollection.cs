using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlElementCollection : IEnumerable<IHtmlElement>
    {
        void Add(IHtmlElement element);
        void Clear();
        int Count { get; }
        bool ReadOnly { get; }
        bool Remove(IHtmlElement element);
    }
}
