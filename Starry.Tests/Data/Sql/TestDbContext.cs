using Starry.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Data.Sql
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(string connectionString, string providerName) : base(connectionString, providerName) { }
        public DbTable<TestEntity> TestTable { get { return new DbTable<TestEntity>(this); } }
        public DbTable<TestTableMulti> TestTableMulti { get { return new DbTable<TestTableMulti>(this); } }
    }
}
