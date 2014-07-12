using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class SitePasswords : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Site Passwords";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
        }

        protected void submitAdminChange_Click(object sender, EventArgs e)
        {
            if (newAdminPwd.Text.Trim() == newAdminConfirm.Text.Trim())
            {
                string response = sqlClass.AdministrationUpdate(oldAdminPwd.Text, newAdminPwd.Text, "Login");

                if (response.Contains("successfully"))
                {
                    Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL, false);
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = response;
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "New passwords do not match.";
            }
        }

        protected void submitRenew_Click(object sender, EventArgs e)
        {
            if (newRenewPwd.Text.Trim() == newRenewConfirm.Text.Trim())
            {
                string response = sqlClass.AdministrationUpdate(oldRenewPwd.Text, newRenewPwd.Text, "Renew");

                if (response.Contains("successfully"))
                {
                    Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL, false);
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = response;
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "New passwords do not match.";
            }
        }
    }
}