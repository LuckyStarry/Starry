using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public sealed class DbCommandSource
    {
        public DbCommandSource()
        {
            this.Parameters = new Dictionary<string, object>();
        }

        public string CommandText { set; get; }
        public IDictionary<string, object> Parameters { private set; get; }
    }
}
