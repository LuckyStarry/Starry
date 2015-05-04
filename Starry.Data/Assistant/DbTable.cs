using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Starry.Data.Assistant
{
    public abstract class DbTable<TEntity> : IDbTable<TEntity>
        where TEntity : new()
    {
        public DbTable(IDbContext dbContext)
        {
            this.elementType = typeof(TEntity);
            this.DbContext = dbContext;
        }

        public IDbContext DbContext { private set; get; }

        public virtual Type ElementType { get { return this.elementType; } }
        private Type elementType;

        public abstract Expression Expression { get; }

        public abstract IQueryProvider Provider { get; }

        public abstract IEnumerator<TEntity> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
