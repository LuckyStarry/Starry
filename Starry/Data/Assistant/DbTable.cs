using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Starry.Data.Assistant
{
    public abstract class DbTable<TEntity> : IDbTable<TEntity>
        where TEntity : class, new()
    {
        public DbTable(IDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public IDbContext DbContext { private set; get; }

        public virtual Type ElementType { get { return typeof(TEntity); } }

        public virtual Expression Expression { get { return Expression.Constant(this); } }

        public abstract IQueryProvider Provider { get; }

        public abstract IEnumerator<TEntity> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
