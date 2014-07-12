using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class CustomerTanHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

            try
            {
                tanningHistory.DataSource = sqlClass.GetCustomerTansByID(Convert.ToInt32(Request.QueryString["ID"]));
                tanningHistory.DataBind();
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                sqlClass.LogErrorMessage(ex, "", "Internal: Tan History: PageLoad");
            }
        }

        protected void tanningHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tanningHistory.PageIndex = e.NewPageIndex;
            DataTable dt1 = ViewState["Table"] as DataTable;
            DataView dv = new DataView(dt1);

            tanningHistory.DataSource = dv;
            tanningHistory.DataBind();
        }
    }
}