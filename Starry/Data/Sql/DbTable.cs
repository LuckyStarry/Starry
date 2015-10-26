using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbTable<TEntity>
        where TEntity : new()
    {
        public DbTable(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            this.Mapping = DbMappingCollection.Default.GetDbMapping(typeof(TEntity));
            this.TableName = this.Mapping.TableName;
        }

        public DbContext DbContext { private set; get; }
        public string TableName { private set; get; }
        public DbMapping Mapping { private set; get; }

        public virtual IEnumerable<TEntity> GetList(object conditions = null, object order = null)
        {
            var dbCommand = this.DbContext.DbEntity.DbGenerator.CreateDbCommandForGetList<TEntity>(conditions, order);
            var dataTable = this.DbContext.ExecuteDataTable(dbCommand);
            return dataTable.ToList<TEntity>();
        }

        public virtual int AddEntity(TEntity entity)
        {
            var dbCommand = this.DbContext.DbEntity.DbGenerator.CreateDbCommandForAddEntity(entity);
            return this.DbContext.ExecuteNonQuery(dbCommand);
        }
    }
}
