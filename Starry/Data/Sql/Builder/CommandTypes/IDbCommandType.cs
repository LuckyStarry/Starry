using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.CommandTypes
{
    public interface IDbCommandType : IDbSqlTreeNode
    {
        string CommandTypeText { get; }
    }
}
