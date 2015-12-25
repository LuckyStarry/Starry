using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Conditions
{
    public interface IDbConditionOperation : IDbCondition
    {
        string OperationText { get; }
    }
}
