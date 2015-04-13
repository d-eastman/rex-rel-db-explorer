using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rex.Lib;

namespace Rex.WebForms.ViewModel.Test
{
    /// <summary>
    /// Trivial implementation of ILoader to return a null Database from the Load method
    /// Simulates a loader that fails to instantiate metadata properly
    /// </summary>
    internal sealed class NullDatabaseTestLoader : ILoader
    {
        public string Name
        {
            get { throw new NotImplementedException(); } //This doesn't need implementing for this test
        }

        public Database Load()
        {
            return null;
        }

        public event EventHandler<QueryExecutedEventArgs> QueryExecuted; //Don't need to do anything with this either
    }
}
