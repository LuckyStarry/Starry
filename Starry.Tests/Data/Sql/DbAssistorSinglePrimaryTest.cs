﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var dataConext = new TestDbContext("Datasource=localhost;Database=test;uid=root;pwd=P@ssw0rd;", "MySql.Data.MySqlClient");
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
        }
    }
}
