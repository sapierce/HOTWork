using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                employeeNoteList.Text = "<table align='center' class='tanning' width='500'>";
                List<HOTBAL.EmployeeNote> employeeNotes;
                if (functionsClass.CleanUp(Request.QueryString["From"]) == "F")
                {
                    employeeNotes = sqlClass.GetNotesFromEmployees();
                    employeeNoteList.Text += "<thead><tr><th colspan='5'>Notes From Employees</th></tr></thead>";
                    addNote.Visible = false;
                }
                else
                {
                    employeeNotes = sqlClass.GetNotesToEmployees();
                    employeeNoteList.Text += "<thead><tr><th colspan='5'>Notes To Employees</th></tr></thead>";
                    addNote.Visible = true;
                }

                employeeNoteList.Text += "<tr><td class='centerAlignHeader'>Date</td>" + 
                    "<td class='centerAlignHeader'>To</td>" + 
                    "<td class='centerAlignHeader'>From</td>" +
                    "<td class='centerAlignHeader'>Note</td>" + 
                    "<td class='centerAlignHeader'><br /></td></tr>";

                if (employeeNotes != null)
                {
                    foreach (HOTBAL.EmployeeNote n in employeeNotes)
                    {
                        List<HOTBAL.Employee> employeeTo = sqlClass.GetEmployeeByID(n.NoteTo);
                        List<HOTBAL.Employee> employeeFrom = sqlClass.GetEmployeeByID(n.NoteFrom);

                        employeeNoteList.Text += "<tr><td style='vertical-align: top;'>" +
                            functionsClass.FormatSlash(n.NoteTime) +
                            "</td><td style='vertical-align: top;'>" +
                            employeeTo[0].FirstName + " " + employeeTo[0].LastName +
                            "</td><td style='vertical-align: top;'>" +
                            employeeFrom[0].FirstName + " " + employeeFrom[0].LastName +
                            "</td><td style='vertical-align: top;'>" +
                            n.NoteText +
                            "</td><td style='vertical-align: top;'><a href='" + HOTBAL.TansConstants.ADMIN_DEL_EMP_NOTE_URL + "?ID=" +
                            n.NoteID + "'>Delete</a></td></tr>";
                    }
                }
                else
                {
                    employeeNoteList.Text += "<tr><td colspan=4'>No notes</td></tr>";
                }
                employeeNoteList.Text += "</table>";
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                sqlClass.LogErrorMessage(ex, "", "EmployeeNotes: PageLoad");
            }
        }

        protected void addNote_Click(object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.TansConstants.ADMIN_ADD_EMP_NOTE_URL);
        }
    }
}