using System;
using System.Collections.Generic;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class AppointmentMassageAdd : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Add Massage Appointment";
            try
            {
                if (Request.QueryString["Confirm"] != null)
                {
                    //Schedule appointment
                    bool response = sqlClass.AddMassageAppointment(Convert.ToInt64(Request.QueryString["ID"].ToString()),
                        Convert.ToDateTime(Request.QueryString["Date"].ToString()),
                        functionsClass.LightCleanUp(Request.QueryString["Time"].ToString()),
                        Convert.ToInt32(Request.QueryString["Length"].ToString()));

                    if (!response)
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = response + "<br />";
                    }
                    else
                    {
                        Response.Redirect("Default.aspx?Date=" + Request.QueryString["Date"].ToString(), false);
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
                    appointmentDate.Text = functionsClass.FormatSlash(Convert.ToDateTime(Request.QueryString["Date"].ToString()));
                    appointmentTime.Text = Request.QueryString["Time"].ToString();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                sqlClass.LogErrorMessage(ex, "", "Internal: Add Massage Appointment: PageLoad");
            }
        }

        protected void addAppointment_Click(object sender, EventArgs e)
        {
            bool response = false;

            try
            {
                if ((String.IsNullOrEmpty(customerID.Value)) || (customerID.Value == "0"))
                {
                    // Find the customer
                    List<HOTBAL.Customer> findCustomer = sqlClass.GetCustomerByName(firstName.Text, lastName.Text, true);

                    if (String.IsNullOrEmpty(findCustomer[0].Error))
                    {
                        if (findCustomer.Count == 0)
                        {
                            // Cannot find user
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = "Unable to locate customer.  Check the name and <a href='" + HOTBAL.TansConstants.ADD_APPT_MASSAGE_INTERNAL_URL + "?Date=" + appointmentDate.Text + "&Time=" + appointmentTime.Text + "&Loc=W'>try again</a> or <a href='" + HOTBAL.TansConstants.CUSTOMER_ADD_INTERNAL_URL + "?Date=" + appointmentDate.Text + "'>add a new user</a>." + "<br />";
                        }
                        else if (findCustomer.Count > 1)
                        {
                            // More than one customer found
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = "Which " + firstName + " " + lastName + " were you looking for?<br />";
                            foreach (HOTBAL.Customer c in findCustomer)
                            {
                                errorLabel.Text += "<a href='" + HOTBAL.TansConstants.ADD_APPT_MASSAGE_INTERNAL_URL + "?Confirm=Y&ID=" + c.ID + "&Date=" + appointmentDate.Text + "&Time=" + appointmentTime.Text + "'>" + c.LastName + ", " + c.FirstName + "</a><br />";
                            }
                        }
                        else
                        {
                            // Only found one customer
                            // Schedule appointment
                            response = sqlClass.AddMassageAppointment(Convert.ToInt64(findCustomer[0].ID),
                                Convert.ToDateTime(appointmentDate.Text),
                                appointmentTime.Text, Convert.ToInt32(appointmentLength.SelectedValue));

                            if (!response)
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                            }
                            else
                            {
                                Response.Redirect(HOTBAL.TansConstants.MAIN_INTERNAL_URL + "?Date=" + appointmentDate.Text, false);
                            }
                        }
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = findCustomer[0].Error;
                    }
                }
                else
                {
                    //Schedule appointment
                    response = sqlClass.AddMassageAppointment(Convert.ToInt32(customerID.Value),
                            Convert.ToDateTime(appointmentDate.Text),
                            appointmentTime.Text, Convert.ToInt32(appointmentLength.SelectedValue));

                    if (!response)
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                    else
                    {
                        Response.Redirect(HOTBAL.TansConstants.MAIN_INTERNAL_URL + "?Date=" + appointmentDate.Text, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
                sqlClass.LogErrorMessage(ex, "", "Internal: Add Massage Appointment: addAppointment_Click");
            }
        }

        protected void appointmentDate_TextChanged(object sender, EventArgs e)
        {
            PopulateTime();
        }

        private void PopulateTime()
        {
            ArrayList timeArray = new ArrayList();
            string tanDay = Convert.ToDateTime(appointmentDate.Text).DayOfWeek.ToString();

            appointmentTime.Items.Clear();
            appointmentTime.Items.Insert(0, new ListItem("-Choose-", "0"));

            //Available times
            timeArray = sqlClass.GetAllMassageTimes(tanDay);

            //See what times are already taken
            timeArray = sqlClass.GetAvailableMassageTimes(Convert.ToDateTime(appointmentDate.Text), timeArray);

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
    }
}