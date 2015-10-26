using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbMapping
    {
        public string TableName { set; get; }
        public bool DbColumnOnly { set; get; }

        public DbColumn[] Columns { set; get; }
    }
}
