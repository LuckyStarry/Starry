using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}
