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

        public abstract IEnumerable<TEntity> GetList<TEntity>(string sqlCommandText) where TEntity : new();

        public int ExecuteNonQuery(string dbCommandText)
        {
            var dbCommand = this.CreateDbCommand(dbCommandText);
            return this.ExecuteNonQuery(dbCommand);
        }

        public virtual int ExecuteNonQuery(TDbCommand dbCommand)
        {
            return dbCommand.ExecuteNonQuery();
        }

        public object ExecuteScalar(string dbCommandText)
        {
            var dbCommand = this.CreateDbCommand(dbCommandText);
            return this.ExecuteScalar(dbCommand);
        }

        public virtual object ExecuteScalar(TDbCommand dbCommand)
        {
            return dbCommand.ExecuteScalar();
        }

        public IDbTable<TEntity> GetTable<TEntity>() where TEntity : new()
        {
            return new DbTable<TEntity>(this);
        }

        protected internal abstract TDbCommand CreateDbCommand(string sqlCommandText);
    }
}
