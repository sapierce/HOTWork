using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileSite
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Page.Title + " - Forgot Password";
            responsePanel.Visible = false;
        }

        protected void getPassword_onSubmit(object sender, EventArgs e)
        {
            Page.Validate("password");
            if (Page.IsValid)
            {
                forgotPasswordPanel.Visible = false;
                responsePanel.Visible = true;
                //Get the user from the e-mail
                string response = sqlClass.ResetUserPassword(functionsClass.LightCleanUp(emailAddress.Text));

                if (response == HOTBAL.TansMessages.SUCCESS_MESSAGE)
                {
                    responseMessage.Text = HOTBAL.TansMessages.PASSWORD_SENT.Replace("@Email", emailAddress.Text);
                }
                else
                {
                    errorMessage.Text = response;
                }
            }
        }
    }
}