using Rex.Lib;
using System;

namespace Rex.WebForms.ViewModel.Test
{
    /// <summary>
    /// Trivial implementation of ILoader to return a non-null Database from the Load method
    /// Simulates a loader that at least instantiates the highest level in the metadata hierarchy
    /// but no schemas.
    /// </summary>
    internal sealed class DatabaseWithNoSchemasTestLoader : ILoader
    {
        public string Name
        {
            get { return "DatabaseWithNoSchemasTestLoader"; }
        }

        public Database Load()
        {
            return new Database("DatabaseWithNoSchemasTestLoader NAME", "DatabaseWithNoSchemasTestLoader ID");
        }

        public event EventHandler QueryExecuted; //Don't need to do anything with this either
    }
}