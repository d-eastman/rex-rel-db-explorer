using Rex.Lib;
using System.Collections.Generic;
using System.Linq;

namespace Rex.WebForms.ViewModel
{
    public class ColumnViewModel : ViewModelBase
    {
        public ColumnViewModel(ILoader loader, string connId, string schId, string tvId, string colId)
            : base(loader)
        {
            ConnId = connId;
            SchemaId = schId;
            TVId = tvId;
            ColumnId = colId;
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

        public string ColumnName
        {
            get
            {
                var s = Data.Schemas.Where(x => x.Id == SchemaId).Single();
                var tv = s.TablesViews.Where(x => x.Id == TVId).Single();
                var c = tv.Columns.Where(x => x.Id == ColumnId).Single();
                return c.Name;
            }
        }

        public string ColumnTypeName
        {
            get
            {
                var s = Data.Schemas.Where(x => x.Id == SchemaId).Single();
                var tv = s.TablesViews.Where(x => x.Id == TVId).Single();
                var c = tv.Columns.Where(x => x.Id == ColumnId).Single();
                return c.TypeName;
            }
        }
    }
}
