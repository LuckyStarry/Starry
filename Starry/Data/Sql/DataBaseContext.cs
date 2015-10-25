using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DataBaseContext
    {
        public DataBaseContext(DataBaseEntity dataBaseEntity)
        {
            this.DataBase = dataBaseEntity;
        }

        public DataBaseEntity DataBase { private set; get; }

        protected virtual T DbHandle<TDbCommand, T>(TDbCommand dbCommand, Func<TDbCommand, T> dbHandle)
            where TDbCommand : DbCommand
        {
            if (dbCommand.Connection == null)
            {
                dbCommand.Connection = this.DataBase.CreateDbConnection();
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

        public int ExecuteNonQuery(DbCommand dbCommand)
        {
            return this.DbHandle(dbCommand, cmd => cmd.ExecuteNonQuery());
        }

        public object ExecuteScalar(DbCommand dbCommand)
        {
            return this.DbHandle(dbCommand, cmd => cmd.ExecuteScalar());
        }

        public T ExecuteScalar<T>(DbCommand dbCommand)
        {
            return this.ExecuteScalar(dbCommand, obj => (T)Convert.ChangeType(obj, typeof(T)));
        }

        public T ExecuteScalar<T>(DbCommand dbCommand, Func<object, T> convert)
        {
            var objVal = this.ExecuteScalar(dbCommand);
            return convert.Invoke(objVal);
        }

        public virtual DataSet ExecuteDataSet(DbCommand dbCommand)
        {
            return this.DbHandle(dbCommand, cmd => this.DataBase.CreateDbDataAdapter(cmd).GetData());
        }

        public virtual DataTable ExecuteDataTable(DbCommand dbCommand)
        {
            return this.DbHandle(dbCommand, cmd =>
            {
                var dataSet = this.DataBase.CreateDbDataAdapter(cmd).GetData();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                return null;
            });
        }

        public virtual int ExecuteNonQuery(string commandText)
        {
            var command = this.DataBase.CreateDbCommand(commandText);
            return this.ExecuteNonQuery(command);
        }

        public virtual object ExecuteScalar(string commandText)
        {
            var command = this.DataBase.CreateDbCommand(commandText);
            return this.ExecuteScalar(command);
        }

        public virtual T ExecuteScalar<T>(string commandText)
        {
            return this.ExecuteScalar(commandText, obj => (T)Convert.ChangeType(obj, typeof(T)));
        }

        public virtual T ExecuteScalar<T>(string commandText, Func<object, T> convert)
        {
            var command = this.DataBase.CreateDbCommand(commandText);
            return this.ExecuteScalar(command, convert);
        }

        public virtual DataSet ExecuteDataSet(string commandText)
        {
            var command = this.DataBase.CreateDbCommand(commandText);
            return this.ExecuteDataSet(command);
        }

        public virtual DataTable ExecuteDataTable(string commandText)
        {
            var command = this.DataBase.CreateDbCommand(commandText);
            return this.ExecuteDataTable(command);
        }
    }
}
