using System;
using System.Web.UI;

namespace HOTTropicalTans.employees
{
    public partial class EmployeeProductCounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Employee Product Counts";
        }
    }
}