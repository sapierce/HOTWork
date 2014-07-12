using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MobileSite
{
    public partial class AddAppointment : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["userID"] == null)
            {
                //User is logged out
                lblError.Text = HOTBAL.TansMessages.SESSION_EXPIRED_PUBLIC;
            }
            else
            {
                pnlAppointment.Style.Remove("display");
                pnlConfirmation.Style.Add("display", "none");
                if (!Page.IsPostBack)
                {
                    HOTBAL.Customer user = new HOTBAL.Customer();
                    user = sqlClass.GetCustomerInformationByID(Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()));

                    if (String.IsNullOrEmpty(user.Error))
                    {
                        functionsClass.buildAppointmentDatesList(ddlDate);
                        functionsClass.buildBedTypeList(ddlBed);

                        lblCustName.Text = user.FirstName + " " + user.LastName;

                        if (String.IsNullOrEmpty(user.Error))
                        {
                            if (user.VerifiedEmail)
                            {
                                chkEmailRemind.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        lblError.Text = user.Error;
                    }
                }
            }
        }

        /// <summary>
        /// Bind bed based on type, package check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBed_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<HOTBAL.Bed> bedListing = new List<HOTBAL.Bed>();
            bedListing = sqlClass.GetBedsByType(ddlBed.SelectedValue);
            lblError.Text = String.Empty;
            ddlPreference.Items.Clear();
            ddlPreference.Items.Add(new ListItem("-Choose-", "0"));

            if (bedListing != null)
            {
                foreach (HOTBAL.Bed x in bedListing)
                {
                    ddlPreference.Items.Add(new ListItem(x.BedLong, x.BedShort));
                }
            }
            else
            {
                //No active beds of that type
                ddlPreference.Items.Add(new ListItem("No beds available", "0"));
            }

            //Check the customer's package, alert if they select a bed not on their package
            string packageCheck = sqlClass.CheckCustomerPackage(Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()), 
                ddlBed.SelectedValue);

            if (packageCheck != HOTBAL.TansMessages.SUCCESS_MESSAGE)
            {
                lblError.Text = packageCheck;
            }
        }

        /// <summary>
        /// Bind Beds drop down based on Bed Type/Date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPreference_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList timeArray = new ArrayList();
            string tanDay = Convert.ToDateTime(ddlDate.SelectedValue).DayOfWeek.ToString();

            ddlTimes.Items.Clear();
            ddlTimes.Items.Insert(0, new ListItem("-Choose-", "0"));

            //Available times
            timeArray = sqlClass.GetAllTanTimes(tanDay, ddlPreference.SelectedValue, "W", false);

            //See what times are already taken
            timeArray = sqlClass.GetAvailableTanTimes(ddlPreference.SelectedValue, ddlDate.SelectedValue, timeArray);

            foreach (string i in timeArray)
            {
                if (Convert.ToDateTime(ddlDate.SelectedValue) == DateTime.Now)
                {
                    if (Convert.ToDateTime(i) > DateTime.Now)
                        ddlTimes.Items.Add(new ListItem(i, i));
                }
                else
                {
                    ddlTimes.Items.Add(new ListItem(i, i));
                }
            }
        }

        /// <summary>
        /// Bind Beds drop down based on Bed Type/Date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_onSubmit(object sender, EventArgs e)
        {
            if (ddlTimes.SelectedValue == "0")
            {
                lblError.Text = "Please select a tan time.";
            }
            else
            {
                //Schedule appointment
                string response = sqlClass.ScheduleAppointment(Convert.ToInt32(functionsClass.LightCleanUp(HttpContext.Current.Session["userID"].ToString())),
                    functionsClass.LightCleanUp(ddlPreference.SelectedValue),
                    functionsClass.FormatDash(Convert.ToDateTime(ddlDate.SelectedValue)),
                    functionsClass.LightCleanUp(ddlTimes.SelectedValue), "W", false, chkEmailRemind.Checked);
                if (response != HOTBAL.TansMessages.SUCCESS_MESSAGE)
                {
                    lblError.Text = response;
                }
                else
                {
                    pnlAppointment.Style.Add("display", "none");
                    pnlConfirmation.Style.Remove("display");

                    //Display confirmation
                    DateTime appointmentDate = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(ddlDate.SelectedValue + " " + ddlTimes.SelectedValue).AddMinutes(60));
                    DateTime leaveDate = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(ddlDate.SelectedValue + " " + ddlTimes.SelectedValue).AddMinutes(90));
                    string builtDate = String.Empty;
                    lblConfirmation.Text = "<b>Your Appointment has been Scheduled: <br />"
                            + "<b>Date:</b> " + functionsClass.FormatSlash(Convert.ToDateTime(ddlDate.SelectedValue)) + "<br />"
                            + "<b>Time:</b> " + functionsClass.LightCleanUp(ddlTimes.SelectedValue) + "<br />"
                            + "<b>Bed:</b> " + functionsClass.LightCleanUp(ddlPreference.SelectedValue) + "<br />";

                    builtDate = appointmentDate.ToString("yyyyMMddTHHmm00") + "Z/" + leaveDate.ToString("yyyyMMddTHHmm00") + "Z";
                        
                    lblAddToCalendars.Text = "<a href='http://www.google.com/calendar/event?action=TEMPLATE&text=Tanning%20Appointment&dates=" + builtDate + "&location=HOT%20Tropical%20Tans,%20Waco,%20TX&trp=true&sprop=website:www.hottropicaltans.com&sprop=name:HOT%20Tropical%20Tans'><img src='http://www.google.com/calendar/images/ext/gc_button2.gif' alt='' height='36' width='114' /></a><br />";
                    //lblAddToCalendars.Text += "<asp:Button ID='saveToOutlook' runat='server' onClick='btnAddEvent_Click' Text='Add to Outlook' /><br />";
                }
            }
        }

        protected void btnAddEvent_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sbICSFile =
                new System.Text.StringBuilder();
            DateTime dtNow = DateTime.Now;
            string timezone = "US/Central";
            DateTime appointmentDate = Convert.ToDateTime(ddlDate.SelectedValue + " " + ddlTimes.SelectedValue).ToUniversalTime();
            DateTime leaveDate = Convert.ToDateTime(ddlDate.SelectedValue + " " + ddlTimes.SelectedValue).ToUniversalTime().AddMinutes(30);
                    
            sbICSFile.AppendLine("BEGIN:VCALENDAR");
            sbICSFile.AppendLine("VERSION:2.0");
            sbICSFile.AppendLine("PRODID:-//ICSTestCS/");
            sbICSFile.AppendLine("CALSCALE:GREGORIAN");

            // Define time zones.
            // US/Eastern
            sbICSFile.AppendLine("BEGIN:VTIMEZONE");
            sbICSFile.AppendLine("TZID:US/Eastern");
            sbICSFile.AppendLine("BEGIN:STANDARD");
            sbICSFile.AppendLine("DTSTART:20071104T020000");
            sbICSFile.AppendLine("RRULE:FREQ=YEARLY;BYDAY=1SU;BYMONTH=11");
            sbICSFile.AppendLine("TZOFFSETFROM:-0400");
            sbICSFile.AppendLine("TZOFFSETTO:-0500");
            sbICSFile.AppendLine("TZNAME:EST");
            sbICSFile.AppendLine("END:STANDARD");
            sbICSFile.AppendLine("BEGIN:DAYLIGHT");
            sbICSFile.AppendLine("DTSTART:20070311T020000");
            sbICSFile.AppendLine("RRULE:FREQ=YEARLY;BYDAY=2SU;BYMONTH=3");
            sbICSFile.AppendLine("TZOFFSETFROM:-0500");
            sbICSFile.AppendLine("TZOFFSETTO:-0400");
            sbICSFile.AppendLine("TZNAME:EDT");
            sbICSFile.AppendLine("END:DAYLIGHT");
            sbICSFile.AppendLine("END:VTIMEZONE");

            // US/Central
            sbICSFile.AppendLine("BEGIN:VTIMEZONE");
            sbICSFile.AppendLine("TZID:US/Central");
            sbICSFile.AppendLine("BEGIN:STANDARD");
            sbICSFile.AppendLine("DTSTART:20071104T020000");
            sbICSFile.AppendLine("RRULE:FREQ=YEARLY;BYDAY=1SU;BYMONTH=11");
            sbICSFile.AppendLine("TZOFFSETFROM:-0500");
            sbICSFile.AppendLine("TZOFFSETTO:-0600");
            sbICSFile.AppendLine("TZNAME:CST");
            sbICSFile.AppendLine("END:STANDARD");
            sbICSFile.AppendLine("BEGIN:DAYLIGHT");
            sbICSFile.AppendLine("DTSTART:20070311T020000");
            sbICSFile.AppendLine("RRULE:FREQ=YEARLY;BYDAY=2SU;BYMONTH=3");
            sbICSFile.AppendLine("TZOFFSETFROM:-0600");
            sbICSFile.AppendLine("TZOFFSETTO:-0500");
            sbICSFile.AppendLine("TZNAME:CDT");
            sbICSFile.AppendLine("END:DAYLIGHT");
            sbICSFile.AppendLine("END:VTIMEZONE");

            // US/Mountain
            sbICSFile.AppendLine("BEGIN:VTIMEZONE");
            sbICSFile.AppendLine("TZID:US/Mountain");
            sbICSFile.AppendLine("BEGIN:STANDARD");
            sbICSFile.AppendLine("DTSTART:20071104T020000");
            sbICSFile.AppendLine("RRULE:FREQ=YEARLY;BYDAY=1SU;BYMONTH=11");
            sbICSFile.AppendLine("TZOFFSETFROM:-0600");
            sbICSFile.AppendLine("TZOFFSETTO:-0700");
            sbICSFile.AppendLine("TZNAME:MST");
            sbICSFile.AppendLine("END:STANDARD");
            sbICSFile.AppendLine("BEGIN:DAYLIGHT");
            sbICSFile.AppendLine("DTSTART:20070311T020000");
            sbICSFile.AppendLine("RRULE:FREQ=YEARLY;BYDAY=2SU;BYMONTH=3");
            sbICSFile.AppendLine("TZOFFSETFROM:-0700");
            sbICSFile.AppendLine("TZOFFSETTO:-0600");
            sbICSFile.AppendLine("TZNAME:MDT");
            sbICSFile.AppendLine("END:DAYLIGHT");
            sbICSFile.AppendLine("END:VTIMEZONE");

            // US/Pacific
            sbICSFile.AppendLine("BEGIN:VTIMEZONE");
            sbICSFile.AppendLine("TZID:US/Pacific");
            sbICSFile.AppendLine("BEGIN:STANDARD");
            sbICSFile.AppendLine("DTSTART:20071104T020000");
            sbICSFile.AppendLine("RRULE:FREQ=YEARLY;BYDAY=1SU;BYMONTH=11");
            sbICSFile.AppendLine("TZOFFSETFROM:-0700");
            sbICSFile.AppendLine("TZOFFSETTO:-0800");
            sbICSFile.AppendLine("TZNAME:PST");
            sbICSFile.AppendLine("END:STANDARD");
            sbICSFile.AppendLine("BEGIN:DAYLIGHT");
            sbICSFile.AppendLine("DTSTART:20070311T020000");
            sbICSFile.AppendLine("RRULE:FREQ=YEARLY;BYDAY=2SU;BYMONTH=3");
            sbICSFile.AppendLine("TZOFFSETFROM:-0800");
            sbICSFile.AppendLine("TZOFFSETTO:-0700");
            sbICSFile.AppendLine("TZNAME:PDT");
            sbICSFile.AppendLine("END:DAYLIGHT");
            sbICSFile.AppendLine("END:VTIMEZONE");

            // Define the event.
            sbICSFile.AppendLine("BEGIN:VEVENT");

            sbICSFile.Append("DTSTART;TZID=" + timezone + ":");
            sbICSFile.Append(appointmentDate.ToString("yyyyMMddTHHmm00"));

            sbICSFile.Append("DTEND;TZID=" + timezone + ":");
            sbICSFile.Append(leaveDate.ToString("yyyyMMddTHHmm00"));

            sbICSFile.AppendLine("SUMMARY:Tanning Appointment at HOT Tropical Tans");
            sbICSFile.AppendLine("DESCRIPTION:Tanning Apointment");
            sbICSFile.AppendLine("UID:1");
            sbICSFile.AppendLine("SEQUENCE:0");

            sbICSFile.Append("DTSTAMP:" + dtNow.Year.ToString());
            sbICSFile.Append(FormatDateTimeValue(dtNow.Month));
            sbICSFile.Append(FormatDateTimeValue(dtNow.Day) + "T");
            sbICSFile.Append(FormatDateTimeValue(dtNow.Hour));
            sbICSFile.AppendLine(FormatDateTimeValue(dtNow.Minute) + "00");

            sbICSFile.AppendLine("END:VEVENT");
            sbICSFile.AppendLine("END:VCALENDAR");

            Response.ContentType = "text/calendar";
            Response.AddHeader("content-disposition",
                "attachment; filename=CalendarEvent1.ics");
            Response.Write(sbICSFile);
            Response.End();
        }

        private string FormatDateTimeValue(int DateValue)
        {
            if (DateValue < 10)
                return "0" + DateValue.ToString();
            else
                return DateValue.ToString();
        }
    }
}
