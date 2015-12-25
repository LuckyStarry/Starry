using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class SqlGenerateException : Exception
    {
        public SqlGenerateException() : base() { }
        public SqlGenerateException(string message) : base(message) { }
    }
}
