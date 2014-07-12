﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation
{
    public partial class EditStudentArt : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (functionsClass.SchoolSelected())
            {
                if (!Page.IsPostBack)
                {
                    StudentArt studentArtInformation = methodsClass.GetStudentArt(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                    if (String.IsNullOrEmpty(studentArtInformation.Error))
                    {
                        editArt.Items.AddRange(functionsClass.GetArtList(functionsClass.SchoolID()));
                        editArt.Items.FindByValue(studentArtInformation.ArtID.ToString()).Selected = true;

                        editBelt.Items.AddRange(functionsClass.GetBeltList(studentArtInformation.ArtID));
                        editBelt.Items.FindByValue(studentArtInformation.BeltID.ToString()).Selected = true;
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = "&#149; " + studentArtInformation.Error;
                    }
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.NOT_LOGGED_IN;
            }
        }

        protected void updateArt_Click(object sender, EventArgs e)
        {

        }
    }
}