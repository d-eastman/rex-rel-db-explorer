namespace Rex.WebForms.ViewModel
{
    /// <summary>
    /// Strongly-typed database connection data properties
    /// </summary>
    public class DbConnData
    {
        public string Provider { get; set; }

        public string Name { get; set; }

        public string Server { get; set; }

        public string Database { get; set; }

        public string ConnectAs { get; set; }
    }
}