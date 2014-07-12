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

namespace HOTPOS
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public string scheduleDate = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            backTanning.NavigateUrl = HOTDAL.TansConstants.MAIN_INTERNAL_URL;
            backMartialArts.NavigateUrl = HOTDAL.SDAConstants.MAIN_INTERNAL_URL;
        }
    }
}
