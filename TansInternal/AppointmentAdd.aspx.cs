using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class AppointmentAdd : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Add Tanning Appointment";

            try
            {
                if (!Page.IsPostBack)
                {
                    PopulateBeds();
                    lastName.Focus();
                }

                if (Request.QueryString["Confirm"] != null)
                {
                    // Schedule appointment
                    string response = String.Empty;
                    response = sqlClass.ScheduleAppointment(Convert.ToInt32(Request.QueryString["ID"].ToString()),
                        functionsClass.InternalCleanUp(Request.QueryString["Bed"].ToString()),
                        functionsClass.FormatDash(Convert.ToDateTime(Request.QueryString["Date"].ToString())),
                        functionsClass.InternalCleanUp(Request.QueryString["Time"].ToString()), "W", true, false);

                    if (response != HOTBAL.TansMessages.SUCCESS_MESSAGE)
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = response + "<br />";
                    }
                    else
                    {
                        Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString(), false);
                    }
                }
                else if (Request.QueryString["ID"] != null)
                {
                    HOTBAL.Customer customerInfo = sqlClass.GetCustomerInformationByID(Convert.ToInt32(Request.QueryString["ID"]));

                    if (String.IsNullOrEmpty(customerInfo.Error))
                    {
                        firstName.Text = customerInfo.FirstName;
                        lastName.Text = customerInfo.LastName;
                        customerID.Value = customerInfo.ID.ToString();
                        firstName.Enabled = false;
                        lastName.Enabled = false;
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = customerInfo.Error + "<br />";
                    }
                }
                else if (Request.QueryString["Date"] != null)
                {
                    if (!Page.IsPostBack)
                    {
                        appointmentDate.Text = functionsClass.FormatSlash(Convert.ToDateTime(Request.QueryString["Date"].ToString()));
                        appointmentBed.SelectedIndex = -1;
                        appointmentBed.Items.FindByValue(Request.QueryString["Bed"].ToString()).Selected = true;
                        appointmentBed.Items.FindByValue(Request.QueryString["Bed"].ToString()).Attributes.Add("selected", "true");

                        PopulateTime();
                        appointmentTime.SelectedIndex = -1;
                        appointmentTime.Items.FindByValue(Request.QueryString["Time"].ToString()).Selected = true;
                        appointmentTime.Items.FindByValue(Request.QueryString["Time"].ToString()).Attributes.Add("selected", "true");
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                sqlClass.LogErrorMessage(ex, "", "Internal: Add Tanning Appointment: PageLoad");
            }
        }

        private void PopulateBeds()
        {
            List<HOTBAL.Bed> bedList = sqlClass.GetLocationActiveBeds("W");

            if (bedList.Count > 0)
            {
                appointmentBed.Items.Clear();
                appointmentBed.Items.Add(new ListItem("-Select a Bed-", "0"));

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
            appointmentTime.Items.Insert(0, new ListItem("-Select a Time-", "0"));

            //Available times
            timeArray = sqlClass.GetAllTanTimes(tanDay, appointmentBed.SelectedValue, "W", true);

            //See what times are already taken
            timeArray = sqlClass.GetAvailableTanTimes(appointmentBed.SelectedValue, appointmentDate.Text, timeArray);

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

        protected void addAppointment_Click(object sender, EventArgs e)
        {
            string response = String.Empty;

            try
            {
                if ((String.IsNullOrEmpty(customerID.Value)) || (customerID.Value == "0"))
                {
                    // Find the customer
                    List<HOTBAL.Customer> findCustomer = sqlClass.GetCustomerByName(firstName.Text, lastName.Text, true);

                    if (!String.IsNullOrEmpty(findCustomer[0].Error))
                    {
                        // Got an error looking up customer
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = findCustomer[0].Error + " Check the name and <a href='" + HOTBAL.TansConstants.ADD_APPT_INTERNAL_URL + "?Date=" + appointmentDate.Text + "&Time=" + appointmentTime.Text + "&Bed=" + appointmentBed.SelectedValue + "&Loc=W'>try again</a> or <a href='" + HOTBAL.TansConstants.CUSTOMER_ADD_INTERNAL_URL + "?Date=" + appointmentDate.Text + "'>add a new user</a>." + "<br />";
                    }
                    else if (findCustomer.Count > 1)
                    {
                        // More than one customer found
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "Which " + firstName.Text + " " + lastName.Text + " were you looking for?<br />";
                        foreach (HOTBAL.Customer c in findCustomer)
                        {
                            errorLabel.Text += "<a href='" + HOTBAL.TansConstants.ADD_APPT_INTERNAL_URL + "?Confirm=Y&ID=" + c.ID + "&Date=" + appointmentDate.Text + "&Time=" + appointmentTime.Text + "&Bed=" + appointmentBed.SelectedValue + "'>" + c.LastName + ", " + c.FirstName + "</a><br />";
                        }
                    }
                    else
                    {
                        // Only found one customer
                        // Schedule appointment
                        response = sqlClass.ScheduleAppointment(Convert.ToInt32(findCustomer[0].ID),
                            functionsClass.InternalCleanUp(appointmentBed.SelectedValue),
                            functionsClass.FormatDash(Convert.ToDateTime(appointmentDate.Text)),
                            functionsClass.InternalCleanUp(appointmentTime.Text), "W", true, false);

                        if (response != HOTBAL.TansMessages.SUCCESS_MESSAGE)
                        {
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text += response + "<br />";
                        }
                        else
                        {
                            Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + findCustomer[0].ID, false);
                        }
                    }
                }
                else
                {
                    //Schedule appointment
                    response = sqlClass.ScheduleAppointment(Convert.ToInt32(customerID.Value),
                        functionsClass.InternalCleanUp(appointmentBed.SelectedValue),
                        functionsClass.FormatDash(Convert.ToDateTime(appointmentDate.Text)),
                        functionsClass.InternalCleanUp(appointmentTime.Text), "W", true, false);

                    if (response != HOTBAL.TansMessages.SUCCESS_MESSAGE)
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = response;
                    }
                    else
                    {
                        Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + customerID.Value, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                sqlClass.LogErrorMessage(ex, "", "Internal: Add Appointment: addAppointment_Click");
            }
        }

        protected void appointmentDate_TextChanged(object sender, EventArgs e)
        {
            PopulateTime();
        }
    }
}