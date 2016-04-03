using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Tests.Extend
{
    [TestClass]
    public class StringConvertInt32Test
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
        /// Int32.MaxValue+1:2147483647+1=2147483648
        /// </summary>
        private const string overflowMax = "2147483648";
        /// <summary>
        /// Int32.MinValue-1:-2147483648-1=-2147483649
        /// </summary>
        private const string overflowMin = "-2147483649";
        /// <summary>
        /// Alpha:aaa
        /// </summary>
        private const string alpha = "aaa";
        /// <summary>
        /// Null:null
        /// </summary>
        private const string @null = (string)null;

        [TestMethod]
        public void IsInt32Test()
        {
            this.TestIsInt32(zero, true);
            this.TestIsInt32(minusZero, true);
            this.TestIsInt32(minus, true);
            this.TestIsInt32(overflowMax, false);
            this.TestIsInt32(overflowMin, false);
            this.TestIsInt32(alpha, false);
            this.TestIsInt32(@null, false);
        }

        [TestMethod]
        public void ToInt32Test()
        {
            this.TestToInt32(zero, 0);
            this.TestToInt32(minusZero, 0);
            this.TestToInt32(minus, -1);
            this.TestToInt32(overflowMax, 0, true);
            this.TestToInt32(overflowMin, 0, true);
            this.TestToInt32(alpha, 0, true);
            this.TestToInt32(@null, 0, true);
        }

        [TestMethod]
        public void TryToInt32DefaultZeroTest()
        {
            this.TestTryToInt32DefaultZero(zero, 0);
            this.TestTryToInt32DefaultZero(minusZero, 0);
            this.TestTryToInt32DefaultZero(minus, -1);
            this.TestTryToInt32DefaultZero(overflowMax, 0);
            this.TestTryToInt32DefaultZero(overflowMin, 0);
            this.TestTryToInt32DefaultZero(alpha, 0);
            this.TestTryToInt32DefaultZero(@null, 0);
        }

        [TestMethod]
        public void TryToInt32DefaultMinusTest()
        {
            this.TestTryToInt32DefaultMinus(zero, 0);
            this.TestTryToInt32DefaultMinus(minusZero, 0);
            this.TestTryToInt32DefaultMinus(minus, -1);
            this.TestTryToInt32DefaultMinus(overflowMax, -1);
            this.TestTryToInt32DefaultMinus(overflowMin, -1);
            this.TestTryToInt32DefaultMinus(alpha, -1);
            this.TestTryToInt32DefaultMinus(@null, -1);
        }

        private void TestIsInt32(string value, bool expected)
        {
            Assert.AreEqual(expected, StringExtend.IsInt32(value), "String.IsInt32 Test [{0}] Failed", value);
        }

        private void TestToInt32(string value, Int32 expected, bool isException = false)
        {
            try
            {
                if (isException)
                {
                    var actual = StringExtend.ToInt32(value);
                    Assert.Fail("String.ToInt32 Convert [{0}] Failed, the exception should be happened but not", value);
                }
                else
                {
                    Assert.AreEqual(expected, StringExtend.ToInt32(value), "String.ToInt32 Convert [{0}] Failed", value);
                }
            }
            catch
            {
                if (!isException)
                {
                    Assert.Fail("String.ToInt32 Convert [{0}] Failed, exception happened", value);
                }
            }
        }

        private void TestTryToInt32DefaultZero(string value, Int32 expected)
        {
            var actual = StringExtend.TryToInt32(value);
            Assert.AreEqual(expected, actual, "String.TryToInt32 Convert [{0}] Failed, actual is [{1}]", value, actual);
        }

        private void TestTryToInt32DefaultMinus(string value, Int32 expected)
        {
            var actual = StringExtend.TryToInt32(value, -1);
            Assert.AreEqual(expected, actual, "String.TryToInt32 Convert [{0}] Failed, actual is [{1}]", value, actual);
        }
    }
}
