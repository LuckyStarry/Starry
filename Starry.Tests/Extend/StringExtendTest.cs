using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Extend
{
    [TestClass]
    public class StringExtendTest
    {
        [TestMethod]
        public void ToStringTest()
        {
            var format1 = Guid.NewGuid().ToString();
            var arg0 = Guid.NewGuid().ToString();
            var arg1 = Guid.NewGuid().ToString();

            var format = "{0}" + format1 + "{1}";

            var expected = string.Format(format, arg0, arg1);
            var actual = StringExtend.ToString(format, arg0, arg1);

            Assert.AreEqual(expected, actual, "String.ToString format failed");
        }
    }
}
