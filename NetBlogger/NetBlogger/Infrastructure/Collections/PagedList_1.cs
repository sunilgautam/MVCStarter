using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetBlogger.Infrastructure.Collections
{
    public class PagedList_1<T> : IPagedList_1<T>
    {
        public int TotalPageCount
        {
            get { throw new NotImplementedException(); }
        }

        public int TotalItemCount
        {
            get { throw new NotImplementedException(); }
        }

        public int PageIndex
        {
            get { throw new NotImplementedException(); }
        }

        public int PageNumber
        {
            get { throw new NotImplementedException(); }
        }

        public int PageSize
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasPreviousPage
        {
            get { throw new NotImplementedException(); }
        }

        public bool HasNextPage
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsFirstPage
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsLastPage
        {
            get { throw new NotImplementedException(); }
        }

        public int StartPosition
        {
            get { throw new NotImplementedException(); }
        }

        public int EndPosition
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}