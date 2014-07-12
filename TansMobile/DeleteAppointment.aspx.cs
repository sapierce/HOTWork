using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileSite
{
    public partial class DeleteAppointment : System.Web.UI.Page
    {
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteAppointment(Convert.ToInt32(Request.QueryString["ID"]));

            if (response)
            {
                Response.Redirect("MemberInfo.aspx", false);
            }
        }
    }
}