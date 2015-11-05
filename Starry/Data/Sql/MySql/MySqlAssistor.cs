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

        public override DbCommandSource CreateDbCommandForGetPagedList<TEntity>(string selectText, int pageIndex, int pageSize, object conditions = null, object order = null)
        {
            var orderString = this.GetOrders(order);
            var dbConditions = this.GetDbConditions(conditions);
            var getList = dbConditions ?? new DbCommandSource();
            getList.CommandText = string.Format(@"
SELECT COUNT(1)
  FROM ({0}) AS __TCOUNT;
{0}", selectText);
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

        public override DbCommandSource CreateDbCommandForGetPagedList<TEntity>(int pageIndex, int pageSize, object conditions = null, object order = null)
        {
            var getList = this.CreateDbCommandForGetList<TEntity>(conditions);
            return this.CreateDbCommandForGetPagedList<TEntity>(getList.CommandText, pageIndex, pageSize, conditions, order);
        }

        public override DbCommandSource CreateDbCommandForAddAndGetEntity<TEntity>(TEntity entity)
        {
            var mapping = this.DbMappings.GetDbMapping(typeof(TEntity));
            var primaryKeys = mapping.Columns.Where(c => c.IsPrimaryKey).ToList();
            if (primaryKeys.Count != 1)
            {
                throw new SqlGenerateException(string.Format("The table must have only one primary key, but there are {0} primary key(s)", primaryKeys.Count));
            }
            var primaryKey = primaryKeys.First();
            var propertyType = primaryKey.PropertyInfo.PropertyType;
            if (propertyType != typeof(short)
                && propertyType != typeof(int)
                && propertyType != typeof(long))
            {
                throw new SqlGenerateException(string.Format("The type of primary key must is short int or long"));
            }
            var dbCommand = this.CreateDbCommandForAddEntity(entity);
            var dbGetCommand = this.CreateDbCommandForGetList<TEntity>(string.Format("{0} = LAST_INSERT_ID()", primaryKey.ColumnName));
            dbCommand.CommandText = string.Format("{0};{1}", dbCommand.CommandText, dbGetCommand.CommandText);
            return dbCommand;
        }
    }
}
