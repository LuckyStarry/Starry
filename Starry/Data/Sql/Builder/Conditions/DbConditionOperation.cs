using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Conditions
{
    public abstract class DbConditionOperation : DbCondition, IDbConditionOperation
    {
        public abstract string OperationText { get; }
    }
}
