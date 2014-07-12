using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class Error404 : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Not Found";
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            errorLabel.Text = HOTBAL.TansMessages.ERROR_404;
            functionsClass.SendErrorMail("File not found Error", new Exception("NotFound"), HttpContext.Current.Request.Url.AbsoluteUri);
        }
    }
}