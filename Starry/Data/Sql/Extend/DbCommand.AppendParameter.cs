using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public static partial class DbCommandEx
    {
        public static void AppendParameter(this DbCommand dbCommand, string parameterName, object value)
        {
            var dbParam = dbCommand.CreateParameter();
            dbParam.ParameterName = parameterName;
            dbParam.Value = value;
            dbCommand.Parameters.Add(dbParam);
        }
    }
}
