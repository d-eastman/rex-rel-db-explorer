using Rex.Lib;

namespace Rex.WebForms.ViewModel
{
    public abstract class ViewModelBase
    {
        protected Database _Data;

        public string ConnId { get; protected set; }

        public string SchemaId { get; protected set; }

        public string TVId { get; protected set; }

        public string ColumnId { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public Database Data
        {
            get
            {
                if (_Data == null)
                {
                    _Data = Loader.Load();
                }
                return _Data;
            }
        }

        protected ILoader Loader { get; set; }

        public ViewModelBase(ILoader loader)
        {
            Loader = loader;
        }
    }
}