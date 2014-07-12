using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileSite
{
    public partial class Logon : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods methodClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitLogin_OnClick(object sender, EventArgs e)
        {
            Page.Validate("mobileLogon");

            if (Page.IsValid)
            {
                try
                {
                    bool loginReturn = methodClass.VerifyLogin(loginUser.Text, loginPassword.Text);

                    if (loginReturn)
                    {
                        Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_MOBILE_URL, false);
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "&#149; " + HOTBAL.TansMessages.ERROR_LOGIN + "<br />";
                    }
                }
                catch (Exception ex)
                {
                    methodClass.LogErrorMessage(ex, "", "MobileLogin: PageLoad");
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "&#149; " + HOTBAL.TansMessages.ERROR_GENERIC + "<br />";
                }
            }
        }
    }
}