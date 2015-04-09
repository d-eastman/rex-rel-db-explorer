using Rex.Lib;
using System.Collections.Generic;
using System.Linq;

namespace Rex.WebForms.ViewModel
{
    public class TVViewModel : ViewModelBase
    {
        public TVViewModel(ILoader loader, string connId, string schId, string tvId)
            : base(loader)
        {
            ConnId = connId;
            SchemaId = schId;
            TVId = tvId;
        }

        public string TVName
        {
            get
            {
                var s = Data.Schemas.Where(x => x.Id == SchemaId).Single();
                var t = s.TablesViews.Where(x => x.Id == TVId).Single();
                return t.Name;
            }
        }

        public string TVTypeName
        {
            get
            {
                var s = Data.Schemas.Where(x => x.Id == SchemaId).Single();
                var tv = s.TablesViews.Where(x => x.Id == TVId).Single();
                return tv.Type == TableView.TableViewTypes.Table ? "Table" : "View";
            }
        }

        public List<ColumnDisplayData> ColumnDisplayData //All columns in schema.table
        {
            get
            {
                var s = Data.Schemas.Where(x => x.Id == SchemaId).Single();
                var t = s.TablesViews.Where(x => x.Id == TVId).Single();
                List<ColumnDisplayData> ret = t.Columns.Select(col => new ColumnDisplayData
                {
                    PK = col.PrimaryKey ? "PK" : "",
                    ColumnName = col.Name,
                    ColumnLink = "Column.aspx?conn=" + ConnId + "&sch=" + SchemaId + "&tv=" + TVId + "&col=" + col.Id,
                    ColumnTypeName = col.TypeName,
                    Nullability = col.Nullable ? "Null" : "Req'd"
                }).ToList();
                return ret;
            }
        }

    }
}
