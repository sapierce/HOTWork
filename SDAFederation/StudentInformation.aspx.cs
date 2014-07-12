using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation
{
    public partial class StudentInformation : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (functionsClass.SchoolSelected())
            {
                Student studentInformation = methodsClass.GetStudentInformation(Convert.ToInt32(Request.QueryString["ID"]));

                if (studentInformation != null)
                {
                    if (functionsClass.SchoolID().ToString().Trim() == studentInformation.School.ToString().Trim())
                    {
                        studentName.Text = studentInformation.FirstName + " " + studentInformation.LastName;
                        studentAddress.Text = studentInformation.Address;
                        studentBirthDate.Text = functionsClass.FormatSlash(studentInformation.BirthDate);
                        studentCity.Text = studentInformation.City;
                        studentEmergencyContact.Text = studentInformation.EmergencyContact;
                        studentID.Text = studentInformation.RegistrationID.ToString();
                        studentNotes.Text = studentInformation.Note;
                        studentState.Text = studentInformation.State;
                        studentZip.Text = studentInformation.ZipCode;

                        editStudent.NavigateUrl = FederationConstants.EDIT_STUDENT_INFORMATION_URL + "?ID=" + studentInformation.ID.ToString();

                        List<StudentArt> studentArts = methodsClass.GetStudentArts(studentInformation.ID);

                        foreach (StudentArt art in studentArts)
                        {
                            studentArtInformation.Text += art.ArtTitle + "-" + art.BeltTitle + "&nbsp;&nbsp;<a href='" + FederationConstants.EDIT_STUDENT_ART_INFORMATION_URL + "?ID=" + art.ID + "'>Edit</a><br />";
                        }
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "&#149; " + FederationMessages.NO_ACCESS_STUDENT;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "&#149; " + FederationMessages.NO_STUDENT_FOUND;
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