using Rex.Lib;
using System;

namespace Rex.WebForms.ViewModel.Test
{
    internal sealed class DatabaseWithOneSchemaOneTableTestLoader : ILoader
    {
        public string Name
        {
            get { return "DatabaseWithOneSchemaOneTableTestLoader"; }
        }

        public Database Load()
        {
            Database d = new Database("DatabaseWithOneSchemaOneTableTestLoader DB NAME", "DatabaseWithOneSchemaOneTableTestLoader DB ID");
            Schema s = new Schema("DatabaseWithOneSchemaOneTableTestLoader SCHEMA NAME", "DatabaseWithOneSchemaOneTableTestLoader SCHEMA ID", d);
            TableView tv = new TableView("DatabaseWithOneSchemaOneTableTestLoader TV NAME",
                "DatabaseWithOneSchemaOneTableTestLoader TV ID", s, TableView.TableViewTypes.Table);
            return d;
        }

        public event EventHandler<QueryExecutedEventArgs> QueryExecuted; //Don't need to do anything with this either
    }
}