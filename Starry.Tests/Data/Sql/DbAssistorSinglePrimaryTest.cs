using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Data.Sql
{
    [TestClass]
    public class DbAssistorSinglePrimaryTest
    {
        private TestDbContext InitDataContext()
        {
            var dbText = @"
DROP TABLE IF EXISTS  `TestTable`;
CREATE TABLE `TestTable` (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `TestContent` varchar(256) NOT NULL DEFAULT '' COMMENT 'TestContent',
  `CreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'CreateTime',
  `LastUpdateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'LastUpdateTime',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;";

            var dataConext = new TestDbContext("Datasource=DataCenter-PC;Database=test;uid=root;pwd=P@ssw0rd;", "MySql.Data.MySqlClient");
            dataConext.ExecuteNonQuery(dbText);
            return dataConext;
        }

        [TestMethod]
        public void CRUDTest()
        {
            var dbContext = this.InitDataContext();
            var add = dbContext.TestTable.AddEntity(new TestEntity { Content = "TEST1", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            Assert.AreEqual(add, 1, "ADD ENTITY ERROR");

            var list = dbContext.TestTable.GetList();
            Assert.IsTrue(list != null && list.Any(), "GET LIST ERROR");

            var entity = list.FirstOrDefault();
            entity.Content = "TEST2";
            var up = dbContext.TestTable.UpdateEntity(entity);
            Assert.AreEqual(up, 1, "UPDATE ENTITY ERROR");
            list = dbContext.TestTable.GetList();
            entity = list.FirstOrDefault();
            Assert.AreEqual("TEST2", entity.Content, "UPDATE ENTITY ERROR");

            var result = dbContext.TestTable.DeleteEntity(entity);
            Assert.AreEqual(result, 1, "DELETE ENTITY ERROR");

            dbContext.TestTable.AddEntity(new TestEntity { Content = "TEST1", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            dbContext.TestTable.AddEntity(new TestEntity { Content = "TEST2", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            dbContext.TestTable.AddEntity(new TestEntity { Content = "TEST3", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            dbContext.TestTable.AddEntity(new TestEntity { Content = "TEST4", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            dbContext.TestTable.AddEntity(new TestEntity { Content = "TEST5", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });

            var pagedlist = dbContext.TestTable.GetPagedList(1, 20);
            Assert.IsTrue(pagedlist != null, "[dbContext.TestTable.GetPagedList(1, 20)] GET PAGED LIST ERROR");
            Assert.IsTrue(pagedlist.Count() == 5, "[dbContext.TestTable.GetPagedList(1, 20)] GET PAGED LIST ITEM COUNT ERROR");

            pagedlist = dbContext.TestTable.GetPagedList(2, 3, order: new { id = "DESC" });
            Assert.IsTrue(pagedlist != null, "[dbContext.TestTable.GetPagedList(2, 3, order: new { id = \"DESC\" })] GET PAGED LIST ERROR");
            Assert.IsTrue(pagedlist.TotalItemCount == 5, "[dbContext.TestTable.GetPagedList(2, 3, order: new { id = \"DESC\" })] GET PAGED LIST ITEM TOTAL COUNT ERROR");
            Assert.IsTrue(pagedlist.Count() == 2, "[dbContext.TestTable.GetPagedList(2, 3, order: new { id = \"DESC\" })] GET PAGED LIST ITEM COUNT ERROR");
            Assert.IsTrue(pagedlist.First().Content == "TEST2", "[dbContext.TestTable.GetPagedList(2, 3, order: new { id = \"DESC\" })] GET PAGED LIST ITEM ORDER ERROR");

            pagedlist = dbContext.TestTable.GetPagedList(1, 5, new { testcontent = "TEST4" }, new { id = "DESC" });
            Assert.IsTrue(pagedlist.TotalItemCount == 1, "[dbContext.TestTable.GetPagedList(1, 5, new { testcontent = \"TEST4\" }, new { id = \"DESC\" })] GET PAGED LIST ITEM TOTAL COUNT ERROR");

            pagedlist = dbContext.TestTable.GetPagedList(1, 3, new { id = 0 }, new { id = "DESC" });
            Assert.IsTrue(pagedlist != null, "[dbContext.TestTable.GetPagedList(1, 3, new { id = 0 }, new { id = \"DESC\" })] GET PAGED LIST ERROR");
            Assert.IsTrue(pagedlist.TotalItemCount == 0, "[dbContext.TestTable.GetPagedList(1, 3, new { id = 0 }, new { id = \"DESC\" })] GET PAGED LIST ERROR");
        }
    }
}
