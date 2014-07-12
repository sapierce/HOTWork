using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation
{
    public partial class SchoolLanding : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (functionsClass.SchoolSelected())
            {
                School schoolInformation = methodsClass.GetSchoolBySchoolID(functionsClass.SchoolID());

                if (schoolInformation != null)
                {
                    schoolName.Text = schoolInformation.SchoolName;
                    viewStudents.NavigateUrl = FederationConstants.STUDENT_LIST_URL;
                    addStudent.NavigateUrl = FederationConstants.ADD_STUDENT_INFORMATION_URL;
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "&#149; " + FederationMessages.NO_SCHOOL_FOUND;
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