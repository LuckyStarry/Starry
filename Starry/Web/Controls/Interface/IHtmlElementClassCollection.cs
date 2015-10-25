using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls.Interface
{
    public interface IHtmlElementClassCollection : IEnumerable<string>
    {
        int Count { get; }
        void Clear();
        void Add(string @class);
        bool Remove(string @class);
    }
}
