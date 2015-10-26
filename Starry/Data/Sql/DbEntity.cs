﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbEntity
    {
        protected internal DbEntity(string connectionString, DbProviderFactory dbProviderFactory, DbGenerator dbGenerator)
        {
            this.Initialize(connectionString, dbProviderFactory, dbGenerator);
        }

        protected internal DbEntity(string connectionString, string providerName, DbGenerator dbGenerator)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException("providerName");
            }
            this.Initialize(connectionString, DbProviderFactories.GetFactory(providerName), dbGenerator);
        }

        internal DbEntity(string connectionName, DbGenerator dbGenerator)
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
            if (string.IsNullOrWhiteSpace(connectionSetting.ProviderName))
            {
                throw new ArgumentNullException("providerName");
            }
            this.Initialize(connectionSetting.ConnectionString, DbProviderFactories.GetFactory(connectionSetting.ProviderName), dbGenerator);
        }

        private void Initialize(string connectionString, DbProviderFactory dbProviderFactory, DbGenerator dbGenerator)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (dbProviderFactory == null)
            {
                throw new ArgumentNullException("dbProviderFactory");
            }
            if (dbGenerator == null)
            {
                throw new ArgumentNullException("dbGenerator");
            }
            this.ConnectionString = connectionString;
            this.DbProviderFactory = dbProviderFactory;
            this.DbGenerator = dbGenerator;
            this.DbGenerator.DbEntity = this;
        }

        public string ConnectionString { private set; get; }

        public DbGenerator DbGenerator { private set; get; }

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
    }
}
