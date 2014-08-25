using System;
using System.Web.UI;

namespace PublicWebsite
{
    public partial class PrivacyPolicy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Privacy Policy";
        }
    }
}