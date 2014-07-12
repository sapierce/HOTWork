using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDAFederation
{
    public partial class Problem : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "Federation - Problems";

            if (Page.IsPostBack)
            {
                FunctionsClass.SendMail("hotproblems@hottropicaltans.com", "HOTSDA@hottropicaltans.com", "User Reported Error", "<b>From:</b>: " + txtName.Text + "<br><b>Comment:</b> " + txtComment.Text);
                lblResponse.Text = "Thank you!  It will be fixed/researched as soon as possible.";
            }
        }
    }
}