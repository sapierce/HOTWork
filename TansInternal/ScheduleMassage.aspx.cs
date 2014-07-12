using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class ScheduleMassage : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Massage Schedule";

            if (String.IsNullOrEmpty(enterDate.Text))
            {
                massageScheduleOutput.Text = WacoMassageSchedule(functionsClass.FormatDash(DateTime.Now));
                Page.Title += " for " + functionsClass.FormatSlash(DateTime.Now);
            }
            else
            {
                massageScheduleOutput.Text = WacoMassageSchedule(functionsClass.FormatDash(Convert.ToDateTime(enterDate.Text)));
                Page.Title += " for " + functionsClass.FormatSlash(Convert.ToDateTime(enterDate.Text));
            }
        }

        protected string WacoMassageSchedule(string scheduleDate)
        {
            string scheduleTable = String.Empty;
            List<HOTBAL.Time> scheduleTimes = sqlClass.GetLocationTimes("W", "M");
            string dayOfDate = System.Convert.ToDateTime(scheduleDate).ToString("ddd");
            if ((scheduleTimes != null) && (!String.IsNullOrEmpty(scheduleDate)) && (!String.IsNullOrEmpty(dayOfDate.Trim())))
            {
                scheduleTable = scheduleTable + "<tr>";
                DateTime beginTime = DateTime.MinValue;
                DateTime endTime = DateTime.MaxValue;

                foreach (HOTBAL.Time t in scheduleTimes)
                {
                    if (t != null)
                    {
                        if (t.TimeDay == dayOfDate.Substring(0, 3).ToUpper())
                        {
                            beginTime = Convert.ToDateTime(t.BeginTime);
                            endTime = Convert.ToDateTime(t.EndTime);
                        }
                    }
                }

                DateTime currentTime = beginTime;

                while (currentTime < endTime)
                {
                    scheduleTable += "<td>" + currentTime.ToShortTimeString() + "</td>";
                    currentTime = currentTime.AddMinutes(30);
                }
                scheduleTable = scheduleTable + "</tr>";
                currentTime = beginTime;

                scheduleTable = scheduleTable + "<tr>";
                while (currentTime < endTime)
                {
                    HOTBAL.Massage massageData = sqlClass.GetMassageByData(scheduleDate, currentTime.ToShortTimeString());

                    if ((massageData != null) && (massageData.ID != 0))
                    {
                        if (massageData.Length == 60)
                        {
                            scheduleTable += "<td colspan='2'>";
                            currentTime = currentTime.AddMinutes(60);
                        }
                        else
                        {
                            scheduleTable += "<td>";
                            currentTime = currentTime.AddMinutes(30);
                        }

                        HOTBAL.Customer customerData = sqlClass.GetCustomerInformationByID(massageData.UserID);
                        if (String.IsNullOrEmpty(customerData.Error))
                        {
                            scheduleTable += "<a href='" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + massageData.UserID + "'>"
                                + customerData.LastName + ", " + customerData.FirstName.Substring(0, 1) + "</a>";
                        }
                        else
                        {
                            scheduleTable += "<a href='" + HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + massageData.UserID + "'>Unknown</a>";
                        }
                        scheduleTable += "</td>";
                    }
                    else
                    {
                        // Time is available to schedule
                        scheduleTable += "<td><a href='" + HOTBAL.TansConstants.ADD_APPT_MASSAGE_INTERNAL_URL
                                + "?Date=" + scheduleDate + "&Time=" + currentTime.ToShortTimeString() + "'>-----</a></td>";
                        currentTime = currentTime.AddMinutes(30);
                    }
                }

                scheduleTable = scheduleTable + "</tr>";
                currentTime = beginTime;
            }
            return scheduleTable;
        }

        protected void changeDate_Click(object sender, EventArgs e)
        {
            Label currentDate = (Label)Master.FindControl("currentDate");
            currentDate.Text = functionsClass.FormatSlash(Convert.ToDateTime(enterDate.Text));
            massageScheduleOutput.Text = WacoMassageSchedule(functionsClass.FormatDash(Convert.ToDateTime(enterDate.Text)));
        }
    }
}