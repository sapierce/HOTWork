using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation.admin
{
    public partial class AddArt : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            federationSchools.Items.AddRange(functionsClass.GetSchoolList());
        }

        protected void addArt_Click(object sender, EventArgs e)
        {
            bool response = methodsClass.AddArt(artTitle.Text, Convert.ToInt32(federationSchools.SelectedValue));

            if (response)
            {
                Response.Redirect(HOTBAL.FederationConstants.ADMIN_DEFAULT_URL, false);
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.ERROR_ADD_ART;
            }
        }
    }
}