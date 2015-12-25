using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbMapping
    {
        public string TableName { internal set; get; }
        public bool DbColumnOnly { internal set; get; }
        public DbColumn[] Columns { internal set; get; }
    }
}
