using Rex.WebForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TV : ViewPageBase
{
    private TVViewModel vm;

    protected void Page_Load(object sender, EventArgs e)
    {
        vm = new TVViewModel(GetMetaLoader(), conn_id, schema_id, tableview_id);
        if (!IsPostBack)
        {
            reload();
        }
    }

    private void reload()
    {
        lnkDatabase.NavigateUrl = String.Format("Database.aspx?conn={0}", Server.UrlEncode(vm.ConnId));
        lnkDatabase.Text = "Database: " + Server.HtmlEncode(vm.ConnId);
        lblTV.Text = Server.HtmlEncode(vm.TVName) + " (" + Server.HtmlEncode(vm.TVTypeName) + ")";
        gv.DataSource = vm.ColumnDisplayData.OrderBy(x => x.ColumnName);
        DataBind();
    }
}