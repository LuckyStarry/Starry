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

        protected internal override DbCommandSource CreateDbCommandForAddAndGetEntity<TEntity>(TEntity entity)
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
