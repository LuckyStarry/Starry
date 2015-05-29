using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Starry.Test.Web.Controls.BuildTest
{
    [TestClass]
    public class BuildTest
    {
        [TestMethod]
        public void BuildEmptyAnchor()
        {
            var control = new Starry.Web.Controls.HtmlAnchor();
            Assert.AreEqual("<a></a>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyBody()
        {
            var control = new Starry.Web.Controls.HtmlBody();
            Assert.AreEqual("<body></body>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyButton()
        {
            var control = new Starry.Web.Controls.HtmlButton();
            Assert.AreEqual("<button type=\"button\"></button>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyDiv()
        {
            var control = new Starry.Web.Controls.HtmlDiv();
            Assert.AreEqual("<div></div>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyDocument()
        {
            var control = new Starry.Web.Controls.HtmlDocument();
            Assert.AreEqual("<html></html>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyForm()
        {
            var control = new Starry.Web.Controls.HtmlForm();
            Assert.AreEqual("<form></form>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyHead()
        {
            var control = new Starry.Web.Controls.HtmlHead();
            Assert.AreEqual("<head></head>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyImg()
        {
            var control = new Starry.Web.Controls.HtmlImg();
            Assert.AreEqual("<img />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInput()
        {
            var control = new Starry.Web.Controls.HtmlInput();
            Assert.AreEqual("<input type=\"text\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInputButton()
        {
            var control = new Starry.Web.Controls.HtmlInputButton();
            Assert.AreEqual("<input type=\"button\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInputCheckBox()
        {
            var control = new Starry.Web.Controls.HtmlInputCheckBox();
            Assert.AreEqual("<input type=\"checkbox\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInputFile()
        {
            var control = new Starry.Web.Controls.HtmlInputFile();
            Assert.AreEqual("<input type=\"file\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInputHidden()
        {
            var control = new Starry.Web.Controls.HtmlInputHidden();
            Assert.AreEqual("<input type=\"hidden\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInputImage()
        {
            var control = new Starry.Web.Controls.HtmlInputImage();
            Assert.AreEqual("<input type=\"image\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInputPassword()
        {
            var control = new Starry.Web.Controls.HtmlInputPassword();
            Assert.AreEqual("<input type=\"password\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInputRadio()
        {
            var control = new Starry.Web.Controls.HtmlInputRadio();
            Assert.AreEqual("<input type=\"radio\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInputSumbit()
        {
            var control = new Starry.Web.Controls.HtmlInputSubmit();
            Assert.AreEqual("<input type=\"submit\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyInputText()
        {
            var control = new Starry.Web.Controls.HtmlInputText();
            Assert.AreEqual("<input type=\"text\" />", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyLabel()
        {
            var control = new Starry.Web.Controls.HtmlLabel();
            Assert.AreEqual("<label></label>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyLI()
        {
            var control = new Starry.Web.Controls.HtmlLI();
            Assert.AreEqual("<li></li>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyLink()
        {
            var control = new Starry.Web.Controls.HtmlLink();
            Assert.AreEqual("<link></link>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyMeta()
        {
            var control = new Starry.Web.Controls.HtmlMeta();
            Assert.AreEqual("<meta></meta>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyOption()
        {
            var control = new Starry.Web.Controls.HtmlOption();
            Assert.AreEqual("<option></option>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyScript()
        {
            var control = new Starry.Web.Controls.HtmlScript();
            Assert.AreEqual("<script></script>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptySelect()
        {
            var control = new Starry.Web.Controls.HtmlSelect();
            Assert.AreEqual("<select></select>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptySpan()
        {
            var control = new Starry.Web.Controls.HtmlSpan();
            Assert.AreEqual("<span></span>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTable()
        {
            var control = new Starry.Web.Controls.HtmlTable();
            Assert.AreEqual("<table></table>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTableCell()
        {
            var control = new Starry.Web.Controls.HtmlTableCell();
            Assert.AreEqual("<td></td>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTableRow()
        {
            var control = new Starry.Web.Controls.HtmlTableRow();
            Assert.AreEqual("<tr></tr>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTBody()
        {
            var control = new Starry.Web.Controls.HtmlTBody();
            Assert.AreEqual("<tbody></tbody>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTD()
        {
            var control = new Starry.Web.Controls.HtmlTD();
            Assert.AreEqual("<td></td>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTextArea()
        {
            var control = new Starry.Web.Controls.HtmlTextArea();
            Assert.AreEqual("<textarea></textarea>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTFoot()
        {
            var control = new Starry.Web.Controls.HtmlTFoot();
            Assert.AreEqual("<tfoot></tfoot>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTH()
        {
            var control = new Starry.Web.Controls.HtmlTH();
            Assert.AreEqual("<th></th>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTHead()
        {
            var control = new Starry.Web.Controls.HtmlTHead();
            Assert.AreEqual("<thead></thead>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTitle()
        {
            var control = new Starry.Web.Controls.HtmlTitle();
            Assert.AreEqual("<title></title>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyTR()
        {
            var control = new Starry.Web.Controls.HtmlTR();
            Assert.AreEqual("<tr></tr>", control.ToHtmlString());
        }

        [TestMethod]
        public void BuildEmptyUL()
        {
            var control = new Starry.Web.Controls.HtmlUL();
            Assert.AreEqual("<ul></ul>", control.ToHtmlString());
        }
    }
}
