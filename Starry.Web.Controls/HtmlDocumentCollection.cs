using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlDocumentCollection : IEnumerable<HtmlDocument>
    {
        public HtmlDocumentCollection() : this(false) { }
        public HtmlDocumentCollection(bool isReadOnly)
        {
            this.ReadOnly = isReadOnly;
            this.documents = new List<HtmlDocument>();
        }

        public bool ReadOnly { internal set; get; }

        public int Count { get { return this.documents.Count; } }

        public void Append(HtmlDocument document)
        {
            if (this.ReadOnly)
            {
                throw new HtmlDocumentCollectionReadOnlyException();
            }
            if (document == null)
            {
                throw new ArgumentNullException("Unable to append a Null-Refrence object into the collection");
            }
            this.documents.Add(document);
        }

        public bool Remove(HtmlDocument document)
        {
            if (this.ReadOnly)
            {
                throw new HtmlDocumentCollectionReadOnlyException();
            }
            if (document == null)
            {
                throw new ArgumentNullException("Unable to remove a Null-Refrence object from the collection");
            }
            return this.documents.Remove(document);
        }

        public void Clear()
        {
            if (this.ReadOnly)
            {
                throw new HtmlDocumentCollectionReadOnlyException();
            }
            this.documents.Clear();
        }

        private IList<HtmlDocument> documents;

        public IEnumerator<HtmlDocument> GetEnumerator()
        {
            return this.documents.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ToHtmlString()
        {
            if (this.documents != null && this.documents.Any())
            {
                return string.Join(System.Environment.NewLine, this.documents.Select(doc => doc.ToHtmlString()).ToArray());
            }
            return string.Empty;
        }
    }
}
