using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class ucMember : System.Web.UI.UserControl
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (functionsClass.isLoggedIn())
            {
                HOTBAL.Customer  customer = sqlClass.GetCustomerInformationByID(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()));

                if (String.IsNullOrEmpty(customer.Error))
                {
                    memberName.Text = "<b>Welcome, " + customer.FirstName + " " + customer.LastName + "!</b>";
                    linksList.Text = "<a href='/Members/MemberInfo.aspx'>View Your Information</a> | <a href='/Members/AddAppointment.aspx'>Add Appointment</a> | <a href='/ShoppingCart.aspx?action=viewcart'>View Your Cart</a>";
                }

                //TODO: Check that they've accepted the warning if they are signed up online users
            }
        }
    }
}