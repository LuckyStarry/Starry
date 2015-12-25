using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.CommandTypes
{
    public sealed class DbCommandTypeSelect : DbCommandType
    {
        public override string CommandTypeText { get { return "SELECT"; } }

        public override DbCommandSource Generate()
        {
            throw new NotImplementedException();
        }
    }
}
