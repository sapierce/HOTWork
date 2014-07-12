using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class Error500 : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Server Error";
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            errorLabel.Text = HOTBAL.TansMessages.ERROR_500;
            functionsClass.SendErrorMail("Server Error", new Exception("ServerError"), HttpContext.Current.Request.Url.AbsoluteUri);
        }
    }
}