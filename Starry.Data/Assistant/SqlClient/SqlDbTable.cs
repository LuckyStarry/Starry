using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Assistant.SqlClient
{
    public class SqlDbTable<TEntity> : DbTable<TEntity>
        where TEntity : new()
    {
        public SqlDbTable(SqlDbContext dbContext) : base(dbContext) { }

        public override IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)this.Provider.Execute(this.Expression)).GetEnumerator();
        }

        public override System.Linq.Expressions.Expression Expression
        {
            get { throw new NotImplementedException(); }
        }

        public override IQueryProvider Provider
        {
            get { throw new NotImplementedException(); }
        }
    }
}
