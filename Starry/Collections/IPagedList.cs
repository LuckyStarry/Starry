using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Collections
{
    public interface IPagedList : IEnumerable
    {
        /// <summary>
        /// Total items' count in the list
        /// </summary>
        int TotalItemCount { get; }
        /// <summary>
        /// How many items in a page
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// One-based page index
        /// </summary>
        int PageIndex { get; }
    }

    public interface IPagedList<T> : IPagedList, IEnumerable<T>
    {
    }
}
