using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation.admin
{
    public partial class EditArt : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Art artInformation = methodsClass.GetArtByID(Convert.ToInt32(Request.QueryString["ID"]));
                if (artInformation.ArtTitle != null)
                {
                    School schoolInformation = methodsClass.GetSchoolBySchoolID(artInformation.SchoolId);
                    artTitle.Text = artInformation.ArtTitle;
                    artSchool.Text = schoolInformation.SchoolName;
                }
            }
        }

        protected void editArt_Click(object sender, EventArgs e)
        {
            bool response = methodsClass.UpdateArt(Convert.ToInt32(Request.QueryString["ID"]), artTitle.Text);

            if (response)
                Response.Redirect(HOTBAL.FederationConstants.ADMIN_DEFAULT_URL);
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.ERROR_EDIT_ART;
            }
        }
    }
}