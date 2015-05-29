using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    internal class HtmlElementCollection : Interface.IHtmlElementCollection
    {
        public HtmlElementCollection() : this(false) { }
        public HtmlElementCollection(bool isReadOnly)
        {
            this.ReadOnly = isReadOnly;
            this.elements = new List<Interface.IHtmlElement>();
        }

        public bool ReadOnly { internal set; get; }

        public int Count { get { return this.elements.Count; } }

        public void Add(Interface.IHtmlElement element)
        {
            if (this.ReadOnly)
            {
                throw new HtmlElementCollectionReadOnlyException();
            }
            if (element == null)
            {
                throw new ArgumentNullException("Unable to append a Null-Refrence object into the collection");
            }
            this.elements.Add(element);
        }

        public bool Remove(Interface.IHtmlElement document)
        {
            if (this.ReadOnly)
            {
                throw new HtmlElementCollectionReadOnlyException();
            }
            if (document == null)
            {
                throw new ArgumentNullException("Unable to remove a Null-Refrence object from the collection");
            }
            return this.elements.Remove(document);
        }

        public void Clear()
        {
            if (this.ReadOnly)
            {
                throw new HtmlElementCollectionReadOnlyException();
            }
            this.elements.Clear();
        }

        private IList<Interface.IHtmlElement> elements;

        public IEnumerator<Interface.IHtmlElement> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ToHtmlString()
        {
            if (this.elements != null && this.elements.Any())
            {
                return string.Join(System.Environment.NewLine, this.elements.Select(ele => ele.ToHtmlString()).ToArray());
            }
            return string.Empty;
        }
    }
}
