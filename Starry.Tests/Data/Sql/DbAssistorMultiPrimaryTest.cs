using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Data.Sql
{
    [TestClass]
    public class DbAssistorMultiPrimaryTest
    {
        private TestDbContext InitDataContext()
        {
            var dbText = @"
DROP TABLE IF EXISTS  `TestTableMulti`;
CREATE TABLE `TestTableMulti` (
  `ID1` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID1',
  `ID2` int(11) NOT NULL COMMENT 'ID2',
  `TestContent` varchar(256) NOT NULL DEFAULT '' COMMENT 'TestContent',
  `CreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'CreateTime',
  `LastUpdateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'LastUpdateTime',
  PRIMARY KEY (`ID1`, `ID2`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;";

            var dataConext = new TestDbContext("Datasource=DataCenter-PC;Database=test;uid=root;pwd=P@ssw0rd;", "MySql.Data.MySqlClient");
            dataConext.ExecuteNonQuery(dbText);
            return dataConext;
        }

        [TestMethod]
        public void CRUDTest()
        {
            var dbContext = this.InitDataContext();
            var add = dbContext.TestTableMulti.AddEntity(new TestTableMulti { ID2 = int.Parse(DateTime.Now.ToString("HHmmss")), Content = "TEST1", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            Assert.AreEqual(add, 1, "ADD ENTITY ERROR");

            var list = dbContext.TestTableMulti.GetList();
            Assert.IsTrue(list != null && list.Any(), "GET LIST ERROR");

            var entity = list.FirstOrDefault();
            entity.Content = "TEST2";
            var up = dbContext.TestTableMulti.UpdateEntity(entity);
            Assert.AreEqual(up, 1, "UPDATE ENTITY ERROR");
            list = dbContext.TestTableMulti.GetList();
            entity = list.FirstOrDefault();
            Assert.AreEqual("TEST2", entity.Content, "UPDATE ENTITY ERROR");

            var result = dbContext.TestTableMulti.DeleteEntity(entity);
            Assert.AreEqual(result, 1, "DELETE ENTITY ERROR");

            dbContext.TestTableMulti.AddEntity(new TestTableMulti { ID2 = int.Parse(DateTime.Now.ToString("HHmmss")), Content = "TEST1", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            dbContext.TestTableMulti.AddEntity(new TestTableMulti { ID2 = int.Parse(DateTime.Now.ToString("HHmmss")), Content = "TEST2", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            dbContext.TestTableMulti.AddEntity(new TestTableMulti { ID2 = int.Parse(DateTime.Now.ToString("HHmmss")), Content = "TEST3", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            dbContext.TestTableMulti.AddEntity(new TestTableMulti { ID2 = int.Parse(DateTime.Now.ToString("HHmmss")), Content = "TEST4", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
            dbContext.TestTableMulti.AddEntity(new TestTableMulti { ID2 = int.Parse(DateTime.Now.ToString("HHmmss")), Content = "TEST5", CreateTime = DateTime.Now, LastUpdateTime = DateTime.Now });
        }
    }
}
