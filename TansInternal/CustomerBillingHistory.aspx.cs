using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class CustomerBillingHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
            HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

            HOTBAL.Customer customer = sqlClass.GetCustomerInformationByID(Convert.ToInt32(Request.QueryString["ID"]));

            if (String.IsNullOrEmpty(customer.Error))
            {
                customerName.Text = customer.FirstName + " " + customer.LastName;
                currentPlan.Text = customer.Plan;
                renewalDate.Text = functionsClass.FormatSlash(customer.RenewalDate);

                billingHistory.DataSource = sqlClass.GetCustomerBillingByID(Convert.ToInt32(Request.QueryString["ID"]));
                billingHistory.DataBind();
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = customer.Error;
            }
        }

        protected void billingHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            billingHistory.PageIndex = e.NewPageIndex;
            DataTable dt1 = ViewState["Table"] as DataTable;
            DataView dv = new DataView(dt1);

            billingHistory.DataSource = dv;
            billingHistory.DataBind();
        }
    }
}