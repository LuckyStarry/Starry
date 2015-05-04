using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Starry.Data.Assistant
{
    public abstract class DbContext<TDbConnection, TDbCommand> : IDbContext<TDbConnection, TDbCommand>
        where TDbConnection : IDbConnection
        where TDbCommand : IDbCommand
    {
        protected DbContext(TDbConnection dbConnection)
        {
            this.Connection = dbConnection;
        }

        public TDbConnection Connection { private set; get; }

        IDbConnection IDbContext.Connection { get { return this.Connection; } }

        public abstract IDbTable<TEntity> GetTable<TEntity>() where TEntity : new();

    }
}
