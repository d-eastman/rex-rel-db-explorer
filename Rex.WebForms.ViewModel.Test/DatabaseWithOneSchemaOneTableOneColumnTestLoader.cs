using Rex.Lib;
using System;

namespace Rex.WebForms.ViewModel.Test
{
    internal sealed class DatabaseWithOneSchemaOneTableTwoColumnTestLoader : ILoader
    {
        public string Name
        {
            get { return "DatabaseWithOneSchemaOneTableTwoColumnTestLoader"; }
        }

        public Database Load()
        {
            Database d = new Database("DatabaseWithOneSchemaOneTableTwoColumnTestLoader DB NAME", "DatabaseWithOneSchemaOneTableTwoColumnTestLoader DB ID");
            Schema s = new Schema("DatabaseWithOneSchemaOneTableTwoColumnTestLoader SCHEMA NAME", "DatabaseWithOneSchemaOneTableTwoColumnTestLoader SCHEMA ID", d);
            TableView tv = new TableView("DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV NAME",
                "DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV ID", s, TableView.TableViewTypes.Table);
            Column c = new Column("DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN1 NAME",
                "DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN1 ID", tv, "int", false, true);
            Column c2 = new Column("DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN2 NAME",
                "DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN2 ID", tv, "varchar(50)", true, false); 
            return d;
        }

        public event EventHandler<QueryExecutedEventArgs> QueryExecuted;
    }
}
