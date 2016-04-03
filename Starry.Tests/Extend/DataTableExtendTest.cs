using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Starry.Tests.Extend
{
    [TestClass]
    public class DataTableExtendTest
    {
        private DataTable CreateNewTable()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Guid", typeof(Guid)));
            dataTable.Columns.Add(new DataColumn("Value", typeof(string)));
            return dataTable;
        }

        [TestMethod]
        public void ToListFullTest()
        {
            var dataTable = this.CreateNewTable();
            for (var i = 0; i < 10; i++)
            {
                var row = dataTable.NewRow();
                row[0] = i + 1;
                row[1] = Guid.NewGuid();
                row[2] = Guid.NewGuid().ToString();
            }
            this.ToListTest(dataTable);
        }

        [TestMethod]
        public void ToListEmptyTest()
        {
            var dataTable = this.CreateNewTable();
            this.ToListTest(dataTable);
        }

        private void ToListTest(DataTable dataTable)
        {
            var list = DataTableExtend.ToList<EnumerableEntityDef>(dataTable);
            var entityProperties = list.GetType().GetGenericArguments()[0].GetProperties();
            var dataTableColumns = dataTable.Columns;
            Assert.AreEqual(dataTable.Columns.Count, entityProperties.Length, "DataTable.ToList convert columns failed");
            for (var i = 0; i < dataTable.Columns.Count; i++)
            {
                Assert.AreEqual(dataTable.Columns[i].ColumnName, entityProperties[i].Name, "DataTable.ToList convert column's name failed");
                Assert.AreEqual(dataTable.Columns[i].DataType, entityProperties[i].PropertyType, "DataTable.ToList convert column's type failed");
            }
            Assert.AreEqual(dataTable.Rows.Count, list.Count(), "DataTable.ToList convert rows failed");
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                for (var j = 0; j < dataTable.Columns.Count; j++)
                {
                    var v = entityProperties[j].GetValue(list[i], null);
                    Assert.AreEqual(dataTable.Rows[i][j], v, "DataTable.ToList convert cell failed");
                }
            }
        }
    }
}
