using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class EmployeeClockedHours : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Employee Clocked Hours";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);

            if (Page.IsPostBack)
            {
                string[] dateRangeSplit = dateRanges.SelectedValue.Split(Convert.ToChar("-"));
                buildEmployeeHours(Convert.ToDateTime(dateRangeSplit[0]), Convert.ToDateTime(dateRangeSplit[1]));
            }
            else
            {
                buildTimeLists();
                buildEmployeeHours(DateTime.Now.AddDays(-7), DateTime.Now);
            }
        }

        private void buildTimeLists()
        {
            try
            {
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

                dateRanges.Items.Add(new ListItem(functionsClass.FormatSlash(begDate) + "-" + functionsClass.FormatSlash(endDate)));

                while (endDate <= DateTime.Now)
                {
                    begDate = endDate.AddDays(1);
                    endDate = endDate.AddDays(7);
                    dateRanges.Items.Add(new ListItem(functionsClass.FormatSlash(begDate) + "-" + functionsClass.FormatSlash(endDate)));
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = ex.Message;
            }
        }

        private void buildEmployeeHours(DateTime beginDate, DateTime endDate)
        {
            hoursList.Text = String.Empty;
            selectedDates.Text = "Employee Hours and Sales for " + functionsClass.FormatSlash(beginDate) + " - " + functionsClass.FormatSlash(endDate);
            
            // Get all employees
            List<HOTBAL.Employee> employees = sqlClass.GetAllEmployees();

            if (employees != null)
            {
                if (employees.Count > 0)
                {
                    foreach (HOTBAL.Employee employee in employees)
                    {
                        // Get the employee hours
                        DataTable employeeHours = sqlClass.EmployeeWorked(employee.EmployeeID, functionsClass.FormatDash(beginDate), functionsClass.FormatDash(endDate));
                        TimeSpan totalHours = new TimeSpan();

                        if (employeeHours != null)
                        {
                            if (employeeHours.Rows.Count > 0)
                            {
                                foreach (DataRow row in employeeHours.Rows)
                                {
                                    TimeSpan currentHours;
                                    if (row["SHFT_END_HOUR"].ToString() == "00:00:00")
                                        currentHours = (DateTime.Now - Convert.ToDateTime(row["SHFT_START_HOUR"].ToString()));
                                    else
                                        currentHours = (Convert.ToDateTime(row["SHFT_END_HOUR"].ToString()) - Convert.ToDateTime(row["SHFT_START_HOUR"].ToString()));

                                    totalHours += currentHours;
                                }
                            }
                        }

                        DataTable employeeSales = sqlClass.EmployeeSales(employee.EmployeeID, functionsClass.FormatDash(beginDate), functionsClass.FormatDash(endDate));
                        double totalSales = 0.00;

                        if (employeeSales != null)
                        {
                            if (employeeSales.Rows.Count > 0)
                            {
                                foreach (DataRow row in employeeSales.Rows)
                                {
                                    totalSales = totalSales + Convert.ToDouble(row["TotalSales"].ToString());
                                }
                            }
                        }

                        hoursList.Text += "<tr><td><a href='" + HOTBAL.TansConstants.EMP_INFO_INTERNAL_URL + "?ID=" + employee.EmployeeID + "&Admin=Yes'>" + 
                            employee.LastName + ", " + employee.FirstName + "</a></td><td>" + totalHours.ToString() + "</td><td>" + 
                            String.Format("{0:C}", totalSales) + "</td></tr>";
                    }
                }
            }
        }
    }
}