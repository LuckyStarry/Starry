using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Starry.Tests.Web.Controls.BuildTest
{
    [TestClass]
    public class BuildTest
    {
        private Starry.Web.Controls.HtmlRenderer htmlRender = new Starry.Web.Controls.HtmlRenderer();

        [TestMethod]
        public void BuildEmptyAnchor()
        {
            var control = new Starry.Web.Controls.HtmlAnchor();
            Assert.AreEqual("<a></a>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyBody()
        {
            var control = new Starry.Web.Controls.HtmlBody();
            Assert.AreEqual("<body></body>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyButton()
        {
            var control = new Starry.Web.Controls.HtmlButton();
            Assert.AreEqual("<button type=\"button\"></button>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyDiv()
        {
            var control = new Starry.Web.Controls.HtmlDiv();
            Assert.AreEqual("<div></div>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyDocument()
        {
            var control = new Starry.Web.Controls.HtmlDocument();
            Assert.AreEqual("<html></html>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyForm()
        {
            var control = new Starry.Web.Controls.HtmlForm();
            Assert.AreEqual("<form></form>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyHead()
        {
            var control = new Starry.Web.Controls.HtmlHead();
            Assert.AreEqual("<head></head>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyImg()
        {
            var control = new Starry.Web.Controls.HtmlImg();
            Assert.AreEqual("<img />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInput()
        {
            var control = new Starry.Web.Controls.HtmlInput();
            Assert.AreEqual("<input type=\"text\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInputButton()
        {
            var control = new Starry.Web.Controls.HtmlInputButton();
            Assert.AreEqual("<input type=\"button\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInputCheckBox()
        {
            var control = new Starry.Web.Controls.HtmlInputCheckBox();
            Assert.AreEqual("<input type=\"checkbox\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInputFile()
        {
            var control = new Starry.Web.Controls.HtmlInputFile();
            Assert.AreEqual("<input type=\"file\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInputHidden()
        {
            var control = new Starry.Web.Controls.HtmlInputHidden();
            Assert.AreEqual("<input type=\"hidden\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInputImage()
        {
            var control = new Starry.Web.Controls.HtmlInputImage();
            Assert.AreEqual("<input type=\"image\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInputPassword()
        {
            var control = new Starry.Web.Controls.HtmlInputPassword();
            Assert.AreEqual("<input type=\"password\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInputRadio()
        {
            var control = new Starry.Web.Controls.HtmlInputRadio();
            Assert.AreEqual("<input type=\"radio\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInputSumbit()
        {
            var control = new Starry.Web.Controls.HtmlInputSubmit();
            Assert.AreEqual("<input type=\"submit\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyInputText()
        {
            var control = new Starry.Web.Controls.HtmlInputText();
            Assert.AreEqual("<input type=\"text\" />", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyLabel()
        {
            var control = new Starry.Web.Controls.HtmlLabel();
            Assert.AreEqual("<label></label>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyLI()
        {
            var control = new Starry.Web.Controls.HtmlLI();
            Assert.AreEqual("<li></li>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyLink()
        {
            var control = new Starry.Web.Controls.HtmlLink();
            Assert.AreEqual("<link></link>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyMeta()
        {
            var control = new Starry.Web.Controls.HtmlMeta();
            Assert.AreEqual("<meta></meta>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyOption()
        {
            var control = new Starry.Web.Controls.HtmlOption();
            Assert.AreEqual("<option></option>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyScript()
        {
            var control = new Starry.Web.Controls.HtmlScript();
            Assert.AreEqual("<script></script>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptySelect()
        {
            var control = new Starry.Web.Controls.HtmlSelect();
            Assert.AreEqual("<select></select>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptySpan()
        {
            var control = new Starry.Web.Controls.HtmlSpan();
            Assert.AreEqual("<span></span>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTable()
        {
            var control = new Starry.Web.Controls.HtmlTable();
            Assert.AreEqual("<table></table>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTableCell()
        {
            var control = new Starry.Web.Controls.HtmlTableCell();
            Assert.AreEqual("<td></td>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTableRow()
        {
            var control = new Starry.Web.Controls.HtmlTableRow();
            Assert.AreEqual("<tr></tr>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTBody()
        {
            var control = new Starry.Web.Controls.HtmlTBody();
            Assert.AreEqual("<tbody></tbody>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTD()
        {
            var control = new Starry.Web.Controls.HtmlTD();
            Assert.AreEqual("<td></td>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTextArea()
        {
            var control = new Starry.Web.Controls.HtmlTextArea();
            Assert.AreEqual("<textarea></textarea>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTFoot()
        {
            var control = new Starry.Web.Controls.HtmlTFoot();
            Assert.AreEqual("<tfoot></tfoot>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTH()
        {
            var control = new Starry.Web.Controls.HtmlTH();
            Assert.AreEqual("<th></th>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTHead()
        {
            var control = new Starry.Web.Controls.HtmlTHead();
            Assert.AreEqual("<thead></thead>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTitle()
        {
            var control = new Starry.Web.Controls.HtmlTitle();
            Assert.AreEqual("<title></title>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyTR()
        {
            var control = new Starry.Web.Controls.HtmlTR();
            Assert.AreEqual("<tr></tr>", htmlRender.Render(control));
        }

        [TestMethod]
        public void BuildEmptyUL()
        {
            var control = new Starry.Web.Controls.HtmlUL();
            Assert.AreEqual("<ul></ul>", htmlRender.Render(control));
        }
    }
}
