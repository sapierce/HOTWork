using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class Error403 : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Access Denied";
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            errorLabel.Text = HOTBAL.TansMessages.ERROR_403;
            functionsClass.SendErrorMail("Access Denied Error", new UnauthorizedAccessException("Access Denied"), HttpContext.Current.Request.Url.AbsoluteUri);
        }
    }
}