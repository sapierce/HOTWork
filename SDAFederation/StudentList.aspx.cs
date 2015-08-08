using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation
{
    public partial class StudentList : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (functionsClass.SchoolSelected())
            {
                List<Student> studentListBySchool = methodsClass.GetStudentsBySchoolID(functionsClass.SchoolID());

                if (studentListBySchool != null)
                {
                    foreach (Student student in studentListBySchool)
                    {
                        studentList.Text += "<a href='" + FederationConstants.STUDENT_INFORMATION_URL + "?ID=" + student.StudentId.ToString() + "'>" + student.FirstName + " " + student.LastName + "</a><br />";
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "&#149; " + FederationMessages.NO_STUDENTS_FOUND;
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.NOT_LOGGED_IN;
            }
        }
    }
}