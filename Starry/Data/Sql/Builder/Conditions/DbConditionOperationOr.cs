using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Conditions
{
    public sealed class DbConditionOperationOr : DbConditionOperationBinary
    {
        public DbConditionOperationOr(IDbCondition left, IDbCondition right) : base(left, right) { }

        public override string OperationText { get { return "OR"; } }
    }
}
