using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Jelly.Web.Helpers;
using Jelly.Helpers;

namespace Jelly.Web.Paging
{
    /// <summary>
    /// The Pagination generator.
    /// </summary>
    public class Pagination
    {
        // Pager Templates
        private const string PaginationTemplate = "<div class=\"{0}\">{1}</div>";
        private const string PageNumberTemplate = "<a href=\"{0}\" class=\"page-num\"><{1}</a>";
        private const string CurrentPageNumberTemplate = "<span class=\"{0}\">{1}</span>";
        private const string PageNavigationTemplate = "<span class=\"page-nav\">{0}</span>";
        private const string PageBarTemplate = "<div class=\"page-bar\">{0}</div>";
        private const string PageRecordTemplate = "<div class=\"page-record\">{0}</div>";
        private const string PageJumpWrapperTemplate = "<div class=\"page-jump-wrapper\">{0}</div>";
        private const string PageJumpTexBoxTemplate = "<input type=\"text\" class=\"page-jump\" />";
        private const string PageJumpConfirmButtonTemplate = "<input type=\"button\" class=\"page-jump-confirm\" value=\"{0}\" />";
        private const string PageDropDownListTemplate = "<select class=\"page-jump-list\">{0}</select>";

        // Pagebar properties 
        private int _pageSize;
        private int _pageIndex;
        private int _recordCount;
        private int _pageCount;
        private bool _showFirstLastPage = true;
        private bool _showPrevNextPage = true;
        private bool _showPageCount = true;
        private bool _showPageJumpTextBox = false;
        private bool _showPageDropDownList = false;
        private string _firstPageName = "首  页";
        private string _prevPageName = "上一页";
        private string _nextPageName = "下一页";
        private string _lastPageName = "尾  页";
        private string _pageTotalCountTemplate = "共有{0}页";
        private string _confirmButtonText = "确定";
        private int _showPageBarNumber = 10;

        // Style
        /// <summary>
        /// The current page class name.
        /// </summary>
        private string _currentPageClassName = "current";
        private string _pagerClassName = "pagination";

        // Page url parameters
        /// <summary>
        /// The page query string parameter name.
        /// </summary>
        private string _pageParamName = "page"; 
        private string _queryUrl;
        private string _urlRewritePattern = null;

        public Pagination(int pageSize,int recordCount) 
            : this(pageSize, recordCount, "page", 10, true) 
        {
        }
        

        public Pagination(int pageSize, int recordCount, int showPageBarNumber, bool showFirstLastPage) 
            : this(pageSize, recordCount, "page", showPageBarNumber, showFirstLastPage)
        {
        }

        
        public Pagination(int pageSize, int recordCount, string pageParamName, int showPageBarNumber, bool showFirstLastPage) 
        {
            this._pageSize = pageSize;
            this._recordCount = recordCount;
            this._pageCount = GetPageCount(recordCount, pageSize);
            this._pageParamName = pageParamName;
            this._showPageBarNumber = showPageBarNumber;
            this._pageIndex = SiteUtils.GetPageIndex(pageParamName);
            if (this._pageIndex > this._pageCount) 
            { 
                this._pageIndex = this._pageCount; 
            }

            this._queryUrl = UriUtils.AppendQueryDelimiter(SiteUtils.BuildQueryUrl(pageParamName));
            this._showFirstLastPage = showFirstLastPage;
        }

        public int PageSize 
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int PageIndex 
        {
            get { return _pageIndex; }
        }

        public int RecordCount 
        {
            get { return _recordCount; }
            set { _recordCount = value; }
        }

        public int PageCount 
        {
            get { return _pageCount; }
        }

        public string QueryUrl
        {
            get { return _queryUrl; }
            set { _queryUrl = value; }
        }

        public int ShowPageBarNumber 
        {
            get { return _showPageBarNumber; }
            set { _showPageBarNumber = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PageParamName 
        {
            get { return _pageParamName; }
            set { _pageParamName = value; }
        }

        /// <summary>
        /// The current page class name.
        /// </summary>
        public string CurrentPageClassName 
        {
            get { return _currentPageClassName; }
            set { _currentPageClassName = value; }
        }

        public string FirstPageName 
        {
            get { return _firstPageName; }
            set { _firstPageName = value; }
        }

        /// <summary>
        /// 上一页名称
        /// </summary>
        public string PrevPageName 
        {
            get { return _prevPageName; }
            set { _prevPageName = value; }
        }

        /// <summary>
        /// 下一页名称
        /// </summary>
        public string NextPageName 
        {
            get { return _nextPageName; }
            set { _nextPageName = value; }
        }

        /// <summary>
        /// 末页名称
        /// </summary>
        public string LastPageName 
        {
            get { return _lastPageName; }
            set { _lastPageName = value; }
        }

        public string PagerClassName 
        {
            get { return _pagerClassName; }
            set { _pagerClassName = value; }
        }

        public bool ShowFirstLastPage 
        {
            get { return _showFirstLastPage; }
            set { _showFirstLastPage = value; }
        }

        public bool ShowPrevNextPage 
        {
            get { return _showPrevNextPage; }
            set { _showPrevNextPage = value; }
        }

        public bool ShowPageCount 
        {
            get { return _showPageCount; }
            set { _showPageCount = value; }
        }

        public string PageTotalCountTemplate 
        {
            get { return _pageTotalCountTemplate; }
            set { _pageTotalCountTemplate = value; }
        }

        public bool ShowPageJumpTextBox 
        {
            get { return _showPageJumpTextBox; }
            set { _showPageJumpTextBox = value; }
        }

        public bool ShowPageDropDownList 
        {
            get { return _showPageDropDownList; }
            set { _showPageDropDownList = value; }
        }

        public string ConfirmButtonText 
        {
            get { return _confirmButtonText; }
            set { _confirmButtonText = value; }
        }

        /// <summary>
        /// Url重写规则
        /// </summary>
        public string UrlRewritePattern 
        {
            set { _urlRewritePattern = value; }
        }

        /// <summary>
        /// The first page html.
        /// </summary>
        protected virtual string FirstPageHtml 
        {
            get 
            {
                if (this._pageIndex == 1)
                {
                    return string.Format(PageNavigationTemplate, this._firstPageName);
                }
                else
                {
                    string html = string.Empty;

                    if (this._urlRewritePattern == null)
                    {
                        html = string.Format(PageNumberTemplate, string.Concat(this._queryUrl, this._pageParamName, "=1"), _firstPageName);
                    }
                    else
                    {
                        //格式化this._urlrewritepattern,{0}放入pageIndex
                        html = string.Format(PageNumberTemplate, string.Format(_urlRewritePattern, 1), _firstPageName);
                    }
                    return html;
                }
            }
        }

        /// <summary>
        /// The next page html.
        /// </summary>
        protected virtual string NextPageHtml
        {
            get 
            {
                if (this._pageIndex == this._pageCount)
                {
                    return string.Format(PageNavigationTemplate, this._nextPageName);
                }
                else
                {
                    string html = string.Empty;

                    if (this._urlRewritePattern == null)
                    {
                        html = string.Format(
                            PageNumberTemplate, 
                            string.Concat(this._queryUrl, this._pageParamName, "=", this._pageIndex + 1), 
                            this._nextPageName); 
                    }
                    else
                    {
                        html = string.Format(PageNumberTemplate, string.Format(this._urlRewritePattern, this._pageIndex + 1), this._nextPageName);
                    }
                    return html;
                }
            }
        }

        /// <summary>
        /// The previous page html
        /// </summary>
        protected virtual string PrevPageHtml
        {
            get
            {
                if (this._pageIndex == 1)
                {
                    return string.Format(PageNavigationTemplate, this._prevPageName);
                }
                else
                {
                    string html = string.Empty;

                    if (this._urlRewritePattern == null)
                    {
                        html = string.Format(
                            PageNumberTemplate,
                            string.Concat(this._queryUrl, this._pageParamName, "=", this._pageIndex - 1),
                            this._prevPageName);
                    }
                    else
                    {
                        html = string.Format(PageNumberTemplate, string.Format(this._urlRewritePattern, this._pageIndex - 1), this._prevPageName);
                    }
                    return html;
                }
            }
        }

        /// <summary>
        /// The last page html.
        /// </summary>
        protected virtual string LastPageHtml
        {
            get 
            {
                if (this._pageIndex == this._pageCount)
                {
                    return string.Format(PageNavigationTemplate, this._lastPageName);
                }
                else
                {
                    string html = string.Empty;

                    if (this._urlRewritePattern == null)
                    {
                        html = string.Format(PageNumberTemplate, string.Concat(this._queryUrl, this._pageParamName, "=", this._pageCount), this._lastPageName);
                    }
                    else
                    {
                        html = string.Format(PageNumberTemplate, string.Format(this._urlRewritePattern, this._pageCount), this._lastPageName);
                    }
                    return html;
                }
            }
        }

        /// <summary>
        /// Gets the page numbers html.
        /// </summary>
        protected virtual string PageNumbersHtml 
        {
            get
            {
                if (this._recordCount == 0 || this._showPageBarNumber <= 0)
                {
                    return string.Empty;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    int startPage, endPage;
                    string url;

                    if (this._showPageBarNumber > this._pageCount) 
                    { 
                        this._showPageBarNumber = this._pageCount; 
                    }

                    //根据当前页来获取PageNumberic,把当前页放在PageNumberic的中间
                    if (this._showPageBarNumber % 2 == 0)
                    {
                        startPage = this._pageIndex - (this._showPageBarNumber / 2 + 1);
                    }
                    else
                    {
                        startPage = this._pageIndex - (int)(this._showPageBarNumber / 2);
                    }

                    if (startPage < 1) 
                    { 
                        startPage = 1; 
                    }

                    endPage = startPage + this._showPageBarNumber - 1;

                    if (endPage >= this._pageCount)
                    {
                        endPage = this._pageCount;

                    }
                    if (endPage - startPage + 1 != this._showPageBarNumber)
                    {
                        startPage = (this._pageCount - this._showPageBarNumber + 1 < 1) ? 1 : this._pageCount - this._showPageBarNumber + 1;
                    }

                    for (int i = startPage; i <= endPage; i++)
                    {
                        if (this._urlRewritePattern == null)
                        {
                            url = string.Concat(this._queryUrl, this._pageParamName, "=", i);
                        }
                        else
                        {
                            url = string.Format(this._urlRewritePattern, i);
                        }

                        if (i == this._pageIndex)
                        {
                            sb.AppendFormat(CurrentPageNumberTemplate, this._currentPageClassName, i);
                        }
                        else
                        {
                            sb.AppendFormat(PageNumberTemplate, url, i);
                        }
                    }

                    return sb.ToString();
                }
            }
        }

        protected virtual string PagerBarHtml 
        {
            get
            {
                string html = string.Empty;

                if (_showPrevNextPage) 
                {
                    html = string.Concat(PrevPageHtml, PageNumbersHtml, NextPageHtml);
                }

                if (_showFirstLastPage) 
                {
                    html += string.Concat(FirstPageHtml, html, LastPageHtml);
                }

                return string.Format(PageBarTemplate, html);
            }
        }

        protected virtual string PageRecordHtml 
        {
            get 
            {
                if (_showPageCount)
                {
                    string html = string.Format(_pageTotalCountTemplate, PageCount);
                    return string.Format(PageRecordTemplate, html);
                }

                return string.Empty;
            }
        }

        protected virtual string PageJumpHtml 
        {
            get 
            {
                string html = string.Empty;

                if (_showPageJumpTextBox) 
                {
                    html = string.Format(PageJumpWrapperTemplate, 
                        string.Concat(PageJumpTexBoxTemplate, string.Format(PageJumpConfirmButtonTemplate, _confirmButtonText)));   
                }

                if (_showPageDropDownList) 
                {
                    StringBuilder builder = new StringBuilder();

                    for (int i = 1; i <= PageCount; i++) 
                    {
                        if (i == PageIndex)
                        {
                            builder.AppendFormat("<option value=\"{0}\" selected=\"selected\">{0}</option>", i);
                        }
                        else 
                        {
                            builder.AppendFormat("<option value=\"{0}\">{0}</option>", i);
                        }
                    }

                    html = string.Format(PageDropDownListTemplate, builder.ToString());
                    html = string.Format(PageJumpWrapperTemplate, html);
                }

                return html;
            }
        }

        public virtual string Render()
        {
            return string.Format(PageNavigationTemplate, _pagerClassName,
                string.Concat(PagerBarHtml, PageRecordHtml, PageJumpHtml));

        }

        public int GetPageCount(int recordCount, int pageSize)
        {
            if (recordCount == 0)
            {
                return 1;
            }
            else
            {
                return (recordCount % pageSize == 0) ? (recordCount / pageSize) : (int)(recordCount / pageSize) + 1;
            }
        }
    }
}
