using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Starry.Data.Assistant
{
    public static partial class IDbTableExtend
    {
        public static IEnumerable<TEntity> GetList<TEntity>(this IDbTable<TEntity> dbTable, Expression<Func<TEntity, bool>> expression)
        {
            return dbTable.GetList<TEntity>(expression);
        }
    }
}
