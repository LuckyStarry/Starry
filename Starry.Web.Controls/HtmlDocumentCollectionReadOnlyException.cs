using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public class HtmlDocumentCollectionReadOnlyException : Exception
    {
        public HtmlDocumentCollectionReadOnlyException() : base("Unable to insert or delete when the collection has been setted with Read-Only") { }
    }
}
