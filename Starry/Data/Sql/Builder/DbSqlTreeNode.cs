using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder
{
    public abstract class DbSqlTreeNode : IDbSqlTreeNode
    {
        public abstract DbCommandSource Generate();
    }
}
