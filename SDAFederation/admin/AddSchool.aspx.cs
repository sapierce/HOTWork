using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation.admin
{
    public partial class AddSchool : System.Web.UI.Page
    {
        HOTBAL.FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addSchool_Click(object sender, EventArgs e)
        {
            Page.Validate("addSchool");
            if (Page.IsValid)
            {
                bool response = methodsClass.AddSchoolInformation(schoolName.Text);//, schoolPassword.Text);

                if (response)
                    Response.Redirect(HOTBAL.FederationConstants.ADMIN_DEFAULT_URL);
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "&#149; " + HOTBAL.FederationMessages.ERROR_ADD_SCHOOL;
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; Missing information";
            }
        }
    }
}