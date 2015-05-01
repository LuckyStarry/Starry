using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Starry.Data.Assistant.SqlClient
{
    public class SqlDbContext : DbContext<System.Data.SqlClient.SqlConnection, System.Data.SqlClient.SqlCommand>
    {
        public SqlDbContext(string connectionString) : this(new System.Data.SqlClient.SqlConnection(connectionString)) { }
        public SqlDbContext(System.Data.SqlClient.SqlConnection connection) : base(connection) { }

        protected internal override System.Data.SqlClient.SqlCommand CreateDbCommand(string sqlCommandText)
        {
            return new System.Data.SqlClient.SqlCommand(sqlCommandText);
        }

        public override IEnumerable<TEntity> GetList<TEntity>(string sqlCommandText)
        {
            throw new NotImplementedException();
        }
    }
}
