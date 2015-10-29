using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DbTableAttribute : Attribute
    {
        public DbTableAttribute()
        {
            this.DbColumnOnly = false;
        }

        public string TableName { set; get; }
        public bool DbColumnOnly { set; get; }
    }
}
