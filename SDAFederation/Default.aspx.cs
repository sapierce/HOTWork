using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation
{
    public partial class _Default : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            federationSchools.Items.AddRange(functionsClass.GetSchoolList());
        }

        protected void login_Click(object sender, EventArgs e)
        {
            bool response = methodsClass.GetSchoolByLogin(Convert.ToInt32(federationSchools.SelectedValue));//, schoolPassword.Text);

            if (response)
            {
                HttpContext.Current.Session["school"] = federationSchools.SelectedValue;
                Response.Redirect(HOTBAL.FederationConstants.SCHOOL_LANDING_URL, false);
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.INVALID_LOGIN;
            }
        }
    }
}