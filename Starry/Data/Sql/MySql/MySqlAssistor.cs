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

        protected internal override DbCommandSource CreateDbCommandForGetPagedList<TEntity>(int pageIndex, int pageSize, object conditions = null, object order = null)
        {
            var orderString = this.GetOrders(order);
            var getList = this.CreateDbCommandForGetList<TEntity>(conditions);
            getList.CommandText = string.Format(@"
SELECT COUNT(1)
  FROM ({0}) AS __TCOUNT;
{0}
", getList.CommandText);
            if (!string.IsNullOrWhiteSpace(orderString))
            {
                getList.CommandText += string.Format(@" ORDER BY {0}
", orderString);
            }
            var pRowStart = "__rowStart";
            var pPageSize = "__pageSize";
            getList.CommandText += string.Format(" LIMIT {0}{1},{0}{2}", this.ParameterSymbol, pRowStart, pPageSize);
            getList.Parameters.Add(pRowStart, (pageIndex - 1) * pageSize);
            getList.Parameters.Add(pPageSize, pageSize);
            return getList;
        }

        public override DbCommandSource CreateDbCommandForAddEntityAndGetRecordID<TEntity>(TEntity entity)
        {
            var dbCommand = this.CreateDbCommandForAddEntity(entity);
            dbCommand.CommandText = string.Format("{0};SELECT LAST_INSERT_ID()", dbCommand.CommandText);
            return dbCommand;
        }
    }
}
