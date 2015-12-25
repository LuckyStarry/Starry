using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbContext
    {
        public DbContext(string connectionName) : this(new DbEntity(connectionName)) { }
        public DbContext(string connectionString, string providerName) : this(new DbEntity(connectionString, providerName)) { }
        public DbContext(DbEntity dbEntity)
        {
            if (dbEntity == null)
            {
                throw new ArgumentNullException("dbEntity");
            }
            this.DbEntity = dbEntity;
            this.dbAssistor = DbAssistorFactory.Default.CreateDbAssistor(this.DbEntity);
        }

        private DbAssistor dbAssistor;
        public DbEntity DbEntity { private set; get; }
        public virtual DbAssistor DbAssistor { get { return this.dbAssistor; } }

        protected virtual T DbHandle<TDbCommand, T>(TDbCommand dbCommand, Func<TDbCommand, T> dbHandle)
            where TDbCommand : DbCommand
        {
            if (dbCommand.Connection == null)
            {
                dbCommand.Connection = this.DbEntity.CreateDbConnection();
            }
            if (dbCommand.Connection.State != ConnectionState.Open)
            {
                dbCommand.Connection.Open();
            }
            try
            {
                return dbHandle.Invoke(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand.Connection != null && dbCommand.Connection.State == ConnectionState.Open)
                {
                    dbCommand.Connection.Close();
                }
            }
        }

        protected internal int ExecuteNonQuery(DbCommand dbCommand)
        {
            return this.DbHandle(dbCommand, cmd => cmd.ExecuteNonQuery());
        }

        protected internal object ExecuteScalar(DbCommand dbCommand)
        {
            return this.DbHandle(dbCommand, cmd => cmd.ExecuteScalar());
        }

        protected internal T ExecuteScalar<T>(DbCommand dbCommand)
        {
            return this.ExecuteScalar(dbCommand, obj => (T)Convert.ChangeType(obj, typeof(T)));
        }

        protected internal T ExecuteScalar<T>(DbCommand dbCommand, Func<object, T> convert)
        {
            var objVal = this.ExecuteScalar(dbCommand);
            return convert.Invoke(objVal);
        }

        protected internal virtual DataSet ExecuteDataSet(DbCommand dbCommand)
        {
            return this.DbHandle(dbCommand, cmd => this.DbEntity.CreateDbDataAdapter(cmd).GetData());
        }

        protected internal virtual DataTable ExecuteDataTable(DbCommand dbCommand)
        {
            return this.DbHandle(dbCommand, cmd =>
            {
                var dataSet = this.DbEntity.CreateDbDataAdapter(cmd).GetData();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                return null;
            });
        }

        public virtual int ExecuteNonQuery(string commandText)
        {
            var command = this.DbEntity.CreateDbCommand(commandText);
            return this.ExecuteNonQuery(command);
        }

        public virtual object ExecuteScalar(string commandText)
        {
            var command = this.DbEntity.CreateDbCommand(commandText);
            return this.ExecuteScalar(command);
        }

        public virtual T ExecuteScalar<T>(string commandText)
        {
            return this.ExecuteScalar(commandText, obj => (T)Convert.ChangeType(obj, typeof(T)));
        }

        public virtual T ExecuteScalar<T>(string commandText, Func<object, T> convert)
        {
            var command = this.DbEntity.CreateDbCommand(commandText);
            return this.ExecuteScalar(command, convert);
        }

        public virtual DataSet ExecuteDataSet(string commandText)
        {
            var command = this.DbEntity.CreateDbCommand(commandText);
            return this.ExecuteDataSet(command);
        }

        public virtual DataTable ExecuteDataTable(string commandText)
        {
            var command = this.DbEntity.CreateDbCommand(commandText);
            return this.ExecuteDataTable(command);
        }

        public virtual IEnumerable<TEntity> GetList<TEntity>(object conditions = null, object order = null)
            where TEntity : new()
        {
            var dbCommandSource = this.DbAssistor.CreateDbCommandForGetList<TEntity>(conditions, order);
            var dbCommand = this.DbEntity.CreateDbCommand(dbCommandSource);
            var dataTable = this.ExecuteDataTable(dbCommand);
            return dataTable.ToList<TEntity>(this.DbAssistor.DbMappings.GetDbMapping(typeof(TEntity)));
        }

        public virtual Collections.IPagedList<TEntity> GetPagedList<TEntity>(int pageIndex, int pageSize, object conditions = null, object order = null)
            where TEntity : new()
        {
            var dbCommandSource = this.DbAssistor.CreateDbCommandForGetPagedList<TEntity>(pageIndex, pageSize, conditions, order);
            var dbCommand = this.DbEntity.CreateDbCommand(dbCommandSource);
            var dataSet = this.ExecuteDataSet(dbCommand);
            if (dataSet != null && dataSet.Tables.Count == 2)
            {
                var total = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                var list = dataSet.Tables[1].ToList<TEntity>(this.DbAssistor.DbMappings.GetDbMapping(typeof(TEntity)));
                return new Collections.PagedList<TEntity>(list, pageIndex, pageSize, total);
            }
            return null;
        }
    }
}
