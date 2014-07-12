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


namespace PublicWebsite.MembersArea
{
    public partial class DeleteAppointment : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteAppointment(Convert.ToInt32(Request.QueryString["ID"]));

            Server.Transfer(HOTBAL.TansConstants.CUSTOMER_INFO_PUBLIC_URL);
        }
    }
}
