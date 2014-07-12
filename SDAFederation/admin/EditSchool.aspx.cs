using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation.admin
{
    public partial class EditSchool : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                School schoolInformation = methodsClass.GetSchoolBySchoolID(Convert.ToInt32(Request.QueryString["ID"]));

                if (schoolInformation != null)
                {
                    schoolName.Text = schoolInformation.SchoolName;
                }
            }
        }

        protected void editSchool_Click(object sender, EventArgs e)
        {
            bool response = methodsClass.UpdateSchoolInformation(Convert.ToInt32(Request.QueryString["ID"]), schoolName.Text);

            if (response)
                Response.Redirect(FederationConstants.ADMIN_DEFAULT_URL);
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.ERROR_EDIT_SCHOOL;
            }
        }
    }
}