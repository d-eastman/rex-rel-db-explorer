using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rex.Lib
{
    /// <summary>
    /// Custom EventArgs that carries information about the execution of a query
    /// </summary>
    public class QueryExecutedEventArgs : EventArgs
    {
        /// <summary>
        /// SQL code that was executed against the database
        /// </summary>
        public string Sql { get; private set; }

        public QueryExecutedEventArgs(string sql)
        {
            Sql = sql;
        }
    }
}
