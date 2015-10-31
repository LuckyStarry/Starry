﻿using System;
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
            this.DbContext = dbContext;
            this.DbMapping = this.DbContext.DbAssistor.DbMappings.GetDbMapping(typeof(TEntity));
        }

        public DbContext DbContext { private set; get; }

        public DbMapping DbMapping { private set; get; }

        public virtual IEnumerable<TEntity> GetList(object conditions = null, object order = null)
        {
            return this.DbContext.GetList<TEntity>(conditions, order);
        }

        public virtual IEnumerable<TEntity> GetPagedList(int pageIndex, int pageSize, out int total, object conditions = null, object order = null)
        {
            var assistor = this.DbContext.DbAssistor;
            var dbCommandSource = assistor.CreateDbCommandForGetList<TEntity>(conditions, order);
            dbCommandSource.Parameters.Add(assistor.ParameterNameRecordFrom, (pageIndex - 1) * pageSize + 1);
            dbCommandSource.Parameters.Add(assistor.ParameterNameRecordTo, pageIndex * pageSize);
            var dbCommand = this.DbContext.DbEntity.CreateDbCommand(dbCommandSource);
            var dataSet = this.DbContext.ExecuteDataSet(dbCommand);
            if (dataSet != null && dataSet.Tables.Count == 2)
            {
                total = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                var table = dataSet.Tables[1];
                if (table != null && table.Rows.Count > 0)
                {
                    return table.ToList<TEntity>(assistor.DbMappings.GetDbMapping(typeof(TEntity)));
                }
            }
            total = 0;
            return null;
        }

        public virtual int AddEntity(TEntity entity)
        {
            var dbCommandSource = this.DbContext.DbAssistor.CreateDbCommandForAddEntity(entity);
            var dbCommand = this.DbContext.DbEntity.CreateDbCommand(dbCommandSource);
            return this.DbContext.ExecuteNonQuery(dbCommand);
        }

        public virtual int UpdateEntity(TEntity entity)
        {
            var dbCommandSource = this.DbContext.DbAssistor.CreateDbCommandForUpdateEntity(entity);
            var dbCommand = this.DbContext.DbEntity.CreateDbCommand(dbCommandSource);
            return this.DbContext.ExecuteNonQuery(dbCommand);
        }

        public virtual int UpdateEntity(object entity, object conditions = null)
        {
            var dbCommandSource = this.DbContext.DbAssistor.CreateDbCommandForUpdateEntity(this.DbMapping.TableName, entity, conditions);
            var dbCommand = this.DbContext.DbEntity.CreateDbCommand(dbCommandSource);
            return this.DbContext.ExecuteNonQuery(dbCommand);
        }

        public virtual int DeleteEntity(TEntity entity)
        {
            var dbCommandSource = this.DbContext.DbAssistor.CreateDbCommandForDeleteEntity(entity);
            var dbCommand = this.DbContext.DbEntity.CreateDbCommand(dbCommandSource);
            return this.DbContext.ExecuteNonQuery(dbCommand);
        }

        public virtual int DeleteEntity(object conditions = null)
        {
            var dbCommandSource = this.DbContext.DbAssistor.CreateDbCommandForDeleteEntity(this.DbMapping.TableName, conditions);
            var dbCommand = this.DbContext.DbEntity.CreateDbCommand(dbCommandSource);
            return this.DbContext.ExecuteNonQuery(dbCommand);
        }
    }
}
