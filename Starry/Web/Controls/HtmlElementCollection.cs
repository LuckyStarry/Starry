using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    internal class HtmlElementCollection : Interface.IHtmlElementCollection
    {
        public HtmlElementCollection(Interface.IHtmlContainerControl container) : this(container, false) { }
        public HtmlElementCollection(Interface.IHtmlContainerControl container, bool isReadOnly)
        {
            if (container == null)
            {
                throw new ArgumentNullException("htmlElement", "Must set a container for the collection");
            }
            this.Container = container;
            this.ReadOnly = isReadOnly;
            this.elements = new List<Interface.IHtmlElement>();
        }

        public Interface.IHtmlContainerControl Container { private set; get; }

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
                throw new ArgumentNullException("element", "Unable to append a Null-Refrence object into the collection");
            }
            if (!this.CheckLoop(this.Container, element))
            {
                throw new ArgumentException("Cannot append a parent element", "element");
            }
            element.Parent = this.Container;
            this.elements.Add(element);
        }

        private bool CheckLoop(Interface.IHtmlContainerControl container, Interface.IHtmlElement element)
        {
            if (container == null)
            {
                return true;
            }
            if (object.ReferenceEquals(container, element))
            {
                return false;
            }
            if (container.Parent == null)
            {
                return true;
            }
            if (container.Parent is Interface.IHtmlContainerControl)
            {
                return this.CheckLoop(container.Parent as Interface.IHtmlContainerControl, element);
            }
            return true;
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
            document.Parent = null;
            return this.elements.Remove(document);
        }

        public void Clear()
        {
            if (this.ReadOnly)
            {
                throw new HtmlElementCollectionReadOnlyException();
            }
            foreach (var ele in this.elements)
            {
                ele.Parent = null;
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
