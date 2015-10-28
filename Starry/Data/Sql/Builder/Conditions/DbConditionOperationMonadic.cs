using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Conditions
{
    public abstract class DbConditionOperationMonadic : DbConditionOperation
    {
        protected DbConditionOperationMonadic(IDbCondition right)
        {
            this.Right = right;
        }

        public IDbCondition Right { private set; get; }

        public override DbCommandSource Generate()
        {
            var dbCommandSource = new DbCommandSource();
            var right = this.Right.Generate();
            dbCommandSource.CommandText = string.Format("{0} ({1})", this.OperationText, right.CommandText);
            foreach (var param in right.Parameters)
            {
                dbCommandSource.Parameters.Add(param.Key, param.Value);
            }
            return dbCommandSource;
        }
    }
}
