using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Conditions
{
    public sealed class DbConditionOperationNot : DbConditionOperationMonadic
    {
        public DbConditionOperationNot(IDbCondition right) : base(right) { }

        public override string OperationText { get { return "NOT"; } }
    }
}
