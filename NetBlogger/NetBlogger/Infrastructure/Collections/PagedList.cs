using System;
using System.Collections.Generic;

namespace NetBlogger.Infrastructure.Collections
{
    public class PagedList<T> : IPagedList<T>
    {
        int _ItemsPerPage;
        int _ItemsTotal;
        int _CurrentPage;
        int _NumPages;
        int _MidRange;
        int _Low;
        int _High;
        int _DefaultIpp = 25;
        IEnumerable<T> _Items;

        public PagedList(IEnumerable<T> _Items, int _ItemsPerPage, int _CurrentPage)
        {
            this._MidRange = 7;
            //SetUp(_Items, _ItemsPerPage, _CurrentPage, this._MidRange);
        }

        public PagedList(IEnumerable<T> _Items, int _ItemsPerPage, int _CurrentPage, int _MidRange)
        {
            //SetUp(_Items, _ItemsPerPage, _CurrentPage, _MidRange);
        }

        private void SetUp(IEnumerable<T> _Items, int _ItemsPerPage, int _ItemsTotal, int _CurrentPage, int _MidRange)
        {
            this._Items = _Items;
            this._ItemsPerPage = _ItemsPerPage;
            this._ItemsTotal = _ItemsTotal;
            this._CurrentPage = _CurrentPage;
            this._NumPages = (int)Math.Ceiling((double)this._ItemsTotal / this._ItemsPerPage);
            this._MidRange = _MidRange;
            // SETTING LOWERBOUND OF PAGINATION
            this._Low = (this._CurrentPage - 1) * this._ItemsPerPage;
            // SETTING UPPERBOUND OF PAGINATION
            this._High = (this._CurrentPage * this._ItemsPerPage) - 1;
        }

        public int ItemsPerPage
        {
            get
            {
                return this._ItemsPerPage;
            }
            set
            {
                this._ItemsPerPage = value;
            }
        }

        public int ItemsTotal
        {
            get { return this._ItemsTotal; }
        }

        public int CurrentPage
        {
            get { return this._CurrentPage; }
        }

        public int NumPages
        {
            get { return this._NumPages; }
        }

        public int MidRange
        {
            get
            {
                return this._MidRange;
            }
            set
            {
                this._MidRange = value;
            }
        }

        public int Low
        {
            get
            {
                return this._Low;
            }
        }

        public int High
        {
            get
            {
                return this._High;
            }
        }

        public int DefaultIpp
        {
            get { return this._DefaultIpp; }
        }

        public IEnumerable<T> Items
        {
            get
            {
                return this._Items;
            }
        }
    }
}