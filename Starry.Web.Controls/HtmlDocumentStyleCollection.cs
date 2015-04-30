using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlDocumentStyleCollection : IEnumerable<HtmlDocumentStyle>
    {
        public HtmlDocumentStyleCollection()
        {
            this.styles = new List<HtmlDocumentStyle>();
        }

        public int Count { get { return this.styles.Count; } }

        public void Clear()
        {
            this.styles.Clear();
        }

        public string this[string name]
        {
            get
            {
                name = (name ?? string.Empty).Trim().ToLower();
                var style = this.styles.FirstOrDefault(css => css.Name == name);
                return style == null ? string.Empty : style.Value ?? string.Empty;
            }
            set
            {
                name = (name ?? string.Empty).Trim().ToLower();
                var style = this.styles.FirstOrDefault(css => css.Name == name);
                if (style == null)
                {
                    this.styles.Add(new HtmlDocumentStyle { Name = name, Value = value });
                }
                else
                {
                    style.Value = value;
                }
            }
        }

        public bool Remove(string name)
        {
            name = (name ?? string.Empty).Trim().ToLower();
            var style = this.styles.FirstOrDefault(css => css.Name == name);
            if (style != null)
            {
                return this.styles.Remove(style);
            }
            return false;
        }

        private IList<HtmlDocumentStyle> styles;

        public IEnumerator<HtmlDocumentStyle> GetEnumerator()
        {
            return this.styles.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ToHtmlString()
        {
            if (this.styles != null && this.styles.Any())
            {
                var html = new StringBuilder("style=\"");
                var stylesString = new List<string>();
                foreach (var style in this.styles)
                {
                    stylesString.Add(string.Format("{0}: {1}", style.Name, style.Value));
                }
                html.Append(string.Join("; ", stylesString.ToArray()));
                html.Append("\"");
                return html.ToString();
            }
            return string.Empty;
        }
    }
}
