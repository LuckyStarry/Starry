using Starry.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Data.Sql
{
    [DbTable(TableName = "TestTable", DbColumnOnly = true)]
    public class TestEntity
    {
        [DbPrimaryKey]
        public int ID { set; get; }
        [DbColumn(ColumnName = "TestContent")]
        public string Content { set; get; }
        [DbColumn]
        public DateTime CreateTime { set; get; }
        [DbColumn]
        public DateTime LastUpdateTime { set; get; }
        [DbIgnore]
        public string Ingore { set; get; }
        public string Cancel { set; get; }
    }
}
