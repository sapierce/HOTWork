using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class EmployeeInformation : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();
        bool isAdministrator = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Employee Information";

            if (String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                if (Request.QueryString["Admin"] != null)
                {
                    if (Request.QueryString["Admin"] == "Yes")
                        if (!functionsClass.isAdmin())
                            Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL); 
                        else
                            isAdministrator = true;
                }

                if (!Page.IsPostBack)
                {
                    List<HOTBAL.Employee> employeeInformation = sqlClass.GetEmployeeByID(Convert.ToInt32(Request.QueryString["ID"]));

                    if (employeeInformation != null)
                    {
                        employeeName.Text = employeeInformation[0].FirstName + " " + employeeInformation[0].LastName;
                        EmployeeClockDisplay(Convert.ToInt32(Request.QueryString["ID"]));
                        EmployeeScheduledHours(Convert.ToInt32(Request.QueryString["ID"]), "Current", "Current", isAdministrator);
                        EmployeeWorkedHours(Convert.ToInt32(Request.QueryString["ID"]), "Current", "Current", isAdministrator);
                        EmployeeNotes(Convert.ToInt32(Request.QueryString["ID"]));
                        EmployeeSales(Convert.ToInt32(Request.QueryString["ID"]), "Current", "Current");

                        if (isAdministrator)
                            addHours.Visible = true;
                    }
                    else
                    {
                        errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                    BuildTimeLists();
                }
            }
        }

        protected void BuildTimeLists()
        {
            try
            {
                DateTime ddlEnd = DateTime.Now.AddMonths(1);
                DateTime begDate = DateTime.Now.AddMonths(-2);
                DateTime endDate = DateTime.MaxValue;

                if (Convert.ToDateTime(begDate).DayOfWeek == DayOfWeek.Monday)
                {
                    begDate = begDate.AddDays(7);
                    endDate = begDate.AddDays(6);
                }
                else if (Convert.ToDateTime(begDate).DayOfWeek == DayOfWeek.Tuesday)
                {
                    begDate = begDate.AddDays(6);
                    endDate = begDate.AddDays(6);
                }
                else if (Convert.ToDateTime(begDate).DayOfWeek == DayOfWeek.Wednesday)
                {
                    begDate = begDate.AddDays(5);
                    endDate = begDate.AddDays(6);
                }
                else if (Convert.ToDateTime(begDate).DayOfWeek == DayOfWeek.Thursday)
                {
                    begDate = begDate.AddDays(4);
                    endDate = begDate.AddDays(6);
                }
                else if (Convert.ToDateTime(begDate).DayOfWeek == DayOfWeek.Friday)
                {
                    begDate = begDate.AddDays(3);
                    endDate = begDate.AddDays(6);
                }
                else if (Convert.ToDateTime(begDate).DayOfWeek == DayOfWeek.Saturday)
                {
                    begDate = begDate.AddDays(2);
                    endDate = begDate.AddDays(6);
                }
                else if (Convert.ToDateTime(begDate).DayOfWeek == DayOfWeek.Sunday)
                {
                    begDate = begDate.AddDays(1);
                    endDate = begDate.AddDays(6);
                }

                workedDateRange.Items.Add(new ListItem(functionsClass.FormatSlash(begDate) + "-" + functionsClass.FormatSlash(endDate)));
                scheduledDateRange.Items.Add(new ListItem(functionsClass.FormatSlash(begDate) + "-" + functionsClass.FormatSlash(endDate)));
                salesDateRange.Items.Add(new ListItem(functionsClass.FormatSlash(begDate) + "-" + functionsClass.FormatSlash(endDate)));
                
                while (endDate <= ddlEnd)
                {
                    begDate = endDate.AddDays(1);
                    endDate = endDate.AddDays(7);
                    workedDateRange.Items.Add(new ListItem(functionsClass.FormatSlash(begDate) + "-" + functionsClass.FormatSlash(endDate)));
                    scheduledDateRange.Items.Add(new ListItem(functionsClass.FormatSlash(begDate) + "-" + functionsClass.FormatSlash(endDate)));
                    salesDateRange.Items.Add(new ListItem(functionsClass.FormatSlash(begDate) + "-" + functionsClass.FormatSlash(endDate)));
                }
            }
            catch(Exception ex)
            {
                errorMessage.Text = ex.Message;
            }
        }

        protected void EmployeeClockDisplay(int employeeNumber)
        {
            int isClockedIn = sqlClass.EmployeeShiftCheck(employeeNumber);
            employeeClockOut.Attributes["ShiftId"] = isClockedIn.ToString();
            
            if (isClockedIn > 0)
            {
                employeeClockIn.Visible = false;
                employeeClockOut.Visible = true;
            }
            else
            {
                employeeClockIn.Visible = true;
                employeeClockOut.Visible = false;
            }
        }

        protected void EmployeeScheduledHours(int employeeNumber, string scheduleStartDate, string scheduleEndDate, bool isAdmin)
        {
            DataTable scheduledHoursTable;

            if (scheduleStartDate == "Current")
            {
                scheduledRangeText.Text = functionsClass.FormatSlash(DateTime.Now.AddDays(-7)) + "-" + functionsClass.FormatSlash(DateTime.Now.AddDays(7));
                scheduledHoursTable = sqlClass.EmployeeSchedule(employeeNumber, functionsClass.FormatDash(DateTime.Now.AddDays(-7)), functionsClass.FormatDash(DateTime.Now.AddDays(7)));
            }
            else
            {
                scheduledRangeText.Text = functionsClass.FormatSlash(Convert.ToDateTime(scheduleStartDate)) + "-" + functionsClass.FormatSlash(Convert.ToDateTime(scheduleEndDate));
                scheduledHoursTable = sqlClass.EmployeeSchedule(employeeNumber, scheduleStartDate, scheduleEndDate);
            }
            
            if (scheduledHoursTable.Rows.Count > 0)
            {
                foreach (DataRow row in scheduledHoursTable.Rows)
                {
                    scheduledHours.Text += "<tr><td>" + functionsClass.FormatSlash(Convert.ToDateTime(row["SCHD_DATE"])) + "</td><td>" + row["BEG_TIME"] + "</td><td>" + row["END_TIME"] + "</td><td>" + row["SCHD_LOC"] + "</td>";
                    if (isAdmin)
                    {
                        scheduledHours.Text += "<td><a href='../admin/reports/empl_schd_edit.aspx?ID=" + row["SCHD_ID"] + "&UID=" + employeeNumber.ToString() + "'>Edit</a></td></tr>";
                    }
                    else
                    {
                        scheduledHours.Text += "<td><br /></td></tr>";
                    }
                }
            }
            else
            {
                scheduledHours.Text += "<tr><td colspan='5'>No hours scheduled</td></tr>";
            }
        }

        protected void EmployeeWorkedHours(int employeeNumber, string scheduleStartDate, string scheduleEndDate, bool isAdmin)
        {
            DataTable workedHoursTable;

            if (scheduleStartDate == "Current")
            {
                workedRangeText.Text = functionsClass.FormatSlash(DateTime.Now.AddDays(-7)) + "-" + functionsClass.FormatSlash(DateTime.Now.AddDays(7));
                workedHoursTable = sqlClass.EmployeeWorked(employeeNumber, functionsClass.FormatDash(DateTime.Now.AddDays(-7)), functionsClass.FormatDash(DateTime.Now.AddDays(7)));
            }
            else
            {
                workedRangeText.Text = functionsClass.FormatSlash(Convert.ToDateTime(scheduleStartDate)) + "-" + functionsClass.FormatSlash(Convert.ToDateTime(scheduleEndDate));
                workedHoursTable = sqlClass.EmployeeWorked(employeeNumber, scheduleStartDate, scheduleEndDate);
            }

            if (workedHoursTable.Rows.Count > 0)
            {
                foreach (DataRow row in workedHoursTable.Rows)
                {
                    workedHours.Text += "<tr><td>" + functionsClass.FormatSlash(Convert.ToDateTime(row["SHFT_DATE"].ToString())) +
                        "</td><td>" + row["SHFT_START_HOUR"].ToString() + "</td><td>" + row["SHFT_END_HOUR"].ToString() + "</td>";

                    TimeSpan curHours;
                    if (row["SHFT_END_HOUR"].ToString() == "00:00:00")
                        curHours = (DateTime.Now - Convert.ToDateTime(row["SHFT_START_HOUR"].ToString()));
                    else
                        curHours = (Convert.ToDateTime(row["SHFT_END_HOUR"].ToString()) - Convert.ToDateTime(row["SHFT_START_HOUR"].ToString()));

                    workedHours.Text += "<td>" + curHours.ToString() + "</td>";
                    if (isAdmin)
                        workedHours.Text += "<td><a href='EmployeeTimeEdit.aspx?ID=" + row["SHFT_ID"] + "&UID=" + Request.QueryString["ID"] + "'>Edit</a></td></tr>";
                    else
                        workedHours.Text += "<td><br /></td></tr>";
                }
            }
            else
                workedHours.Text += "<tr><td colspan='4'>No hours worked</td></tr>";
        }

        protected void EmployeeSales(int employeeNumber, string salesStartDate, string salesEndDate)
        {
            DataTable employeeSalesTable;

            if (salesStartDate == "Current")
            {
                salesRangeText.Text = functionsClass.FormatSlash(DateTime.Now.AddDays(-7)) + "-" + functionsClass.FormatSlash(DateTime.Now.AddDays(7));
                employeeSalesTable = sqlClass.EmployeeSales(employeeNumber, functionsClass.FormatDash(DateTime.Now.AddDays(-7)), functionsClass.FormatDash(DateTime.Now));
            }
            else
            {
                salesRangeText.Text = functionsClass.FormatSlash(Convert.ToDateTime(salesStartDate)) + "-" + functionsClass.FormatSlash(Convert.ToDateTime(salesEndDate));
                employeeSalesTable = sqlClass.EmployeeSales(employeeNumber, salesStartDate, salesEndDate);
            }

            if (employeeSalesTable.Rows.Count > 0)
            {
                foreach (DataRow row in employeeSalesTable.Rows)
                {
                    employeeSales.Text += "<tr><td>" + functionsClass.FormatSlash(Convert.ToDateTime(row["TRNS_DATE"])) + "</td><td>" +
                        String.Format("{0:C}", Convert.ToDouble(row["TotalSales"].ToString())) + "</td>";
                }
            }
            else
                employeeSales.Text += "<tr><td colspan='2'>No sales found</td></tr>";
        }

        protected void EmployeeNotes(int employeeNumber)
        {
            List<HOTBAL.EmployeeNote> notesReturn = sqlClass.GetNotesToEmployees();

            if (notesReturn != null)
            {
                foreach (HOTBAL.EmployeeNote note in notesReturn)
                    {
                        List<HOTBAL.Employee> toEmployee = sqlClass.GetEmployeeByID(Convert.ToInt32(note.NoteTo));
                        List<HOTBAL.Employee> fromEmployee = sqlClass.GetEmployeeByID(Convert.ToInt32(note.NoteFrom));

                        if ((toEmployee != null) && (fromEmployee != null))
                            employeeNotes.Text += "<tr><td>" + toEmployee[0].FirstName + " " + toEmployee[0].LastName +
                                "</td><td>" + fromEmployee[0].FirstName + " " + fromEmployee[0].LastName + "</td><td>" +
                                note.NoteText + "</td></tr>";
                        else
                        {
                            employeeNotes.Text += "<tr><td colspan=3'>No notes</td></tr>";
                        }
                    }
            }
            else
            {
                employeeNotes.Text += "<tr><td colspan=3'>No notes</td></tr>";
            }
        }

        protected void employeeClockIn_Click(object sender, EventArgs e)
        {
            sqlClass.EmployeeClockIn(Convert.ToInt32(Request.QueryString["ID"].ToString()), functionsClass.FormatDash(DateTime.Now));

            EmployeeWorkedHours(Convert.ToInt32(Request.QueryString["ID"].ToString()), "Current", "Current", (Request.QueryString["Admin"] == "Yes" ? true : false));
            EmployeeClockDisplay(Convert.ToInt32(Request.QueryString["ID"].ToString()));
        }

        protected void employeeClockOut_Click(object sender, EventArgs e)
        {
            sqlClass.EmployeeClockOut(Convert.ToInt32(employeeClockOut.Attributes["ShiftId"]));

            EmployeeWorkedHours(Convert.ToInt32(Request.QueryString["ID"].ToString()), "Current", "Current", (Request.QueryString["Admin"] == "Yes" ? true : false));
            EmployeeClockDisplay(Convert.ToInt32(Request.QueryString["ID"].ToString()));
        }

        protected void addHours_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeHours.aspx?ID=" + functionsClass.CleanUp(Request.QueryString["ID"]), false);
        }

        protected void productCounts_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeProductCounts.aspx?ID=" + functionsClass.CleanUp(Request.QueryString["ID"]), false);
        }

        protected void addNote_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.EMP_NOTES_INTERNAL_URL + "?ID=" + functionsClass.CleanUp(Request.QueryString["ID"]), false);
        }

        protected void changePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeePassword.aspx?status=change&ID=" + functionsClass.CleanUp(Request.QueryString["ID"]), false);
        }

        protected void workedDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] workedDates = workedDateRange.SelectedValue.Split(Convert.ToChar("-"));
            workedHours.Text = String.Empty;
            EmployeeWorkedHours(Convert.ToInt32(Request.QueryString["ID"]), 
                functionsClass.FormatDash(Convert.ToDateTime(workedDates[0])), 
                functionsClass.FormatDash(Convert.ToDateTime(workedDates[1])), isAdministrator);
        }

        protected void scheduledDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] scheduledDates = scheduledDateRange.SelectedValue.Split(Convert.ToChar("-"));
            scheduledHours.Text = String.Empty;
            EmployeeScheduledHours(Convert.ToInt32(Request.QueryString["ID"]),
                functionsClass.FormatDash(Convert.ToDateTime(scheduledDates[0])),
                functionsClass.FormatDash(Convert.ToDateTime(scheduledDates[1])), isAdministrator);
        }

        protected void salesDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] salesDates = salesDateRange.SelectedValue.Split(Convert.ToChar("-"));
            employeeSales.Text = String.Empty;
            EmployeeSales(Convert.ToInt32(Request.QueryString["ID"]),
                functionsClass.FormatDash(Convert.ToDateTime(salesDates[0])),
                functionsClass.FormatDash(Convert.ToDateTime(salesDates[1])));
        }
    }
}