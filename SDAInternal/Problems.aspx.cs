using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page allows the users to report issues and replay
    ///     comments to the programmer.
    /// </summary>
    public partial class Problems : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();

        /// <summary>
        /// This method loads and sets up the initial reporting page.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Build the title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Problems";
        }

        /// <summary>
        /// This is the onClick method for sending the reported problem/comment.
        ///     If the page is validated, the e-mail is sent and a message relayed
        ///     to the user.
        /// </summary>
        protected void sendProblem_Click(object sender, EventArgs e)
        {
            // Is the page valid?
            if (Page.IsValid)
            {
                // Send the e-mail to the programmer
                FunctionsClass.SendMail("problems@hotselfdefense.net", "problems@hotselfdefense.net", "HOTSDA: User Reported Error", "<b>From:</b> " +
                    reporterName.Text + "<br><b>Comment:</b> " + reportComment.Text);

                // Build the success label
                Label successLabel = (Label)this.Master.FindControl("successMessage");

                // Output the success message
                successLabel.Text = "Thank you!  It will be fixed/researched as soon as possible.";
            }
        }
    }
}