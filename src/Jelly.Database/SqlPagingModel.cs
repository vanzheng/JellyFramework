using System;

namespace Jelly.Database
{
    /// <summary>
    /// Represent a sql paging model.
    /// </summary>
    public class SqlPagingModel : SqlSelectModel
    {
        private int _pageindex = 1;
        private int _pagesize = 20;

        public SqlPagingModel() : base()
        {
        }

        public SqlPagingModel(int pageIndex, int pageSize) : base()
        {
            this._pageindex = pageIndex;
            this._pagesize = PageSize;
        }

        public int PageIndex
        {
            get
            {
                return this._pageindex;
            }
            set
            {
                if (value <= 0) 
                {
                    throw new Exception("The PageIndex cannot be less or equal to than 0.");
                }

                this._pageindex = value; 
            }
        }

        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
    }
}
