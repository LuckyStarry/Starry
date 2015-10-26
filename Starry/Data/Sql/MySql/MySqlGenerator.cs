﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.MySql
{
    public class MySqlGenerator : DbCommandGenerator
    {
        public override string ParameterNamePrefix { get { return "?"; } }

        public override DbCommand CreateDbCommandForGetPagedList(string selectText, string order)
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
            sqlText.AppendFormat(" LIMIT {0}{1},{0}{2}", this.ParameterNamePrefix, this.ParameterNameNumFrom, this.ParameterNameNumTo);
            return this.DbEntity.CreateDbCommand(sqlText.ToString());
        }
    }
}
