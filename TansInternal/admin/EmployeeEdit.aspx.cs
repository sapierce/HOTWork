using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class EmployeeEdit : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Edit Employee";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL); 
            
            List<HOTBAL.Employee> employeeInformation = sqlClass.GetEmployeeByID(Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ID"])));

            if (employeeInformation != null)
            {
                employeeNumber.Text = employeeInformation[0].EmployeeID.ToString();
                firstName.Text = employeeInformation[0].FirstName;
                lastName.Text = employeeInformation[0].LastName;
            }
            else
                errorMessage.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
        }

        protected void editEmployee_OnClick(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    bool response = sqlClass.EditEmployee(Convert.ToInt32(functionsClass.CleanUp(employeeNumber.Text)),
                        functionsClass.LightCleanUp(firstName.Text),
                        functionsClass.LightCleanUp(lastName.Text));

                    if (response)
                    {
                        Response.Redirect("default.aspx");
                    }
                    else
                    {
                        errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    sqlClass.LogErrorMessage(ex, "", "EmployeeEdit: editEmployee_OnClick");
                }
            }
        }

        protected void deleteEmployee_OnClick(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    bool response = sqlClass.DeleteEmployee(Convert.ToInt32(functionsClass.CleanUp(employeeNumber.Text)));

                    if (response)
                    {
                        Response.Redirect("default.aspx");
                    }
                    else
                    {
                        errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    sqlClass.LogErrorMessage(ex, "", "EmployeeEdit: deleteEmployee_OnClick");
                }
            }
        }

        protected void resetPassword_OnClick(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    bool response = sqlClass.ResetEmployeePassword(Convert.ToInt32(functionsClass.CleanUp(employeeNumber.Text)));

                    if (response)
                    {
                        Response.Redirect("default.aspx");
                    }
                    else
                    {
                        errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    sqlClass.LogErrorMessage(ex, "", "EmployeeEdit: resetPassword_OnClick");
                }
            }
        }
    }
}