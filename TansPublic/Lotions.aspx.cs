using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class Lotions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Lotions";
        }
    }
}