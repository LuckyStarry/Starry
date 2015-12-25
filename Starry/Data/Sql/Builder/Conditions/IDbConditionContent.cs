using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Conditions
{
    public interface IDbConditionContent : IDbCondition
    {
        string ConditionString { get; }
    }
}
