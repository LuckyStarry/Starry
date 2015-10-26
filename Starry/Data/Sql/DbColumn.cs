using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbColumn
    {
        public DbColumn()
        {
            this.IsPrimaryKey = false;
        }
        internal PropertyInfo PropertyInfo { set; get; }
        public string ColumnName { set; get; }
        public DbType DbType { set; get; }
        public bool IsPrimaryKey { set; get; }
    }
}
