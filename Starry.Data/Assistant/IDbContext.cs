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
        IDbTable<TEntity> GetTable<TEntity>();
        IEnumerable<TEntity> GetList<TEntity>(string sqlCommandText);
        int ExecuteNonQuery(string sqlCommandText);
        object ExecuteScalar(string sqlCommandText);
    }

    public interface IDbContext<TDbConnection> : IDbContext
        where TDbConnection : IDbConnection
    {
        new TDbConnection Connection { get; }
    }
}
