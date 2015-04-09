using Rex.WebForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Column : ViewPageBase
{
    private ColumnViewModel vm;

    protected void Page_Load(object sender, EventArgs e)
    {
        vm = new ColumnViewModel(GetMetaLoader(), conn_id, schema_id, tableview_id, column_id);
        if (!IsPostBack)
        {
            reload();
        }
    }

    private void reload()
    {
        lnkDatabase.NavigateUrl = String.Format("Database.aspx?conn={0}", Server.UrlEncode(vm.ConnId));
        lnkDatabase.Text = "Database: " + Server.HtmlEncode(vm.ConnId);
        lnkTV.NavigateUrl = String.Format("TV.aspx?conn={0}&sch={1}&tv={2}", Server.UrlEncode(vm.ConnId), Server.UrlEncode(vm.SchemaId), Server.UrlEncode(vm.TVId));
        lnkTV.Text = "Table/View: " + Server.HtmlEncode(vm.TVName) + " (" + Server.HtmlEncode(vm.TVTypeName) + ")";
        lblColumn.Text = Server.HtmlEncode(vm.ColumnName) + " (" + Server.HtmlEncode(vm.ColumnTypeName) + ")";
    }
}