using Starry.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Data.Sql
{
    [DbTable(TableName = "TestTableMulti", DbColumnOnly = true)]
    public class TestTableMulti
    {
        [DbPrimaryKey]
        public int ID1 { set; get; }
        [DbPrimaryKey(IngoreOnInsert = false)]
        public int ID2 { set; get; }
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
