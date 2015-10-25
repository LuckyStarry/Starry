using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.MySql
{
    public class MySqlEntity : DbEntity
    {
        public const string ProviderName = "MySql.Data.MySqlClient";
        public MySqlEntity(string connectionString)
            : base(connectionString, MySqlEntity.ProviderName)
        {
        }
    }
}
