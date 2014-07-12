using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class Default : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Employee Administration";
            if (!Page.IsPostBack)
            {
                employeeNumber.Text = String.Empty;
                employeePassword.Text = String.Empty;
            }
        }

        protected void submit_OnClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(employeePassword.Text))
            {
                if (sqlClass.EmployeeLogin(Convert.ToInt32(employeeNumber.Text), ""))
                {
                    Response.Redirect("EmployeePassword.aspx?ID=" + functionsClass.CleanUp(employeeNumber.Text) + "&status=new");
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "There is already a password set for this employee number.";
                }
            }
            else
            {
                if (sqlClass.EmployeeLogin(Convert.ToInt32(employeeNumber.Text), employeePassword.Text))
                {
                    Response.Redirect("EmployeeInformation.aspx?ID=" + functionsClass.CleanUp(employeeNumber.Text));
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "Invalid password for employee number " + functionsClass.CleanUp(employeeNumber.Text);
                }
            }
        }
    }
}