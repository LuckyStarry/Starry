using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Starry
{
    public static partial class IEnumerableExtend
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable) where T : class
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var dataTable = new DataTable();
            foreach (var property in properties)
            {
                var column = new DataColumn(property.Name, property.PropertyType);
                dataTable.Columns.Add(column);
            }
            if (enumerable == null)
            {
                return dataTable;
            }
            foreach (var item in enumerable)
            {
                var row = dataTable.NewRow();
                foreach (var property in properties)
                {
                    var value = property.GetValue(item, null);
                    if (value != null)
                    {
                        row[property.Name] = value;
                    }
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
