using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Conditions
{
    public abstract class DbConditionOperationBinary : DbConditionOperation
    {
        protected DbConditionOperationBinary(IDbCondition left, IDbCondition right)
        {
            this.Left = left;
            this.Right = right;
        }

        public IDbCondition Left { private set; get; }
        public IDbCondition Right { private set; get; }

        public override DbCommandSource Generate()
        {
            var dbCommandSource = new DbCommandSource();
            var left = this.Left.Generate();
            var right = this.Right.Generate();
            dbCommandSource.CommandText = string.Format("({0}) {2} ({1})", left.CommandText, right.CommandText, this.OperationText);
            foreach (var param in left.Parameters)
            {
                dbCommandSource.Parameters.Add(param.Key, param.Value);
            }
            foreach (var param in right.Parameters)
            {
                dbCommandSource.Parameters.Add(param.Key, param.Value);
            }
            return dbCommandSource;
        }
    }
}
