using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace PublicWebsite.MembersArea
{
    public partial class MemberVerify : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!functionsClass.isLoggedIn())
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.SESSION_EXPIRED_PUBLIC;
                }
                else
                {
                    //Get customer information
                    HOTBAL.Customer  user = new HOTBAL.Customer ();
                    user = sqlClass.GetCustomerInformationByID(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()));

                    if (String.IsNullOrEmpty(user.Error))
                    {
                        emailAddress.Text = user.Email;
                    }
                }
            }
        }

        protected void sendEmail_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                sendVerificationEmail(emailAddress.Text);

                String formattedEmail = "<h4 style='color:red'>" + emailAddress.Text + "</h4>";
                verificationResponse.Text = "A verification email had been sent to the following address: " + formattedEmail;
                sendEmail.Visible = false;
            }
        }

        private void sendVerificationEmail(string emailAddress)
        {
            try
            {
                string guid = Guid.NewGuid().ToString().Replace("-","");

                bool saveResponse = sqlClass.SaveLinkInfo(guid, Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()));

                if (saveResponse)
                {
                    functionsClass.SendMail(emailAddress, "verify@hottropicaltans.net", "Email Verification from HOT Tropical Tans", "Thank you for tanning! <br /><br />" +
                                    "Please click on the link below to verify your email address: " +
                                    "<a href='http://www.hottropicaltans.net/Members/Verification.aspx?ID="
                                    + guid+ "'> Click to verify email</a>");
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                sqlClass.LogErrorMessage(ex, "", "Site: MemberVerify");
            }
        }
    }
}