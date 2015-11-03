using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Collections
{
    public class PagedList<T> : IPagedList<T>
    {
        internal PagedList(IEnumerable<T> list, int pageIndex, int pageSize, int totalItemCount)
        {
            this.innerList = list;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalItemCount = totalItemCount;
        }

        private IEnumerable<T> innerList;

        public int TotalItemCount { private set; get; }
        public int PageIndex { private set; get; }
        public int PageSize { private set; get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.innerList != null)
            {
                return this.innerList.GetEnumerator();
            }
            return null;
        }
    }
}
