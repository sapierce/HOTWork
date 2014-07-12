using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace PublicWebsite.MembersArea
{
    public partial class LogOn : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods methodClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Page.Title + " - Member Log On";
        }

        protected void siteLogOn_OnClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    bool logonReturn = methodClass.VerifyLogin(userName.Text, passWord.Text);

                    if (logonReturn)
                    {
                        Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_PUBLIC_URL, false);
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_LOGIN;
                    }
                }
                catch (Exception ex)
                {
                    methodClass.LogErrorMessage(ex, "", "Login: PageLoad");
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                }
            }
        }
    }
}
