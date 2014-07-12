using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void onClick_btnLogin(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.CUSTOMER_LOGON_MOBILE_URL, false);
        }

        protected void onClick_btnAbout(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.ABOUT_MOBILE_URL, false);
        }

        protected void onClick_btnProducts(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.PRODUCTS_MOBILE_URL, false);
        }

        protected void onClick_btnBeds(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.BEDS_MOBILE_URL, false);
        }
    }
}
