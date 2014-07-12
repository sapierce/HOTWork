using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation.admin
{
    public partial class AddBelt : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            federationSchools.Items.AddRange(functionsClass.GetSchoolList());
        }

        protected void federationSchools_SelectedIndexChanged(object sender, EventArgs e)
        {
            schoolArts.Items.AddRange(functionsClass.GetArtList(Convert.ToInt32(federationSchools.SelectedValue)));
        }

        protected void addBelt_Click(object sender, EventArgs e)
        {
            bool response = methodsClass.AddBelt(beltTitle.Text, Convert.ToInt32(beltLevel.Text), Convert.ToInt32(schoolArts.SelectedValue), "T", 0);

            if (response)
            {
                Response.Redirect(HOTBAL.FederationConstants.ADMIN_DEFAULT_URL, false);
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.ERROR_ADD_BELT;
            }
        }
    }
}