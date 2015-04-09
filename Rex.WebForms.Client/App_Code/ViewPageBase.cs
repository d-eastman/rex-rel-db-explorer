using Rex.Lib;

/// <summary>
/// Summary description for ViewPageBase
/// </summary>
public class ViewPageBase : System.Web.UI.Page
{
    protected string conn_id;
    protected string schema_id;
    protected string tableview_id;
    protected string column_id;

    public ViewPageBase()
    {
    }

    protected void LoadQueryStringParams()
    {
        if (Request.QueryString["conn"] != null)
            conn_id = Request.QueryString["conn"].ToString();
        if (Request.QueryString["sch"] != null)
            schema_id = Request.QueryString["sch"].ToString();
        if (Request.QueryString["tv"] != null)
            tableview_id = Request.QueryString["tv"].ToString();
        if (Request.QueryString["col"] != null)
            column_id = Request.QueryString["col"].ToString();
    }

    protected ILoader GetMetaLoader()
    {
        LoadQueryStringParams();
        ILoader ret = LoaderWebFactory.GetSingletonInstance().GetMetaLoader(conn_id);
        if (ret == null)
        {
            Response.Redirect("Default.aspx"); //Something's wrong, go home
        }
        return ret;
    }
}