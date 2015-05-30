using System;
using Starry.Web.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Starry.Tests.Web.Controls
{
    [TestClass]
    public class LoopTest
    {
        [TestMethod]
        public void ContainerLoopTest()
        {
            var pass = false;
            var div = new HtmlDiv();
            try
            {
                div.Append(div);
            }
            catch
            {
                pass = true;
            }
            Assert.IsTrue(pass, "Self Loop Test FAILED");

            pass = false;
            var parent = div.Wrap(new HtmlDiv());
            try
            {
                div.Append(parent);
            }
            catch
            {
                pass = true;
            }
            Assert.IsTrue(pass, "Parent Loop Test FAILED");

            pass = false;
            var ancestor = parent.Wrap(new HtmlDiv());
            try
            {
                div.Append(ancestor);
            }
            catch
            {
                pass = true;
            }
            Assert.IsTrue(pass, "Ancestor Loop Test FAILED");

            ancestor.Children.Clear();
            try
            {
                div.Append(ancestor);
            }
            catch
            {
                Assert.Fail("Ancestor Loop Test FAILED");
            }
        }
    }
}
