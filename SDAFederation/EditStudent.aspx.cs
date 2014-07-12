using System;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation
{
    public partial class EditStudent : System.Web.UI.Page
    {
        private SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        private FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (functionsClass.SchoolSelected())
            {
                Student studentInformation = methodsClass.GetStudentInformation(Convert.ToInt32(Request.QueryString["ID"]));

                if (studentInformation != null)
                {
                    if (functionsClass.SchoolID() == studentInformation.School)
                    {
                        firstName.Text = studentInformation.FirstName;
                        lastName.Text = studentInformation.LastName;
                        address.Text = studentInformation.Address;
                        birthDate.Text = functionsClass.FormatSlash(studentInformation.BirthDate);
                        city.Text = studentInformation.City;
                        emergencyContact.Text = studentInformation.EmergencyContact;
                        studentID.Text = studentInformation.ID.ToString();
                        notes.Text = studentInformation.Note;
                        state.Text = studentInformation.State;
                        zipCode.Text = studentInformation.ZipCode;
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

        protected void updateStudent_Click(object sender, EventArgs e)
        {
            try
            {
                if (functionsClass.SchoolSelected())
                {
                    bool response = methodsClass.UpdateStudent(Convert.ToInt32(studentID.Text), firstName.Text, lastName.Text, address.Text,
                        city.Text, state.Text, zipCode.Text, emergencyContact.Text, Convert.ToDateTime(birthDate.Text), notes.Text);

                    if (response)
                        Response.Redirect(FederationConstants.STUDENT_INFORMATION_URL + "?ID=" + studentID.Text);
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "&#149; " + FederationMessages.ERROR_UPDATE_STUDENT;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "&#149; " + FederationMessages.NOT_LOGGED_IN;
                }
            }
            catch (Exception ex)
            {
                functionsClass.SendErrorMail("Federation: updateStudent_Click", ex, "");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.ERROR_GENERIC;
            }
        }
    }
}