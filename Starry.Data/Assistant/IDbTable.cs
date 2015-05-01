using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Starry.Data.Assistant
{
    public interface IDbTable
    {
        IEnumerable<TEntity> GetList<TEntity>(string sqlCommandText);
    }

    public interface IDbTable<TEntity> : IDbTable
    {
        IEnumerable<T> GetList<T>(Expression<Func<TEntity, bool>> expression);
    }
}
