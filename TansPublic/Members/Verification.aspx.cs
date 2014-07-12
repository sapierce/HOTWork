using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite.MembersArea
{
    public partial class Verification : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Pull QueryStrings
                Guid guid = new Guid(Request.QueryString["ID"]);
                string emailAddress = Request.QueryString["email"];

                if (guid != null && emailAddress != null)
                {
                    string response = sqlClass.VerifyEmail(guid, emailAddress);

                    if (response == HOTBAL.TansMessages.SUCCESS_MESSAGE)
                    {
                        Label errorLabel = (Label)this.Master.FindControl("successMessag");
                        errorLabel.Text += "<h4 style='color:red'> Thank you!  Your email has been verified! </h4>";
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text += response;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text += HOTBAL.TansMessages.ERROR_INVALID_VERIFY_LINK;
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, Request.QueryString.ToString(), "MembersArea: Verification");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }
    }
}