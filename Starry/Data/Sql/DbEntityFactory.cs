using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public static class DbEntityFactory
    {
        private static Dictionary<string, Type> providers = new Dictionary<string, Type>();
        static DbEntityFactory()
        {
        }

        public static DbEntity CreateDbEntity(string connectionName)
        {
            var connectionSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName];
            if (connectionSetting == null)
            {
                throw new ArgumentOutOfRangeException("connectionName");
            }
            return CreateDbEntity(connectionSetting.ConnectionString, connectionSetting.ProviderName);
        }

        public static DbEntity CreateDbEntity(string connectionString, string providerName)
        {
            providerName = (providerName ?? string.Empty).ToLower();
            if (providers.ContainsKey(providerName))
            {
                var typeEntity = providers[providerName];
                var dbEntity = Activator.CreateInstance(typeEntity, new object[] { connectionString }) as DbEntity;
                if (dbEntity == null)
                {
                    throw new Exception(string.Format("Type {0} initialize failed", typeEntity.FullName));
                }
                return dbEntity;
            }
            else
            {
                throw new ArgumentOutOfRangeException("providerName", string.Format("ProviderName {0} havn't been registering"));
            }
        }

        public static void Register(string providerName, Type dbEntityType)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException("providerName");
            }
            if (dbEntityType == null)
            {
                throw new ArgumentNullException("dbEntityType");
            }
            var dbEntityName = typeof(DbEntity).FullName;
            if (dbEntityType.GetInterface(dbEntityName, true) == null)
            {
                throw new ArgumentOutOfRangeException("dbEntityType", string.Format("Type {0} must implement from {1}", dbEntityType, dbEntityName));
            }
            providerName = providerName.Trim().ToLower();
            lock (providers)
            {
                if (providers.ContainsKey(providerName))
                {
                    providers[providerName] = dbEntityType;
                }
                else
                {
                    providers.Add(providerName, dbEntityType);
                }
            }
        }
    }
}
