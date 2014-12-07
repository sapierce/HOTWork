using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite.MembersArea
{
    public partial class AddAppointment : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Page.Title + " - Add an Appointment";

            try
            {
                if (!functionsClass.isLoggedIn())
                {
                    //User is logged out
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.SESSION_EXPIRED_PUBLIC;
                }
                else
                {
                    appointment.Style.Remove("display");
                    //confirmation.Style.Add("display", "none");
                    if (!Page.IsPostBack)
                    {
                        try
                        {
                            HOTBAL.Customer user = new HOTBAL.Customer();
                            user = sqlClass.GetCustomerInformationByID(Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()));

                            if (String.IsNullOrEmpty(user.Error))
                            {
                                //functionsClass.buildAppointmentDatesList(appointmentDate);
                                //functionsClass.buildBedTypeList(bedType);

                                customerName.Text = user.FirstName + " " + user.LastName;
                                unavailableMessage.Text = "We're sorry, online scheduling is not available at this time. Please call the salon to schedule your appointment.";

                                //if (String.IsNullOrEmpty(user.Error))
                                //{
                                //    if (user.VerifiedEmail)
                                //    {
                                //        emailReminder.Visible = true;
                                //    }
                                //}
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text = user.Error;
                            }
                        }
                        catch (Exception ex)
                        {
                            sqlClass.LogErrorMessage(ex, "", "Site: AddAppointment: LoadCustomer");
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "Site: AddAppointment: Load");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }

        ///// <summary>
        ///// Bind bed based on type, package check
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void bedType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        List<HOTBAL.Bed> bedListing = new List<HOTBAL.Bed>();
        //        bedListing = sqlClass.GetBedsByType(functionsClass.CleanUp(bedType.SelectedValue));
        //        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
        //        errorLabel.Text = String.Empty;
        //        bedPreference.Items.Clear();
        //        bedPreference.Items.Add(new ListItem("-Choose-", "0"));

        //        if (bedListing != null)
        //        {
        //            foreach (HOTBAL.Bed x in bedListing)
        //            {
        //                bedPreference.Items.Add(new ListItem(x.BedLong, x.BedShort));
        //            }
        //        }
        //        else
        //        {
        //            //No active beds of that type
        //            bedPreference.Items.Add(new ListItem("No beds available", "0"));
        //        }

        //        //Check the customer's package, alert if they select a bed not on their package
        //        string packageCheck = sqlClass.CheckCustomerPackage(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()), bedType.SelectedValue);

        //        if (packageCheck != HOTBAL.TansMessages.SUCCESS_MESSAGE)
        //        {
        //            errorLabel.Text = packageCheck;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sqlClass.LogErrorMessage(ex, "", "Site: AddAppointment: bedType_SIC");
        //        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
        //        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
        //    }
        //}

        ///// <summary>
        ///// Bind Beds drop down based on Bed Type/Date
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void bedPreference_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ArrayList timeArray = new ArrayList();
        //        string tanDay = Convert.ToDateTime(appointmentDate.SelectedValue).DayOfWeek.ToString();

        //        availableTimes.Items.Clear();
        //        availableTimes.Items.Insert(0, new ListItem("-Choose-", "0"));

        //        //Available times
        //        timeArray = sqlClass.GetAllTanTimes(tanDay, functionsClass.InternalCleanUp(bedPreference.SelectedValue), "W", false);

        //        //See what times are already taken
        //        timeArray = sqlClass.GetAvailableTanTimes(functionsClass.CleanUp(bedPreference.SelectedValue), appointmentDate.SelectedValue, timeArray);

        //        foreach (string i in timeArray)
        //        {
        //            if (Convert.ToDateTime(appointmentDate.SelectedValue) == DateTime.Now)
        //            {
        //                if (Convert.ToDateTime(i) > DateTime.Now)
        //                    availableTimes.Items.Add(new ListItem(i, i));
        //            }
        //            else
        //            {
        //                availableTimes.Items.Add(new ListItem(i, i));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sqlClass.LogErrorMessage(ex, "", "Site: AddAppointment: bedPref_SIC");
        //        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
        //        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
        //    }
        //}

        ///// <summary>
        ///// Bind Beds drop down based on Bed Type/Date
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void scheduleAppointment_onClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (availableTimes.SelectedValue == "0")
        //        {
        //            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
        //            errorLabel.Text = "Please select a tan time.";
        //        }
        //        else
        //        {
        //            //Schedule appointment
        //            string response = sqlClass.ScheduleAppointment(Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()),
        //                bedPreference.SelectedValue,
        //                functionsClass.FormatDash(Convert.ToDateTime(appointmentDate.SelectedValue)),
        //                availableTimes.SelectedValue, "W", false, emailReminder.Checked);

        //            if (response != HOTBAL.TansMessages.SUCCESS_MESSAGE)
        //            {
        //                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
        //                errorLabel.Text = response;
        //            }
        //            else
        //            {
        //                appointment.Style.Add("display", "none");
        //                confirmation.Style.Remove("display");

        //                //Return to Member Info
        //                //Response.Redirect("MemberInfo.aspx", false);
        //                //Display confirmation
        //                DateTime savedDate = Convert.ToDateTime(appointmentDate.SelectedValue + " " + availableTimes.SelectedValue).ToUniversalTime().AddMinutes(60);
        //                DateTime leaveDate = Convert.ToDateTime(appointmentDate.SelectedValue + " " + availableTimes.SelectedValue).ToUniversalTime().AddMinutes(90);
        //                string builtDate = String.Empty;
        //                confirmationNote.Text = "<b>Your Appointment has been Scheduled: <br />"
        //                    + "<b>Date:</b> " + functionsClass.FormatSlash(Convert.ToDateTime(appointmentDate.SelectedValue)) + "<br />"
        //                    + "<b>Time:</b> " + functionsClass.InternalCleanUp(availableTimes.SelectedValue) + "<br />"
        //                    + "<b>Bed:</b> " + functionsClass.InternalCleanUp(bedPreference.SelectedValue) + "<br />";

        //                builtDate = savedDate.ToString("yyyyMMddTHHmm00") + "Z/" + leaveDate.ToString("yyyyMMddTHHmm00") + "Z";

        //                addToCalendars.Text = "<a href='http://www.google.com/calendar/event?action=TEMPLATE&text=Tanning%20Appointment&dates=" + builtDate + "&location=HOT%20Tropical%20Tans,%20Waco,%20TX&trp=true&sprop=website:www.hottropicaltans.com&sprop=name:HOT%20Tropical%20Tans'><img src='http://www.google.com/calendar/images/ext/gc_button2.gif' alt='Remind Me' height='36' width='114' /></a>";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sqlClass.LogErrorMessage(ex, "", "Site: AddAppointment: schedAppt_onClick");
        //        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
        //        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
        //    }
        //}
    }
}