using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Connections : System.Web.UI.Page //This page based on regular ASP Page, not the custom one
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //This should probably be in a viewmodel, but keeping it here for now
            gvConnections.DataSource = LoaderWebFactory.GetSingletonInstance().GetDatabaseConnections().OrderBy(x => x.Name); 
            DataBind();
        }
    }

    protected void gvConnections_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        int index;
        if (int.TryParse(e.CommandArgument.ToString(), out index))
        {
            GridViewRow row = gvConnections.Rows[index];
            if (row.Cells.Count > 0 && row.Cells[0].Controls.Count > 0)
            {
                LinkButton button = row.Cells[0].Controls[0] as LinkButton;
                if (button != null)
                {
                    string connStringName = button.Text;
                    Response.Redirect("Database.aspx?conn=" + Server.UrlEncode(connStringName));
                }
            }
        }
        Response.Write("Blast! Something unexpected happened when you selected the connection!");
    }
}