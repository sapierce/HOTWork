﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation.admin
{
    public partial class EditBelt : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Belt beltInformation = methodsClass.GetBeltByID(Convert.ToInt32(Request.QueryString["ID"]));
                if (beltInformation.BeltId != 0)
                {
                    beltTitle.Text = beltInformation.BeltTitle;
                    beltLevel.Text = beltInformation.BeltLevel;

                    Art artInformation = methodsClass.GetArtByID(beltInformation.ArtId);
                    if (artInformation.ArtId != 0)
                    {
                        artTitle.Text = artInformation.ArtTitle;
                        artID.Value = artInformation.ArtId.ToString();

                        School schoolInformation = methodsClass.GetSchoolBySchoolID(artInformation.SchoolId);
                        if (schoolInformation.SchoolID != 0)
                        {
                            beltSchool.Text = schoolInformation.SchoolName;
                        }
                    }
                }
            }
        }

        protected void editBelt_Click(object sender, EventArgs e)
        {
            bool response = methodsClass.UpdateBelt(Convert.ToInt32(Request.QueryString["ID"]), beltTitle.Text, Convert.ToInt32(beltLevel.Text),
                Convert.ToInt32(artID.Value), "C", 0);

            if (response)
                Response.Redirect(FederationConstants.ADMIN_DEFAULT_URL);
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.ERROR_EDIT_BELT;
            }
        }
    }
}