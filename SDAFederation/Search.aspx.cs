using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation
{
    public partial class Search : System.Web.UI.Page
    {
        private SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        private FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "Federation - Search";

            if (functionsClass.SchoolSelected())
            {
                if (!Page.IsPostBack)
                {
                    customerSearch.Visible = true;
                    customerSearchResults.Visible = false;
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.NOT_LOGGED_IN;
            }
        }

        protected void searchStudents_Click(object sender, EventArgs e)
        {
            if (functionsClass.SchoolSelected())
            {
                customerSearch.Visible = false;
                customerSearchResults.Visible = true;

                List<HOTBAL.Student> studentName = methodsClass.GetStudentsByName(firstName.Text, lastName.Text, functionsClass.SchoolID());

                if (studentName != null)
                {
                    if (String.IsNullOrEmpty(studentName[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Student s in studentName)
                        {
                            List<StudentArt> studentArts = methodsClass.GetStudentArts(s.StudentId);
                            if (studentArts.Count > 0)
                            {
                                int artCount = 0;
                                string artList = String.Empty;
                                foreach (StudentArt studentArt in studentArts)
                                {
                                    if (artCount > 0)
                                    {
                                        artList = "," + studentArt.ArtTitle + "/" + studentArt.BeltTitle;
                                        artCount++;
                                    }
                                    else
                                    {
                                        artList = studentArt.ArtTitle + "/" + studentArt.BeltTitle;
                                        artCount++;
                                    }
                                }
                                studentResults.Text += "<tr><td><a href='" + HOTBAL.FederationConstants.STUDENT_INFORMATION_URL + "?ID=" + s.StudentId + "'>" + s.RegistrationId + " - " + s.LastName + ", " + s.FirstName + " (" + artList + ")</a></td></tr>";
                            }
                            else
                                studentResults.Text += "<tr><td><a href='" + HOTBAL.FederationConstants.STUDENT_INFORMATION_URL + "?ID=" + s.StudentId + "'>" + s.RegistrationId + " - " + s.LastName + ", " + s.FirstName + "</a></td></tr>";
                        }
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "&#149; " + studentName[0].ErrorMessage;
                    }
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