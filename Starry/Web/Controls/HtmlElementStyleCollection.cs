using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    internal class HtmlElementStyleCollection : Interface.IHtmlElementStyleCollection
    {
        public HtmlElementStyleCollection()
        {
            this.styles = new List<Interface.IHtmlElementStyle>();
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
                    this.styles.Add(new HtmlElementStyle { Name = name, Value = value });
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

        private IList<Interface.IHtmlElementStyle> styles;

        public IEnumerator<Interface.IHtmlElementStyle> GetEnumerator()
        {
            return this.styles.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
