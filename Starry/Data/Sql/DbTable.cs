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
            this.DbMapping = DbMappingCollection.Default.GetDbMapping(typeof(TEntity));
            this.TableName = this.DbMapping.TableName;
        }

        public string TableName { private set; get; }
        public DbContext DbContext { private set; get; }
        public DbMapping DbMapping { private set; get; }

        public virtual IEnumerable<TEntity> GetList(object conditions = null, object order = null)
        {
            var dbCommand = this.DbContext.DbCommandGenerator.CreateDbCommandForGetList<TEntity>(conditions, order);
            var dataTable = this.DbContext.ExecuteDataTable(dbCommand);
            return dataTable.ToList<TEntity>();
        }

        public virtual int AddEntity(TEntity entity)
        {
            var dbCommand = this.DbContext.DbCommandGenerator.CreateDbCommandForAddEntity(entity);
            return this.DbContext.ExecuteNonQuery(dbCommand);
        }
    }
}
