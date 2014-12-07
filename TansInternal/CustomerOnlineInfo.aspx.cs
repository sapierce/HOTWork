using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class CustomerOnlineInfo : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                HOTBAL.Customer customerInfo = sqlClass.GetCustomerInformationByID(Convert.ToInt32(Request.QueryString["ID"]));

                if (String.IsNullOrEmpty(customerInfo.Error))
                {
                    userName.Text = customerInfo.OnlineName;
                    emailAddress.Text = customerInfo.Email;

                    if (customerInfo.NewOnlineCustomer)
                    {
                        signUpInfo.Visible = true;
                        address.Text = customerInfo.Address;
                        city.Text = customerInfo.City;
                        state.Text = customerInfo.State;
                        dateOfBirth.Text = functionsClass.FormatSlash(customerInfo.DateOfBirth);
                        fitzPatrickNumber.Text = customerInfo.FitzPatrickNumber.ToString();
                        name.Text = customerInfo.FirstName + " " + customerInfo.LastName;
                        phoneNumber.Text = customerInfo.PhoneNumber;
                        signature.Text = customerInfo.AcknowledgeWarningText;
                        acknowledgedNotice.Checked = customerInfo.AcknowledgeWarning;
                        familyHistory.Checked = customerInfo.FamilyHistory;
                        personalHistory.Checked = customerInfo.PersonalHistory;
                    }
                    else
                    {
                        signUpInfo.Visible = false;
                    }
                }
            }
        }

        protected void sendPassword_Click(object sender, EventArgs e)
        {
            string response = sqlClass.ResetUserPassword(emailAddress.Text);
            Label errorLabel = (Label)this.Master.FindControl("successMessage");
            errorLabel.Text = response;
        }

        protected void deleteAccount_Click(object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteCustomerOnlineAccount(Convert.ToInt32(Request.QueryString["ID"]));
            if (response)
            {
                Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"]);
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
        }

        protected void changeEmail_Click(object sender, EventArgs e)
        {
            if (sqlClass.UpdateOnlineInfo(Convert.ToInt32(Request.QueryString["ID"]), functionsClass.InternalCleanUp(newEmailAddress.Text), true))
            {
                Label errorLabel = (Label)this.Master.FindControl("successMessage");
                errorLabel.Text = HOTBAL.TansMessages.SUCCESS_MESSAGE;
                emailAddress.Text = functionsClass.InternalCleanUp(newEmailAddress.Text);
            }
        }
    }
}