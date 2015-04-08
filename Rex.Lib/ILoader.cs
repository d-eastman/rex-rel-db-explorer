using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rex.Lib
{
    /// <summary>
    /// Interface defining a metadata loader intended to populate a hierarchy of Database-Schemas-TableViews-Columns.
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Name of loader
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Populate and return metadata hierarchy
        /// </summary>
        /// <returns>Root object of hierarchy</returns>
        Database Load();

        /// <summary>
        /// Whenever a data store is hit with a SQL Query, we want to be notified so we can see the
        /// internals.
        /// </summary>
        event EventHandler QueryExecuted;
    }
}
