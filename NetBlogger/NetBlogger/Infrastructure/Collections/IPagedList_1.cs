using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetBlogger.Infrastructure.Collections
{
    public interface IPagedList_1<T> : IEnumerable<T>
    {
        int TotalPageCount { get; }
        int TotalItemCount { get; }

        int PageIndex { get; }
        int PageNumber { get; }
        int PageSize { get; }

        bool HasPreviousPage { get; }
        bool HasNextPage { get; }

        bool IsFirstPage { get; }
        bool IsLastPage { get; }

        int StartPosition { get; }
        int EndPosition { get; }
    }
}
