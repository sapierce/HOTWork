using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederationPOS
{
    public partial class Default : System.Web.UI.Page
    {
        SDAFunctionsClass FunctionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "Federation - Point of Sale";
            List<HOTBAL.School> schoolList = methodsClass.GetAllSchools();
            federationSchools.Items.Add(new ListItem("-SELECT-", "0"));
            foreach (HOTBAL.School school in schoolList)
            {
                federationSchools.Items.Add(new ListItem(school.SchoolName, school.SchoolID.ToString()));
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lastName.Text))
            {
                List<HOTBAL.Student> studentName = methodsClass.GetStudentsByName(firstName.Text, lastName.Text, Convert.ToInt32(federationSchools.SelectedValue));

                if (studentName != null)
                {
                    if (String.IsNullOrEmpty(studentName[0].Error))
                    {
                        if (studentName.Count > 1)
                        {
                            resultsList.Text = "Customer Results";
                            customersList.Text = "<table>";
                            foreach (Student s in studentName)
                            {
                                customersList.Text += "<tr><td class='standardField'><a href='" + FederationConstants.CART_URL + "?ID=" + s.ID.ToString() + "&Action='>" + s.LastName + ", " + s.FirstName + "</a></td></tr>";
                            }
                            customersList.Text += "</table>";
                        }
                        else
                        {
                            Response.Redirect(FederationConstants.CART_URL + "?ID=" + studentName[0].ID.ToString() + "&Action=");
                        }
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "&#149; " + studentName[0].Error;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "&#149; " + FederationMessages.NO_STUDENT_FOUND;
                }
            }
        }
    }
}