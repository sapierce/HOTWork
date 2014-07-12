using System;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class AppointmentDelete : System.Web.UI.Page
    {
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Did we actually get the query string variable?
            if (Request.QueryString["TanID"] != null)
            {
                // Is there a value in the query string variable?
                if (!String.IsNullOrEmpty(Request.QueryString["TanID"].ToString()))
                {
                    int tanId = 0;
                    // Is the value a number?
                    if (Int32.TryParse(Request.QueryString["TanID"].ToString(), out tanId))
                    {
                        // Get the tan information
                        HOTBAL.Tan tanInfo = sqlClass.GetTanInformationByTanID(Convert.ToInt32(Request.QueryString["TanID"]));
                        
                        // Delete the tan
                        bool response = sqlClass.DeleteAppointment(Convert.ToInt32(Request.QueryString["TanID"]));

                        // Was the delete successful?
                        if (response)
                        {
                            // Go back to user information
                            Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + tanInfo.CustomerID, false);
                        }
                    }
                }
            }

            // Unable to delete tan, present a message
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            errorLabel.Text = "Unable to delete this tan. Please try again.<br />";
        }
    }
}