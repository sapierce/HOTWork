using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class AppointmentEdit : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Edit Appointment";
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["TanID"] != null)
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["TanID"].ToString()))
                        {
                            long tanId = 0;
                            if (Int64.TryParse(Request.QueryString["TanID"].ToString(), out tanId))
                            {
                                HOTBAL.Tan tanInformation = sqlClass.GetTanInformationByTanID(Convert.ToInt32(Request.QueryString["TanID"].ToString()));

                                if (tanInformation != null)
                                {
                                    HOTBAL.Customer customerInformation = sqlClass.GetCustomerInformationByID(tanInformation.CustomerID);

                                    if (String.IsNullOrEmpty(customerInformation.Error))
                                    {
                                        customerName.Text = customerInformation.FirstName + " " + customerInformation.LastName;
                                        customerID.Value = tanInformation.CustomerID.ToString();
                                        PopulateBeds();
                                        appointmentBed.Items.FindByText(tanInformation.Bed).Selected = true;
                                        appointmentDate.Text = tanInformation.Date;
                                        PopulateTime();
                                        appointmentTime.Items.FindByText(tanInformation.Time).Selected = true;
                                        appointmentLength.Text = tanInformation.Length.ToString();
                                    }
                                    else
                                    {
                                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                        errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_CUSTOMER_INTERNAL + "<br />";
                                    }
                                }
                                else
                                {
                                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                    errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_TAN + "<br />";
                                }
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_TAN + "<br />";
                            }
                        }
                        else
                        {
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_TAN + "<br />";
                        }
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_TAN + "<br />";
                    }
                }
                catch (Exception ex)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                    sqlClass.LogErrorMessage(ex, "", "Internal: Edit Appointment: PageLoad");
                }
            }
        }

        private void PopulateBeds()
        {
            List<HOTBAL.Bed> bedList = new List<HOTBAL.Bed>();
            bedList = sqlClass.GetLocationActiveBeds("W");

            if (bedList.Count > 0)
            {
                appointmentBed.Items.Add(new ListItem("-Choose-", "0"));

                foreach (HOTBAL.Bed b in bedList)
                {
                    if (b.BedDisplayInternal)
                        appointmentBed.Items.Add(new ListItem(b.BedShort, b.BedShort));
                }
            }
        }

        private void PopulateTime()
        {
            ArrayList timeArray = new ArrayList();
            string tanDay = Convert.ToDateTime(appointmentDate.Text).DayOfWeek.ToString();

            appointmentTime.Items.Clear();
            appointmentTime.Items.Insert(0, new ListItem("-Choose-", "0"));

            //Available times
            timeArray = sqlClass.GetAllTanTimes(tanDay, functionsClass.LightCleanUp(appointmentBed.SelectedValue), "W", true);

            //See what times are already taken
            //timeArray = sqlClass.GetAvailableTanTimes(functionsClass.LightCleanUp(appointmentBed.SelectedValue), appointmentDate.Text, timeArray);

            foreach (string i in timeArray)
            {
                if (Convert.ToDateTime(appointmentDate.Text) == DateTime.Now)
                {
                    if (Convert.ToDateTime(i) > DateTime.Now)
                        appointmentTime.Items.Add(new ListItem(i, i));
                }
                else
                {
                    appointmentTime.Items.Add(new ListItem(i, i));
                }
            }
        }

        protected void editAppointment_Click(object sender, EventArgs e)
        {
            string response = String.Empty;

            try
            {
                if (swapAppointments.Checked)
                {
                    // Swapping appointments
                    string oldUpdatingTanBed = String.Empty;

                    HOTBAL.Tan updatingTan = sqlClass.GetTanInformationByTanID(Convert.ToInt32(Request.QueryString["TanID"].ToString()));
                    if (updatingTan != null)
                    {
                        oldUpdatingTanBed = updatingTan.Bed;

                        HOTBAL.Tan swappingTan = sqlClass.GetTanInformationByData(appointmentBed.SelectedValue,
                            functionsClass.FormatDash(Convert.ToDateTime(appointmentDate.Text)),
                            appointmentTime.SelectedValue, "W");

                        if (swappingTan != null)
                        {
                            string swappedTanUpdate = sqlClass.UpdateAppointment(swappingTan.TanID, swappingTan.CustomerID, oldUpdatingTanBed,
                                swappingTan.Date, swappingTan.Time, swappingTan.Location, swappingTan.Length, true);

                            if (swappedTanUpdate == HOTBAL.TansMessages.SUCCESS_MESSAGE)
                            {
                                string newTanUpdate = sqlClass.UpdateAppointment(Convert.ToInt32(Request.QueryString["TanID"].ToString()), Convert.ToInt32(customerID.Value),
                                    appointmentBed.SelectedValue, functionsClass.FormatDash(Convert.ToDateTime(appointmentDate.Text)),
                                    appointmentTime.SelectedValue, "W", Convert.ToInt32(appointmentLength.Text), false);

                                if (newTanUpdate != HOTBAL.TansMessages.SUCCESS_MESSAGE)
                                {
                                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                    errorLabel.Text = newTanUpdate + "<br />";
                                }
                                else
                                {
                                    Label errorLabel = (Label)this.Master.FindControl("successMessage");
                                    errorLabel.Text = "Appointment updated.<br />";
                                    errorLabel.Text += "<a href='" + HOTBAL.TansConstants.MAIN_INTERNAL_URL + "'>Back to Schedule</a><br>";
                                    errorLabel.Text += "<a href='" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + customerID.Value + "'>Back to User Information</a><br>";
                                }
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text = swappedTanUpdate + "<br />";
                            }
                        }
                    }
                }
                else
                {
                    // Editing current appointment
                    response = sqlClass.UpdateAppointment(Convert.ToInt32(Request.QueryString["TanID"].ToString()), Convert.ToInt32(customerID.Value),
                        appointmentBed.SelectedValue, functionsClass.FormatDash(Convert.ToDateTime(appointmentDate.Text)),
                        appointmentTime.SelectedValue, "W", Convert.ToInt32(appointmentLength.Text), false);

                    if (response != HOTBAL.TansMessages.SUCCESS_MESSAGE)
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = response + "<br />";
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("successMessage");
                        errorLabel.Text = "Appointment updated.<br />";
                        errorLabel.Text += "<a href='" + HOTBAL.TansConstants.MAIN_INTERNAL_URL + "'>Back to Schedule</a><br>";
                        errorLabel.Text += "<a href='" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + customerID.Value + "'>Back to User Information</a><br>";
                    }
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                sqlClass.LogErrorMessage(ex, "", "Internal: Edit Appointment: editAppointment_Click");
            }
        }
    }
}