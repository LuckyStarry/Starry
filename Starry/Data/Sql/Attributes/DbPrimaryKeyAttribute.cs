using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DbPrimaryKeyAttribute : DbColumnAttribute
    {
        public bool IngoreOnInsert { set; get; }
    }
}
