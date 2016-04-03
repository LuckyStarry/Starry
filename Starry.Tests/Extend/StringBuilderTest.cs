using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Starry.Tests.Extend
{
    [TestClass]
    public class StringBuilderTest
    {
        [TestMethod]
        public void AppendLineTest()
        {
            var format1 = Guid.NewGuid().ToString();
            var format2 = Guid.NewGuid().ToString();
            var arg0 = Guid.NewGuid().ToString();
            var arg1 = Guid.NewGuid().ToString();
            var arg2 = Guid.NewGuid().ToString();
            var arg3 = Guid.NewGuid().ToString();

            var sbTestActual = new StringBuilder();
            sbTestActual.AppendLine(format1 + "{0}{1}", arg0, arg1);
            sbTestActual.AppendLine(format2 + "{0}{1}", arg2, arg3);

            var sbTestExpected = new StringBuilder();
            sbTestExpected.AppendFormat(format1 + "{0}{1}", arg0, arg1);
            sbTestExpected.AppendLine();
            sbTestExpected.AppendFormat(format2 + "{0}{1}", arg2, arg3);
            sbTestExpected.AppendLine();

            var expected = sbTestExpected.ToString();
            var actual = sbTestActual.ToString();

            Assert.AreEqual(expected, actual, "StringBuilder.AppendLine format failed");
        }
    }
}
