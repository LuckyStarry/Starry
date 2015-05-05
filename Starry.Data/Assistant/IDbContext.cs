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
        IDbTable<TEntity> GetTable<TEntity>() where TEntity : class, new();
    }

    public interface IDbContext<TDbConnection, TDbCommand> : IDbContext
        where TDbConnection : IDbConnection
        where TDbCommand : IDbCommand
    {
        new TDbConnection Connection { get; }
    }
}
