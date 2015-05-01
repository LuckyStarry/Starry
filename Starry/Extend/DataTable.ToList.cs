using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Starry
{
    public static partial class DataTableExtend
    {
        public static IList<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var results = new List<T>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                var columns = dataTable.Columns.Cast<DataColumn>().ToList();
                var properties = typeof(T).GetProperties();
                if (columns.Any() && properties.Any())
                {
                    var mapping = columns
                        .Join(properties, c => c.ColumnName.ToLower(), p => p.Name.ToLower(), (c, p) => new { ColumnName = c.ColumnName, PropertyInfo = p })
                        .ToDictionary(cp => cp.ColumnName, cp => cp.PropertyInfo);

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        var entity = new T();
                        foreach (var cp in mapping)
                        {
                            if (!dr.IsNull(cp.Key))
                            {
                                cp.Value.SetValue(entity, dr[cp.Key], null);
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
