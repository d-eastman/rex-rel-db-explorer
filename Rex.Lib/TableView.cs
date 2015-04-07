using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rex.Lib
{
    /// <summary>
    /// Table/view metadata class. Tables and views are both handled by this class. The ObjectType property 
    /// indicates if it is a Table or a View. A TableView is contained in a Schema and contains 1 or more Columns.
    /// </summary>
    public class TableView
    {
        public enum TableViewTypes { Table = 1, View = 2 };

        public Schema Schema { get; private set; }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public TableViewTypes Type { get; private set; }

        public List<Column> Columns { get; private set; }

        public TableView(string name, string id, Schema schema, TableViewTypes type)
        {
            Schema = schema;
            Name = name;
            Id = id;
            if (!Enum.IsDefined(typeof(TableViewTypes), type))
                throw new ArgumentException("TableViewTypes type value is invalid");
            Type = type;
            Columns = new List<Column>();
            schema.TablesViews.Add(this);
        }
    }
}
