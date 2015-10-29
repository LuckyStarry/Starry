using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Starry.Data.Sql
{
    public static partial class DataTableExtend
    {
        public static IList<T> ToList<T>(this DataTable dataTable, DbMapping mapping) where T : new()
        {
            var results = new List<T>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                var columns = dataTable.Columns.Cast<DataColumn>().ToList();
                var properties = typeof(T).GetProperties();
                if (columns.Any() && properties.Any())
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        var entity = new T();
                        foreach (var cp in mapping.Columns)
                        {
                            if (!dr.IsNull(cp.ColumnName))
                            {
                                cp.PropertyInfo.SetValue(entity, dr[cp.ColumnName], null);
                            }
                        }
                        results.Add(entity);
                    }
                }
            }
            return results;
        }
    }
}
