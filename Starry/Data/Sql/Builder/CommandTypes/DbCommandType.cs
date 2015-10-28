using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.CommandTypes
{
    public abstract class DbCommandType : DbSqlTreeNode, IDbCommandType
    {
        public abstract string CommandTypeText { get; }
    }
}
