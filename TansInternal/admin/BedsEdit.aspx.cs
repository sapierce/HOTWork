using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class BedsEdit : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Edit Bed";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);

            if (Request.QueryString["ID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    // Get bed information
                    HOTBAL.Bed returnBed = sqlClass.GetBedInformationByID(Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ID"].ToString())));

                    bedDescription.Text = returnBed.BedLong;
                    bedNumber.Text = returnBed.BedShort;
                    bedType.Items.FindByValue(returnBed.BedType).Selected = true;
                    bedDisplayInternal.Checked = returnBed.BedDisplayInternal;
                    bedDisplayExternal.Checked = returnBed.BedDisplayExternal;
                }
            }
        }

        protected void editBed_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                try
                {
                    bool response = sqlClass.UpdateBed(Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ID"].ToString())), 
                        functionsClass.InternalCleanUp(bedDescription.Text), 
                        functionsClass.CleanUp(bedNumber.Text), "W", 
                        functionsClass.CleanUp(bedType.SelectedValue),
                        bedDisplayInternal.Checked,
                        bedDisplayExternal.Checked);

                    if (response)
                    {
                        Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
                    }
                    else
                    {
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                catch (Exception ex)
                {
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    sqlClass.LogErrorMessage(ex, "", "BedsEdit: editBed_OnClick");
                }
            }
        }

        protected void deleteBed_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                try
                {
                    bool response = sqlClass.DeleteBed(Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ID"].ToString())));

                    if (response)
                    {
                        Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
                    }
                    else
                    {
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                catch (Exception ex)
                {
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    sqlClass.LogErrorMessage(ex, "", "BedsEdit: editBed_OnClick");
                }
            }
        }
    }
}