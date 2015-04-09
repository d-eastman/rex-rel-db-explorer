using Rex.WebForms.ViewModel;
using System;
using System.Linq;

public partial class Database : ViewPageBase
{
    private DatabaseViewModel vm;

    protected void Page_Load(object sender, EventArgs e)
    {
        vm = new DatabaseViewModel(GetMetaLoader(), conn_id);
        if (!IsPostBack)
        {
            reload();
        }
    }

    private void reload()
    {
        lblDatabase.Text = Server.HtmlEncode(vm.ConnId);
        gv.DataSource = vm.TVDisplayData.OrderBy(x => x.SchemaName).ThenBy(x => x.TVName);
        DataBind();
    }
}