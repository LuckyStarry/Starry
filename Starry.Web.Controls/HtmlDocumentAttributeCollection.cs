using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlDocumentAttributeCollection : IEnumerable<HtmlDocumentAttribute>
    {
        public HtmlDocumentAttributeCollection()
        {
            this.attributes = new List<HtmlDocumentAttribute>();
        }

        public int Count { get { return this.attributes.Count; } }

        public void Clear()
        {
            this.attributes.Clear();
        }

        public string this[string name]
        {
            get
            {
                name = (name ?? string.Empty).Trim().ToLower();
                var attribute = this.attributes.FirstOrDefault(attr => attr.Name == name);
                return attribute == null ? string.Empty : attribute.Value ?? string.Empty;
            }
            set
            {
                name = (name ?? string.Empty).Trim().ToLower();
                var attribute = this.attributes.FirstOrDefault(attr => attr.Name == name);
                if (attribute == null)
                {
                    this.attributes.Add(new HtmlDocumentAttribute { Name = name, Value = value });
                }
                else
                {
                    attribute.Value = value;
                }
            }
        }

        public bool Remove(string name)
        {
            name = (name ?? string.Empty).Trim().ToLower();
            var attribute = this.attributes.FirstOrDefault(attr => attr.Name == name);
            if (attribute != null)
            {
                return this.attributes.Remove(attribute);
            }
            return false;
        }

        private IList<HtmlDocumentAttribute> attributes;

        public IEnumerator<HtmlDocumentAttribute> GetEnumerator()
        {
            return this.attributes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ToHtmlString()
        {
            if (this.attributes != null && this.attributes.Any())
            {
                var attrHtml = new List<string>();
                foreach (var attr in this.attributes)
                {
                    attrHtml.Add(string.Format("{0}=\"{1}\"", attr.Name, attr.Value));
                }
                return string.Join(" ", attrHtml.ToArray());
            }
            return string.Empty;
        }
    }
}
