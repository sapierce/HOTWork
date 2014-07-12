using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace PublicWebsite
{
    public partial class Members : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Members";
        }

        protected void signUpSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate("signUp");
            if (Page.IsValid)
            {
                if (signUpSelect.SelectedValue == "NEW")
                {
                    Response.Redirect(HOTBAL.TansConstants.REGISTER_NEW_PUBLIC_URL);
                }
                else if (signUpSelect.SelectedValue == "CURRENT")
                {
                    Response.Redirect(HOTBAL.TansConstants.REGISTER_EXIST_PUBLIC_URL);
                }
            }
        }
    }
}
