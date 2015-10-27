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
            return this.DbContext.GetList<TEntity>(conditions, order);
        }

        public virtual int AddEntity(TEntity entity)
        {
            var dbCommandSource = this.DbContext.DbAssistor.CreateDbCommandForAddEntity(entity);
            var dbCommand = this.DbContext.DbEntity.CreateDbCommand(dbCommandSource);
            return this.DbContext.ExecuteNonQuery(dbCommand);
        }
    }
}
