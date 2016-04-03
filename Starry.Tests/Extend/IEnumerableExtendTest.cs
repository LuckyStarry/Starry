using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Starry.Tests.Extend
{
    [TestClass]
    public class IEnumerableExtendTest
    {
        [TestMethod]
        public void ToDataTableFullTest()
        {
            var list = (
                from i in new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
                select new EnumerableEntityDef
                {
                    ID = i,
                    Guid = Guid.NewGuid(),
                    Value = Guid.NewGuid().ToString()
                }).ToList();
            this.ToDataTableTest(list);
        }

        [TestMethod]
        public void ToDataTableEmptyTest()
        {
            var list = new List<EnumerableEntityDef>();
            this.ToDataTableTest(list);
        }

        private void ToDataTableTest<T>(IList<T> list) where T : class
        {
            var entityProperties = list.GetType().GetGenericArguments()[0].GetProperties();
            var dataTable = IEnumerableExtend.ToDataTable(list);
            var dataTableColumns = dataTable.Columns;
            Assert.AreEqual(entityProperties.Length, dataTable.Columns.Count, "IEnumerable.ToDataTable convert columns failed");
            for (var i = 0; i < entityProperties.Length; i++)
            {
                Assert.AreEqual(entityProperties[i].Name, dataTable.Columns[i].ColumnName, "IEnumerable.ToDataTable convert column's name failed");
                Assert.AreEqual(entityProperties[i].PropertyType, dataTable.Columns[i].DataType, "IEnumerable.ToDataTable convert column's type failed");
            }
            Assert.AreEqual(list.Count(), dataTable.Rows.Count, "IEnumerable.ToDataTable convert rows failed");
            for (var i = 0; i < list.Count(); i++)
            {
                for (var j = 0; j < entityProperties.Length; j++)
                {
                    var v = entityProperties[j].GetValue(list[i], null);
                    Assert.AreEqual(v, dataTable.Rows[i][j], "IEnumerable.ToDataTable convert cell failed");
                }
            }
        }
    }
}
