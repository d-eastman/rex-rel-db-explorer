using Rex.Lib;
using System.Collections.Generic;
using System.Linq;

namespace Rex.WebForms.ViewModel
{
    public class DatabaseViewModel : ViewModelBase
    {
        public DatabaseViewModel(ILoader loader, string connId)
            : base(loader)
        {
            ConnId = connId;
        }

        public List<TVDisplayData> TVDisplayData //All tables in all schemas
        {
            get
            {
                List<TVDisplayData> ret = Data.Schemas.SelectMany(x => x.TablesViews).Select(tv => new TVDisplayData
                {
                    SchemaName = tv.Schema.Name,
                    TVName = tv.Name,
                    TVTypeName = tv.Type == TableView.TableViewTypes.Table ? "Table" : "View",
                    TVLink = "TV.aspx?conn=" + ConnId + "&sch=" + tv.Schema.Id + "&tv=" + tv.Id
                }).ToList();
                return ret;
            }
        }
    }
}