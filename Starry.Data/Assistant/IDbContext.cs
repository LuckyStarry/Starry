using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Starry.Data.Assistant
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
        IDbTable<TEntity> GetTable<TEntity>() where TEntity : new();
        IEnumerable<TEntity> GetList<TEntity>(string sqlCommandText) where TEntity : new();
        int ExecuteNonQuery(string dbCommandText);
        object ExecuteScalar(string dbCommandText);
    }

    public interface IDbContext<TDbConnection, TDbCommand> : IDbContext
        where TDbConnection : IDbConnection
        where TDbCommand : IDbCommand
    {
        new TDbConnection Connection { get; }
        int ExecuteNonQuery(TDbCommand dbCommand);
        object ExecuteScalar(TDbCommand dbCommand);
    }
}
