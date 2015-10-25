using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    internal class HtmlElementAttributeCollection : Interface.IHtmlElementAttributeCollection
    {
        public HtmlElementAttributeCollection()
        {
            this.attributes = new List<Interface.IHtmlElementAttribute>();
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
                    this.attributes.Add(new HtmlElementAttribute { Name = name, Value = value });
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

        private IList<Interface.IHtmlElementAttribute> attributes;

        public IEnumerator<Interface.IHtmlElementAttribute> GetEnumerator()
        {
            return this.attributes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
