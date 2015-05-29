using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Assistant.Extend
{
    public static partial class IDbContextExtend
    {
        public static T ExecuteScalar<T>(this IDbContext dbContext, string sqlCommandText)
        {
            return (T)dbContext.ExecuteScalar(sqlCommandText);
        }
    }
}
