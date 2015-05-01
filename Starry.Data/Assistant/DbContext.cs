using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Starry.Data.Assistant
{
    public abstract class DbContext<TDbConnection> : IDbContext<TDbConnection>
        where TDbConnection : IDbConnection
    {
        protected DbContext(TDbConnection dbConnection)
        {
            this.Connection = dbConnection;
        }

        public TDbConnection Connection { private set; get; }

        IDbConnection IDbContext.Connection { get { return this.Connection; } }

        public abstract IEnumerable<TEntity> GetList<TEntity>(string sqlCommandText);

        public abstract int ExecuteNonQuery(string sqlCommandText);

        public abstract object ExecuteScalar(string sqlCommandText);

        public IDbTable<TEntity> GetTable<TEntity>()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class DbContext<TDbConnection, TDbCommand> : DbContext<TDbConnection>
        where TDbConnection : IDbConnection
        where TDbCommand : IDbCommand
    {
        protected DbContext(TDbConnection dbConnection) : base(dbConnection) { }

        public override int ExecuteNonQuery(string sqlCommandText)
        {
            var dbCommand = this.CreateDbCommand(sqlCommandText);
            if (dbCommand.Connection == null)
            {
                dbCommand.Connection = this.Connection;
            }
            if (dbCommand.Connection.State != ConnectionState.Open)
            {
                dbCommand.Connection.Open();
            }
            return dbCommand.ExecuteNonQuery();
        }

        public override object ExecuteScalar(string sqlCommandText)
        {
            var dbCommand = this.CreateDbCommand(sqlCommandText);
            if (dbCommand.Connection == null)
            {
                dbCommand.Connection = this.Connection;
            }
            if (dbCommand.Connection.State != ConnectionState.Open)
            {
                dbCommand.Connection.Open();
            }
            return dbCommand.ExecuteScalar();
        }

        protected internal abstract TDbCommand CreateDbCommand(string sqlCommandText);
    }
}
