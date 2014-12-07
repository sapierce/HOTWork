using System;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class Problems : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void submitProblem_Click(object sender, EventArgs e)
        {
            string problemReport = "<b>Name:</b>" + reportName.Text + "<br><b>Message:</b>" + commentProblem.Text;
            functionsClass.SendMail("lowlysacker@gmail.com", "hotproblems@hottropicaltans.com", "Problem from HotTans: User Reported", problemReport);

            Label errorLabel = (Label)this.Master.FindControl("successMessage");
            errorLabel.Text = "Thank you!  I'll get it fixed as soon as possible.";
        }
    }
}