using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbContextFactory
    {
        private IDictionary<string, DbCommandGenerator> dbCommandGenerators = new Dictionary<string, DbCommandGenerator>();

        public static DbContextFactory Default { get { return DbEntityFactoryDefault.Instance; } }

        public virtual DbContext CreateDbContext(string connectionName)
        {
            var connectionSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName];
            if (connectionSetting == null)
            {
                throw new ArgumentOutOfRangeException("connectionName");
            }
            return CreateDbContext(connectionSetting.ConnectionString, connectionSetting.ProviderName);
        }

        public virtual DbContext CreateDbContext(string connectionString, string providerName)
        {
            var dbEntity = new DbEntity(connectionString, providerName);
            providerName = (providerName ?? string.Empty).ToLower();
            if (dbCommandGenerators.ContainsKey(providerName))
            {
                return new DbContext(dbEntity, dbCommandGenerators[providerName]);
            }
            else
            {
                throw new ArgumentOutOfRangeException("providerName", string.Format("ProviderName {0} havn't been registering"));
            }
        }

        public void Register(string providerName, DbCommandGenerator dbCommandGenerator)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException("providerName");
            }
            if (dbCommandGenerator == null)
            {
                throw new ArgumentNullException("dbCommandGenerator");
            }
            providerName = providerName.Trim().ToLower();
            lock (this.dbCommandGenerators)
            {
                if (this.dbCommandGenerators.ContainsKey(providerName))
                {
                    this.dbCommandGenerators[providerName] = dbCommandGenerator;
                }
                else
                {
                    this.dbCommandGenerators.Add(providerName, dbCommandGenerator);
                }
            }
        }
    }
}
