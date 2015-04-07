using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rex.Lib
{
    /// <summary>
    /// Column metadata class. A Column is contained in a TableView.
    /// </summary>
    public class Column
    {
        public TableView TableView { get; private set; }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string TypeName { get; private set; }

        public bool Nullable { get; private set; }

        public bool PrimaryKey { get; private set; }

        public Column(string name, string id, TableView tv, string typeName, bool nullable, bool pk)
        {
            TableView = tv;
            Name = name;
            Id = id;
            TypeName = typeName;
            Nullable = nullable;
            PrimaryKey = pk;
            tv.Columns.Add(this);
        }
    }
}
