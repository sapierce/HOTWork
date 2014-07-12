using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class Error : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Error";
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            functionsClass.SendErrorMail("Generic Error", new Exception("General Error"), HttpContext.Current.Request.Url.AbsoluteUri);
        }
    }
}