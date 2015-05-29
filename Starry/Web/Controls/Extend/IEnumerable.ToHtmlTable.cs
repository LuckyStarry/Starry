using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IEnumerableExtend
    {
        public static HtmlTable ToHtmlTable(this IEnumerable dataList)
        {
            if (dataList == null)
            {
                throw new ArgumentNullException("dataList");
            }

            var isGeneric = false;
            PropertyInfo[] properties = null;
            var genericArguments = dataList.GetType().GetGenericArguments();
            if (genericArguments != null && genericArguments.Any())
            {
                var genericArgument = genericArguments.First();
                properties = genericArgument.GetProperties();
                isGeneric = true;
            }
            else
            {
                var enumerator = dataList.GetEnumerator();
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentOutOfRangeException("dataList", "dataList can't be empty");
                }
                var firstElement = enumerator.Current;
                if (firstElement == null)
                {
                    throw new ArgumentOutOfRangeException("dataList", "dataList's first element can't be null");
                }
                properties = firstElement.GetType().GetProperties();
            }
            var columns = properties.Select(p => p.Name);
            return dataList.ToHtmlTable(columns, data =>
            {
                var tr = new HtmlTR();
                var propertyInfos = isGeneric ? properties : data.GetType().GetProperties();
                foreach (var property in propertyInfos)
                {
                    var text = string.Empty;
                    var value = property.GetValue(data, null);
                    if (value != null)
                    {
                        text = value.ToString();
                    }
                    tr.Append(new HtmlTD(text));
                }
                return tr;
            });
        }

        public static HtmlTable ToHtmlTable(this IEnumerable dataList, IEnumerable columns, Func<object, HtmlTR> generateRow)
        {
            var head = new HtmlTR();
            foreach (var column in columns)
            {
                head.Append(new HtmlTH(column.ToString()));
            }
            return dataList.ToHtmlTable(head, generateRow);
        }

        public static HtmlTable ToHtmlTable(this IEnumerable dataList, HtmlTR trHead, Func<object, HtmlTR> generateRow)
        {
            if (dataList == null)
            {
                throw new ArgumentNullException("dataList");
            }
            var table = new HtmlTable();
            var thead = new HtmlTHead();
            var tbody = new HtmlTBody();

            if (dataList != null)
            {
                foreach (var data in dataList)
                {
                    tbody.Append(generateRow(data));
                }
            }

            return table.Append(thead.Append(trHead), tbody);
        }

        public static HtmlTable ToHtmlTable<T>(this IEnumerable<T> dataList)
        {
            var properties = typeof(T).GetProperties();
            return dataList.ToHtmlTable(data =>
            {
                var tr = new HtmlTR();
                foreach (var property in properties)
                {
                    var text = string.Empty;
                    var value = property.GetValue(data, null);
                    if (value != null)
                    {
                        text = value.ToString();
                    }
                    tr.Append(new HtmlTD(text));
                }
                return tr;
            });
        }

        public static HtmlTable ToHtmlTable<T>(this IEnumerable<T> dataList, Func<T, HtmlTR> generateRow)
        {
            var properties = typeof(T).GetProperties();
            return dataList.ToHtmlTable(properties.Select(p => p.Name), obj => { return generateRow((T)obj); });
        }
    }
}
