using System;
using System.Collections.Generic;

namespace NetBlogger.Infrastructure.Collections
{
    public interface IPagedList<T>
    {
        int ItemsPerPage { get; set; }
        int ItemsTotal { get; }
        int CurrentPage { get; }
        int NumPages { get; }
        int MidRange { get; set; }
        int Low { get; }
        int High { get; }
        int DefaultIpp { get; }
        IEnumerable<T> Items { get; }
    }
}