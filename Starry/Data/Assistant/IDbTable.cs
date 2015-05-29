using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Starry.Data.Assistant
{
    public interface IDbTable<TEntity> : IQueryable<TEntity>
        where TEntity : class, new()
    {
    }
}
