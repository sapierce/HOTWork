using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation
{
    public partial class AddStudent : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (functionsClass.SchoolSelected())
            {
                List<Art> artInformation = methodsClass.GetArtsBySchoolID(functionsClass.SchoolID());
                if (artInformation != null)
                {
                    artSelection.Items.Add(new ListItem("-SELECT-", "0"));
                    foreach (Art artList in artInformation)
                    {
                        artSelection.Items.Add(new ListItem(artList.Title, artList.ID.ToString()));
                    }
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.NOT_LOGGED_IN;
            }
        }

        protected void addStudent_Click(object sender, EventArgs e)
        {
            try
            {
                if (functionsClass.SchoolSelected())
                {
                    long studentID = methodsClass.AddStudentInformation(federationID.Text, firstName.Text, lastName.Text, address.Text, city.Text, state.Text, (zip.Text != "" ? zip.Text : "0"),
                        Convert.ToDateTime((birthDate.Text != "" ? birthDate.Text : DateTime.Now.ToShortDateString())), emergencyContact.Text, Convert.ToInt32(artSelection.SelectedValue), 
                        Convert.ToInt32(beltSelection.SelectedValue), functionsClass.SchoolID());

                    if (studentID != 0)
                    {
                        Response.Redirect(FederationConstants.STUDENT_INFORMATION_URL + "?ID=" + studentID.ToString(), false);
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "&#149; " + FederationMessages.ERROR_ADD_STUDENT;
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
                functionsClass.SendErrorMail("Federation: AddStudent_Click", ex, "");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.ERROR_GENERIC;
            }
        }

        protected void artSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Belt> beltList = methodsClass.GetArtBelts(Convert.ToInt32(artSelection.SelectedValue));

            foreach (Belt belt in beltList)
            {
                beltSelection.Items.Add(new ListItem(belt.Title, belt.ID.ToString()));
            }
        }
    }
}