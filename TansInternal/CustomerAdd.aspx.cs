using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class CustomerAdd : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Add Customer";
            if (!Page.IsPostBack)
            {
                PopulatePlans();
                joinDate.Text = functionsClass.FormatSlash(DateTime.Now);
            }
        }

        protected void PopulatePlans()
        {
            List<HOTBAL.Package> availablePlans = sqlClass.GetAllPackages();

            if (availablePlans != null)
            {
                plans.Items.Clear();
                plans.Items.Add(new ListItem("-Choose-", ""));

                foreach (HOTBAL.Package p in availablePlans)
                {
                    plans.Items.Add(new ListItem(p.PackageName, p.PackageID.ToString() + ":" + p.PackageLength.ToString()));
                }
            }
        }

        protected void plans_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] splitPlan = plans.SelectedValue.Split(Convert.ToChar(":"));

            DateTime calculateDate = DateTime.Now.AddDays(Convert.ToDouble(splitPlan[1]));

            renewalDate.Text = functionsClass.FormatSlash(calculateDate);
        }

        protected void addCustomer_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            try
            {
                if (Page.IsValid)
                {
                    string[] splitPlan = plans.SelectedValue.Split(Convert.ToChar(":"));
                    HOTBAL.Package getPlanInfo = sqlClass.GetPackageByPackageID(Convert.ToInt32(splitPlan[0]));

                    Int64 insertCustomer = sqlClass.InsertNewCustomer(firstName.Text,
                        lastName.Text, Convert.ToDateTime(joinDate.Text),
                        Convert.ToInt32(fitzpatrickNumber.SelectedValue),
                        getPlanInfo.PackageNameShort, Convert.ToDateTime(renewalDate.Text),
                        remarks.Text, false, false);
                    if (insertCustomer > 0)
                        Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + insertCustomer.ToString());
                    else
                    {
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                    }
                }
                else
                {
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "Internal: Add Customer: addCustomer_Click");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
            }
        }
    }
}