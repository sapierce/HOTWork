using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class EmployeeNotes : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Employee Note";
            
        }

        protected void leaveNote_OnClick(object sender, EventArgs e)
        {
            bool noteSent = sqlClass.AddEmployeeNote(functionsClass.CleanUp(commentFromEmployee.Text), "1", functionsClass.CleanUp(Request.QueryString["ID"].ToString()));

            if (noteSent)
                Response.Redirect(HOTBAL.TansConstants.EMP_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"]);
            else
                errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
        }
    }
}