using System.Collections.Generic;

namespace Rex.Lib
{
    /// <summary>
    /// Database metadata class. A database directly contains 0 or more Schemas.
    /// </summary>
    public class Database
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public List<Schema> Schemas { get; private set; }

        public Database(string name, string id)
        {
            Id = id;
            Name = name;
            Schemas = new List<Schema>();
        }
    }
}