using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class AppointmentMassageEdit : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Edit Massage Appointment";
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["MassageID"] != null)
                    {
                        HOTBAL.Massage massageInformation = sqlClass.GetMassageInformationByMassageID(Convert.ToInt32(Request.QueryString["MassageID"].ToString()));

                        if (massageInformation != null)
                        {
                            HOTBAL.Customer customerInformation = sqlClass.GetCustomerInformationByID(massageInformation.UserID);
                            customerName.Text = customerInformation.FirstName + " " + customerInformation.LastName;
                            customerID.Value = massageInformation.UserID.ToString();
                            appointmentDate.Text = functionsClass.FormatSlash(massageInformation.Date);
                            appointmentTime.Text = massageInformation.Time;
                            appointmentLength.Items.FindByValue(massageInformation.Length.ToString()).Selected = true;
                        }
                        else
                        {
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_MASSAGE + "<br />";
                        }
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_MASSAGE + "<br />";
                    }
                }
                catch (Exception ex)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                    sqlClass.LogErrorMessage(ex, "", "Internal: Edit Massage Appointment: PageLoad");
                }
            }
        }

        protected void editAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                // Editing current appointment
                bool response = sqlClass.UpdateMassageAppointment(Convert.ToInt32(Request.QueryString["MassageID"].ToString()),
                    Convert.ToDateTime(appointmentDate.Text),
                    appointmentTime.Text, Convert.ToInt32(appointmentLength.Text));

                if (!response)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("successMessage");
                    errorLabel.Text = "Appointment updated.<br />";
                    errorLabel.Text += "<a href='" + HOTBAL.TansConstants.MAIN_INTERNAL_URL + "'>Back to Schedule</a><br>";
                    errorLabel.Text += "<a href='" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + customerID.Value + "'>Back to User Information</a><br>";
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                sqlClass.LogErrorMessage(ex, "", "Internal: Edit Massage Appointment: editAppointment_Click");
            }
        }
    }
}