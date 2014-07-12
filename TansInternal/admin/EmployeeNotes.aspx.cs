using System;
using System.Collections.Generic;
using System.Web.UI;

namespace HOTTropicalTans
{
    public partial class EmployeeNotes1 : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Employee Notes";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);

            try
            {
                List<HOTBAL.EmployeeNote> employeeNotes;
                if (functionsClass.CleanUp(Request.QueryString["From"]) == "F")
                {
                    employeeNotes = sqlClass.GetNotesToEmployees();
                }
                else
                {
                    employeeNotes = sqlClass.GetNotesFromEmployees();
                }

                employeeNoteList.Text = "<table align='center' class='standardTable' width='500'>";
                employeeNoteList.Text += "<tr><td class='standardHeader'>Date</td><td class='standardHeader'>To</td><td class='standardHeader'>From</td><td class='standardHeader'>Note</td><td class='standardHeader'><br /></td></tr>";

                if (employeeNotes != null)
                {
                    foreach (HOTBAL.EmployeeNote n in employeeNotes)
                    {
                        List<HOTBAL.Employee> employeeTo = sqlClass.GetEmployeeByID(n.NoteTo);
                        List<HOTBAL.Employee> employeeFrom = sqlClass.GetEmployeeByID(n.NoteFrom);

                        employeeNoteList.Text = "<tr><td class='standardField' valign='top'>" +
                            functionsClass.FormatSlash(n.NoteTime) +
                            "</td><td class='standardField' valign='top'>" +
                            employeeTo[0].FirstName + " " + employeeTo[0].LastName +
                            "</td><td class='standardField' valign='top'>" +
                            employeeFrom[0].FirstName + " " + employeeFrom[0].LastName +
                            "</td><td class='standardField' valign='top'>" +
                            n.NoteText +
                            "</td><td class='standardField' valign='top'><a href='EmployeeNotesDelete.aspx?ID=" +
                            n.NoteID + "'>Delete</a></td></tr>";
                    }
                }
                else
                {
                    employeeNoteList.Text = "<tr><td colspan=4'>No notes</td></tr>";
                }
                employeeNoteList.Text += "</table>";
            }
            catch (Exception ex)
            {
                errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                sqlClass.LogErrorMessage(ex, "", "EmployeeEdit: resetPassword_OnClick");
            }
        }
    }
}