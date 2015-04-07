using System.Collections.Generic;

namespace Rex.Lib
{
    /// <summary>
    /// Schema metadata class. A Schema is contained in a Database and contains 0 or more tables/views.
    /// </summary>
    public class Schema
    {
        public Database Database { get; private set; }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public List<TableView> TablesViews { get; private set; }

        public Schema(string name, string id, Database db)
        {
            Database = db;
            db.Schemas.Add(this);
            Id = id;
            Name = name;
            TablesViews = new List<TableView>();
        }
    }
}