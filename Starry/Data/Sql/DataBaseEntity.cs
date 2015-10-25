using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public abstract class DataBaseEntity
    {
        protected DataBaseEntity(string connectionName)
        {
            this.ConnectionName = connectionName;
            var connectionSettings = System.Configuration.ConfigurationManager.ConnectionStrings[this.ConnectionName];
            if (connectionSettings == null)
            {
                throw new ArgumentOutOfRangeException("connectionName");
            }
            this.ConnectionString = connectionSettings.ConnectionString;
            this.ProviderName = connectionSettings.ProviderName;
        }

        public string ConnectionString { private set; get; }
        public string ProviderName { private set; get; }
        public string ConnectionName { private set; get; }

        private DbProviderFactory _provider;
        protected virtual DbProviderFactory DbProviderFactory
        {
            get
            {
                if (this._provider == null)
                {
                    this._provider = DbProviderFactories.GetFactory(this.ProviderName);
                }
                return this._provider;
            }
        }

        public virtual DbConnection CreateDbConnection()
        {
            var dbConnection = this.DbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = this.ConnectionString;
            return dbConnection;
        }

        public virtual DbCommand CreateDbCommand(string commandText)
        {
            var dbCommand = this.DbProviderFactory.CreateCommand();
            dbCommand.CommandText = commandText;
            return dbCommand;
        }

        public virtual DbCommand CreateDbCommand(string commandText, DbConnection dbConnection)
        {
            var dbCommand = this.CreateDbCommand(commandText);
            dbCommand.Connection = dbConnection;
            return dbCommand;
        }

        public virtual DbCommand CreateDbCommand(string commandText, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            var dbCommand = this.CreateDbCommand(commandText, dbConnection);
            dbCommand.Transaction = dbTransaction;
            return dbCommand;
        }

        public virtual DbDataAdapter CreateDbDataAdapter(string commandText)
        {
            var dbCommand = this.DbProviderFactory.CreateCommand();
            return this.CreateDbDataAdapter(dbCommand);
        }

        public virtual DbDataAdapter CreateDbDataAdapter(DbCommand selectCommand)
        {
            var dbAdapter = this.DbProviderFactory.CreateDataAdapter();
            dbAdapter.SelectCommand = selectCommand;
            return dbAdapter;
        }
    }
}
