using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Starry.Data.Assistant.SqlClient
{
    public sealed class SqlDbTable<TEntity> : DbTable<TEntity>, IQueryProvider
        where TEntity : class, new()
    {
        private Table<TEntity> table;

        internal SqlDbTable(SqlDbContext dbContext)
            : base(dbContext)
        {
            this.table = dbContext.DataContext.GetTable<TEntity>();
        }

        public override IQueryProvider Provider { get { return this.table; } }

        public override IEnumerator<TEntity> GetEnumerator()
        {
            return this.table.GetEnumerator();
        }

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            return (this.table as IQueryProvider).CreateQuery<TElement>(expression);
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            return (this.table as IQueryProvider).CreateQuery(expression);
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            return (this.table as IQueryProvider).Execute<TResult>(expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return (this.table as IQueryProvider).Execute(expression);
        }
    }
}
