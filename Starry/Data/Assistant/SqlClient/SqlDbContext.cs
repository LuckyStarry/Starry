using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;

namespace Starry.Data.Assistant.SqlClient
{
    public sealed class SqlDbContext : DbContext<SqlConnection, SqlCommand>
    {
        public SqlDbContext(string connectionString) : this(new SqlConnection(connectionString)) { }
        public SqlDbContext(SqlConnection connection)
            : base(connection)
        {
            this.DataContext = new DataContext(connection);
        }

        internal DataContext DataContext { private set; get; }

        public override IDbTable<TEntity> GetTable<TEntity>()
        {
            return new SqlDbTable<TEntity>(this);
        }
    }
}
