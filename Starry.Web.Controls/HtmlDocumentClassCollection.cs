using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlDocumentClassCollection : IEnumerable<string>
    {
        public HtmlDocumentClassCollection()
        {
            this.classes = new List<string>();
        }

        public int Count { get { return this.classes.Count; } }

        public void Clear()
        {
            this.classes.Clear();
        }

        public void Add(string @class)
        {
            var classes = (@class ?? string.Empty).Trim().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            foreach (var cls in classes)
            {
                if (!this.classes.Contains(cls))
                {
                    this.classes.Add(cls);
                }
            }
        }

        public bool Remove(string @class)
        {
            var classes = (@class ?? string.Empty).Trim().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var beforeRemove = this.Count;
            this.classes = this.classes.Where(cls => !classes.Contains(cls)).ToList();
            return this.Count < beforeRemove;
        }

        private IList<string> classes;

        public IEnumerator<string> GetEnumerator()
        {
            return this.classes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ToHtmlString()
        {
            if (this.classes != null && this.classes.Any())
            {
                var html = new StringBuilder("class=\"");
                html.Append(string.Join(" ", this.classes.ToArray()));
                html.Append("\"");
                return html.ToString();
            }
            return string.Empty;
        }
    }
}
