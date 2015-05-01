using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Starry.Data.Assistant.SqlClient
{
    public class SqlDbContext : DbContext<SqlConnection, SqlCommand>
    {
        public SqlDbContext(string connectionString) : this(new SqlConnection(connectionString)) { }
        public SqlDbContext(SqlConnection connection) : base(connection) { }

        protected internal override SqlCommand CreateDbCommand(string sqlCommandText)
        {
            var sqlCmd = new SqlCommand(sqlCommandText);
            sqlCmd.Connection = this.Connection;
            if (sqlCmd.Connection.State != ConnectionState.Open)
            {
                sqlCmd.Connection.Open();
            }
            return sqlCmd;
        }

        public override IEnumerable<TEntity> GetList<TEntity>(string sqlCommandText)
        {
            var sa = new SqlDataAdapter(this.CreateDbCommand(sqlCommandText));
            var ds = new DataSet();
            sa.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].ToList<TEntity>();
            }
            return null;
        }
    }
}
