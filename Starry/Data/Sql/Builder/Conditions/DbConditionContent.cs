using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Conditions
{
    public sealed class DbConditionContent : DbCondition, IDbConditionContent
    {
        public DbConditionContent()
        {
            this.Parameters = new Dictionary<string, object>();
        }

        public string ConditionString { set; get; }
        public IDictionary<string, object> Parameters { private set; get; }

        public override DbCommandSource Generate()
        {
            var dbCommandSource = new DbCommandSource { CommandText = this.ConditionString };
            foreach (var param in this.Parameters)
            {
                dbCommandSource.Parameters.Add(param.Key, param.Value);
            }
            return dbCommandSource;
        }
    }
}
