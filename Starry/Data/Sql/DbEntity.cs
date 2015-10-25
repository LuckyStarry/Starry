using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbEntity
    {
        protected internal DbEntity(string connectionName)
        {
            this.ConnectionName = connectionName;
            var connectionSettings = System.Configuration.ConfigurationManager.ConnectionStrings[this.ConnectionName];
            if (connectionSettings == null)
            {
                throw new ArgumentOutOfRangeException("connectionName");
            }
            this.ConnectionString = connectionSettings.ConnectionString;
            this.provider = DbProviderFactories.GetFactory(connectionSettings.ProviderName);
        }

        protected internal DbEntity(string connectionString, string providerName)
        {
            this.ConnectionName = string.Empty;
            this.ConnectionString = connectionString;
            this.provider = DbProviderFactories.GetFactory(providerName);
        }

        public string ConnectionString { private set; get; }
        public string ConnectionName { private set; get; }
        //TODO get "@" on ms-sql / "?" on mysql / ":" on oracle 
        public virtual string ParameterPrefix { get { return "@"; } }

        private DbProviderFactory provider;
        protected virtual DbProviderFactory DbProviderFactory
        {
            get { return this.provider; }
        }

        public virtual DbConnection CreateDbConnection()
        {
            var dbConnection = this.DbProviderFactory.CreateConnection();
            dbConnection.ConnectionString = this.ConnectionString;
            return dbConnection;
        }

        public virtual DbCommand CreateDbCommand()
        {
            var dbCommand = this.DbProviderFactory.CreateCommand();
            return dbCommand;
        }

        public virtual DbCommand CreateDbCommand(string commandText)
        {
            var dbCommand = this.CreateDbCommand();
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
