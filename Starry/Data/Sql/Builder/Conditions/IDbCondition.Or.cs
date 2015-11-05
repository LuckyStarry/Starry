using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder
{
    public static partial class IDbConditionEx
    {
        public static Conditions.IDbCondition Or(this Conditions.IDbCondition left, Conditions.IDbCondition right)
        {
            return new Conditions.DbConditionOperationOr(left, right);
        }
    }
}
