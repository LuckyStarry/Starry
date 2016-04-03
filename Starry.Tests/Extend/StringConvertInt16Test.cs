using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Extend
{
    [TestClass]
    public class StringConvertInt16Test
    {
        /// <summary>
        /// Zero:0
        /// </summary>
        private const string zero = "0";
        /// <summary>
        /// ZeroMinus:-0
        /// </summary>
        private const string minusZero = "-0";
        /// <summary>
        /// Minus:-1
        /// </summary>
        private const string minus = "-1";
        /// <summary>
        /// Int16.MaxValue+1:32767+1=32768
        /// </summary>
        private const string overflowMax = "32768";
        /// <summary>
        /// Int16.MinValue-1:-32768-1=-32769
        /// </summary>
        private const string overflowMin = "-32769";
        /// <summary>
        /// Alpha:aaa
        /// </summary>
        private const string alpha = "aaa";
        /// <summary>
        /// Null:null
        /// </summary>
        private const string @null = (string)null;

        [TestMethod]
        public void IsInt16Test()
        {
            this.TestIsInt16(zero, true);
            this.TestIsInt16(minusZero, true);
            this.TestIsInt16(minus, true);
            this.TestIsInt16(overflowMax, false);
            this.TestIsInt16(overflowMin, false);
            this.TestIsInt16(alpha, false);
            this.TestIsInt16(@null, false);
        }

        [TestMethod]
        public void ToInt16Test()
        {
            this.TestToInt16(zero, 0);
            this.TestToInt16(minusZero, 0);
            this.TestToInt16(minus, -1);
            this.TestToInt16(overflowMax, 0, true);
            this.TestToInt16(overflowMin, 0, true);
            this.TestToInt16(alpha, 0, true);
            this.TestToInt16(@null, 0, true);
        }

        [TestMethod]
        public void TryToInt16DefaultZeroTest()
        {
            this.TestTryToInt16DefaultZero(zero, 0);
            this.TestTryToInt16DefaultZero(minusZero, 0);
            this.TestTryToInt16DefaultZero(minus, -1);
            this.TestTryToInt16DefaultZero(overflowMax, 0);
            this.TestTryToInt16DefaultZero(overflowMin, 0);
            this.TestTryToInt16DefaultZero(alpha, 0);
            this.TestTryToInt16DefaultZero(@null, 0);
        }

        [TestMethod]
        public void TryToInt16DefaultMinusTest()
        {
            this.TestTryToInt16DefaultMinus(zero, 0);
            this.TestTryToInt16DefaultMinus(minusZero, 0);
            this.TestTryToInt16DefaultMinus(minus, -1);
            this.TestTryToInt16DefaultMinus(overflowMax, -1);
            this.TestTryToInt16DefaultMinus(overflowMin, -1);
            this.TestTryToInt16DefaultMinus(alpha, -1);
            this.TestTryToInt16DefaultMinus(@null, -1);
        }

        private void TestIsInt16(string value, bool expected)
        {
            Assert.AreEqual(expected, StringExtend.IsInt16(value), "String.IsInt16 Test [{0}] Failed", value);
        }

        private void TestToInt16(string value, Int16 expected, bool isException = false)
        {
            try
            {
                if (isException)
                {
                    var actual = StringExtend.ToInt16(value);
                    Assert.Fail("String.ToInt16 Convert [{0}] Failed, the exception should be happened but not", value);
                }
                else
                {
                    Assert.AreEqual(expected, StringExtend.ToInt16(value), "String.ToInt16 Convert [{0}] Failed", value);
                }
            }
            catch
            {
                if (!isException)
                {
                    Assert.Fail("String.ToInt16 Convert [{0}] Failed, exception happened", value);
                }
            }
        }

        private void TestTryToInt16DefaultZero(string value, Int16 expected)
        {
            var actual = StringExtend.TryToInt16(value);
            Assert.AreEqual(expected, actual, "String.TryToInt16 Convert [{0}] Failed, actual is [{1}]", value, actual);
        }

        private void TestTryToInt16DefaultMinus(string value, Int16 expected)
        {
            var actual = StringExtend.TryToInt16(value, -1);
            Assert.AreEqual(expected, actual, "String.TryToInt16 Convert [{0}] Failed, actual is [{1}]", value, actual);
        }
    }
}
