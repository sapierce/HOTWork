using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class BedsAdd : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Add Beds";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
        }

        protected void addBed_OnClick(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    bool response = sqlClass.AddNewBed(functionsClass.LightCleanUp(bedDescription.Text), 
                        functionsClass.CleanUp(bedNumber.Text), "W", 
                        functionsClass.CleanUp(bedType.SelectedValue), 
                        bedDisplayInternal.Checked,
                        bedDisplayExternal.Checked);

                    if (response)
                    {
                        Response.Redirect("default.aspx");
                    }
                    else
                    {
                        errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL; ;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    sqlClass.LogErrorMessage(ex, "", "BedsAdd: addBed_OnClick");
                }
            }
        }
    }
}