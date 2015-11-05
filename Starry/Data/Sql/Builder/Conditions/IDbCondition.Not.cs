using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder
{
    public static partial class IDbConditionEx
    {
        public static Conditions.IDbCondition Not(this Conditions.IDbCondition right)
        {
            return new Conditions.DbConditionOperationNot(right);
        }
    }
}
