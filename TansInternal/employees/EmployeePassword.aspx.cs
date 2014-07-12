using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class EmployeePassword : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Employee Password";
            if (!Page.IsPostBack)
            {
                List<HOTBAL.Employee> employeeInformation = sqlClass.GetEmployeeByID(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                if (employeeInformation != null)
                {
                    employeeName.Text = employeeInformation[0].FirstName + " " + employeeInformation[0].LastName;
                    employeeNumber.Text = employeeInformation[0].EmployeeID.ToString();
                }
                else
                    errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
        }

        protected void changePassword_Click(object sender, EventArgs e)
        {
            Page.Validate("employeePassword");
            if (Page.IsValid)
            {
                if (functionsClass.CleanUp(Request.QueryString["status"].ToString()) == "new")
                {
                    // Change the password
                    ChangePassword();
                }
                else
                {
                    bool isValidOldPassword = sqlClass.EmployeeLogin(Convert.ToInt32(Request.QueryString["ID"].ToString()), currentPassword.Text);

                    if (isValidOldPassword)
                    {
                        // Change the password
                        ChangePassword();
                    }
                    else
                    {
                        errorMessage.Text = "Invalid old password. Please check and try again.";
                    }
                }
            }
        }

        protected void ChangePassword()
        {
            // Change the password
            bool successful = sqlClass.UpdateEmployeePassword(Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ID"].ToString())), newPassword.Text);

            if (successful)
                Response.Redirect(HOTBAL.TansConstants.EMP_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"]);
            else
                errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
        }
    }
}