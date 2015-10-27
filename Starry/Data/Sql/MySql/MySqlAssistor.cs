using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.MySql
{
    public class MySqlAssistor : DbAssistor
    {
        public override string ParameterSymbol { get { return "?"; } }

        public override DbCommandSource CreateDbCommandForGetPagedList(string selectText, string order)
        {
            var sqlText = new StringBuilder();
            sqlText.AppendFormat(@"
SELECT COUNT(1)
  FROM ({0}) AS __TCOUNT;
{0}
", selectText);
            if (!string.IsNullOrWhiteSpace(order))
            {
                sqlText.AppendFormat(@"
 ORDER BY {0}
", order);
            }
            sqlText.AppendFormat(" LIMIT {0}{1},{0}{2}", this.ParameterSymbol, this.ParameterNameRecordFrom, this.ParameterNameRecordTo);
            return new DbCommandSource { CommandText = sqlText.ToString() };
        }

        public override DbCommandSource CreateDbCommandForAddEntityAndGetRecordID<TEntity>(TEntity entity)
        {
            var dbCommand = this.CreateDbCommandForAddEntity(entity);
            dbCommand.CommandText = string.Format("{0};SELECT LAST_INSERT_ID()", dbCommand.CommandText);
            return dbCommand;
        }
    }
}
