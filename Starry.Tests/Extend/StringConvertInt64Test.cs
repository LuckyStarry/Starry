using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Extend
{
    [TestClass]
    public class StringConvertInt64Test
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
        /// Int64.MaxValue+1:9223372036854775807+1=9223372036854775808
        /// </summary>
        private const string overflowMax = "9223372036854775808";
        /// <summary>
        /// Int64.MinValue-1:-9223372036854775808-1=-9223372036854775809
        /// </summary>
        private const string overflowMin = "-9223372036854775809";
        /// <summary>
        /// Alpha:aaa
        /// </summary>
        private const string alpha = "aaa";
        /// <summary>
        /// Null:null
        /// </summary>
        private const string @null = (string)null;

        [TestMethod]
        public void IsInt64Test()
        {
            this.TestIsInt64(zero, true);
            this.TestIsInt64(minusZero, true);
            this.TestIsInt64(minus, true);
            this.TestIsInt64(overflowMax, false);
            this.TestIsInt64(overflowMin, false);
            this.TestIsInt64(alpha, false);
            this.TestIsInt64(@null, false);
        }

        [TestMethod]
        public void ToInt64Test()
        {
            this.TestToInt64(zero, 0);
            this.TestToInt64(minusZero, 0);
            this.TestToInt64(minus, -1);
            this.TestToInt64(overflowMax, 0, true);
            this.TestToInt64(overflowMin, 0, true);
            this.TestToInt64(alpha, 0, true);
            this.TestToInt64(@null, 0, true);
        }

        [TestMethod]
        public void TryToInt64DefaultZeroTest()
        {
            this.TestTryToInt64DefaultZero(zero, 0);
            this.TestTryToInt64DefaultZero(minusZero, 0);
            this.TestTryToInt64DefaultZero(minus, -1);
            this.TestTryToInt64DefaultZero(overflowMax, 0);
            this.TestTryToInt64DefaultZero(overflowMin, 0);
            this.TestTryToInt64DefaultZero(alpha, 0);
            this.TestTryToInt64DefaultZero(@null, 0);
        }

        [TestMethod]
        public void TryToInt64DefaultMinusTest()
        {
            this.TestTryToInt64DefaultMinus(zero, 0);
            this.TestTryToInt64DefaultMinus(minusZero, 0);
            this.TestTryToInt64DefaultMinus(minus, -1);
            this.TestTryToInt64DefaultMinus(overflowMax, -1);
            this.TestTryToInt64DefaultMinus(overflowMin, -1);
            this.TestTryToInt64DefaultMinus(alpha, -1);
            this.TestTryToInt64DefaultMinus(@null, -1);
        }

        private void TestIsInt64(string value, bool expected)
        {
            Assert.AreEqual(expected, StringExtend.IsInt64(value), "String.IsInt64 Test [{0}] Failed", value);
        }

        private void TestToInt64(string value, Int64 expected, bool isException = false)
        {
            try
            {
                if (isException)
                {
                    var actual = StringExtend.ToInt64(value);
                    Assert.Fail("String.ToInt64 Convert [{0}] Failed, the exception should be happened but not", value);
                }
                else
                {
                    Assert.AreEqual(expected, StringExtend.ToInt64(value), "String.ToInt64 Convert [{0}] Failed", value);
                }
            }
            catch
            {
                if (!isException)
                {
                    Assert.Fail("String.ToInt64 Convert [{0}] Failed, exception happened", value);
                }
            }
        }

        private void TestTryToInt64DefaultZero(string value, Int64 expected)
        {
            var actual = StringExtend.TryToInt64(value);
            Assert.AreEqual(expected, actual, "String.TryToInt64 Convert [{0}] Failed, actual is [{1}]", value, actual);
        }

        private void TestTryToInt64DefaultMinus(string value, Int64 expected)
        {
            var actual = StringExtend.TryToInt64(value, -1);
            Assert.AreEqual(expected, actual, "String.TryToInt64 Convert [{0}] Failed, actual is [{1}]", value, actual);
        }
    }
}
