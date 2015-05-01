using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Starry.Data.Assistant
{
    public class DbTable<TEntity> : IDbTable<TEntity>
        where TEntity : new()
    {
        private string[] columnNames;

        public DbTable(IDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.columnNames = typeof(TEntity).GetProperties().Select(p => p.Name).ToArray();
        }

        public IDbContext DbContext { private set; get; }

        public IEnumerable<TEntity> GetList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
