using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbEntity
    {
        public DbEntity(string connectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
            {
                throw new ArgumentNullException("connectionName");
            }
            var connectionSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName];
            if (connectionSetting == null)
            {
                throw new ArgumentOutOfRangeException("connectionName");
            }
            this.Initialize(connectionSetting.ConnectionString, connectionSetting.ProviderName);
        }

        public DbEntity(string connectionString, string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException("providerName");
            }
            this.Initialize(connectionString, providerName);
        }

        private void Initialize(string connectionString, string providerName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException("providerName");
            }
            var dbProviderFactory = DbProviderFactories.GetFactory(providerName);
            if (dbProviderFactory == null)
            {
                throw new ArgumentException("Generate DbProviderFactory Failed", "providerName");
            }
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
            this.DbProviderFactory = dbProviderFactory;
        }

        public string ConnectionString { private set; get; }

        public string ProviderName { private set; get; }

        protected DbProviderFactory DbProviderFactory { private set; get; }

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

        public virtual DbCommand CreateDbCommand(DbCommandSource dbCommandSource)
        {
            var dbCommand = this.CreateDbCommand(dbCommandSource.CommandText);
            foreach (var parameter in dbCommandSource.Parameters)
            {
                dbCommand.AppendParameter(parameter.Key, parameter.Value);
            }
            return dbCommand;
        }
    }
}
