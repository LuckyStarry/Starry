using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Log
{
    public interface ILogger
    {
        void Record(ILogDetail logDetail);
    }

    public interface ILogger<TLogDetail> : ILogger
        where TLogDetail : ILogDetail
    {
        void Record(TLogDetail logDetail);
    }
}
