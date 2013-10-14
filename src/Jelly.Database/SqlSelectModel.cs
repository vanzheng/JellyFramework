using System.Text;

namespace Jelly.Database
{
    public class SqlSelectModel
    {
        private string _selectClause = "*";

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlSelectModel"/> class.
        /// </summary>
        public SqlSelectModel()
        {
        }

        public string PrimaryKey
        {
            get;
            set;
        }

        public string SelectClause
        {
            get { return this._selectClause; }
            set { this._selectClause = value; }
        }

        public string FromClause
        {
            get;
            set;
        }

        public string WhereClause
        {
            get;
            set;
        }

        public string GroupByClause
        {
            get;
            set;
        }

        public string HavingClause
        {
            get;
            set;
        }

        public string OrderByClause
        {
            get;
            set;
        }

        public virtual string ToSql() 
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT {0} FROM {1}", SelectClause, FromClause);

            if (!string.IsNullOrWhiteSpace(WhereClause))
            {
                builder.AppendFormat(" WHERE {0}", WhereClause);
            }

            if (!string.IsNullOrWhiteSpace(GroupByClause)) 
            {
                builder.AppendFormat(" GROUP BY {0}", GroupByClause);
            }

            if (!string.IsNullOrWhiteSpace(HavingClause)) 
            {
                builder.AppendFormat(" HAVING {0}", HavingClause);
            }

            if (!string.IsNullOrWhiteSpace(OrderByClause))
            {
                builder.AppendFormat(" ORDER BY {0}", OrderByClause);
            }

            return builder.ToString();
        }
    }
}
