using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace HOTTropicalTans
{
    public partial class EmployeeHours : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Employee Hours";
            List<HOTBAL.Employee> employeeInformation = sqlClass.GetEmployeeByID(Convert.ToInt32(Request.QueryString["ID"]));

            if (employeeInformation != null)
            {
                employeeName.Text = employeeInformation[0].FirstName + " " + employeeInformation[0].LastName;
            }

            if (String.IsNullOrEmpty(Request.QueryString["SID"]))
            {
                shiftDate.Text = DateTime.Now.ToShortDateString();
                shiftStartTime.Text = DateTime.Now.ToString();
                shiftEndTime.Text = DateTime.Now.ToString();
            }
            else
            {
                DataTable shiftInformation = sqlClass.GetEmployeeShift(Convert.ToInt32(Request.QueryString["SID"]));
                shiftDate.Text = shiftInformation.Rows[0]["SHFT_DATE"].ToString();
                shiftStartTime.Text = shiftInformation.Rows[0]["SHFT_BEG_HOUR"].ToString();
                shiftEndTime.Text = shiftInformation.Rows[0]["SHFT_END_HOUR"].ToString();
            }
        }

        protected void editHours_onClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Request.QueryString["SID"]))
            {
                sqlClass.AddEmployeeHours(Convert.ToInt32(Request.QueryString["ID"]),
                    functionsClass.InternalCleanUp(shiftDate.Text),
                    Convert.ToDateTime(functionsClass.InternalCleanUp(shiftStartTime.Text)),
                    Convert.ToDateTime(functionsClass.InternalCleanUp(shiftEndTime.Text)));
            }
            else
            {
                sqlClass.EditEmployeeHours(Convert.ToInt32(Request.QueryString["SID"]),
                    Convert.ToDateTime(functionsClass.InternalCleanUp(shiftStartTime.Text)),
                    Convert.ToDateTime(functionsClass.InternalCleanUp(shiftEndTime.Text)));
            }
        }
    }
}