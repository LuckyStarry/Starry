using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbAssistorFactory
    {
        public static DbAssistorFactory Default { get { return DbAssistorFactoryDefault.Instance; } }

        public virtual DbAssistor CreateDbAssistor(DbEntity dbEntity)
        {
            if (dbEntity == null)
            {
                throw new ArgumentNullException("dbEntity");
            }
            switch ((dbEntity.ProviderName ?? string.Empty).ToLower())
            {
                case "mysql.data.mysqlclient": return new MySql.MySqlAssistor();
            }
            return null;
        }
    }
}
