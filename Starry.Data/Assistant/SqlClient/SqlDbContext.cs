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

        public override IDbTable<TEntity> GetTable<TEntity>()
        {
            return new SqlDbTable<TEntity>(this);
        }
    }
}
