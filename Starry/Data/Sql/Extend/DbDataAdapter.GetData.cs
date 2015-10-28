using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public static partial class DbDataAdapterEx
    {
        public static DataSet GetData(this DbDataAdapter dbDataAdapter)
        {
            var dataSet = new DataSet();
            dbDataAdapter.Fill(dataSet);
            return dataSet;
        }
    }
}
