using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite.MembersArea
{
    public partial class RegisterAgree : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Session["age"] != null)
                {
                    if (HttpContext.Current.Session["age"].ToString() == "12")
                    {
                        customerNotification.Text = "As you are under 13, written permission from a physician is required in addition to a parental or legal guardian signature and presence during tanning.";
                    }
                    else if (HttpContext.Current.Session["age"].ToString() == "13")
                    {
                        customerNotification.Text = "As you are under 16, a parental or legal guardian signature and presence during tanning is required.";
                    }
                    else if (HttpContext.Current.Session["age"].ToString() == "16")
                    {
                        customerNotification.Text = "As you are under 18, a parental or legal guardian's signature is required on the first visit.";
                    }
                }
            }
        }

        protected void submit_OnClick(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                if (Session["userID"] != null)
                {
                    if (!String.IsNullOrEmpty(Session["userID"].ToString()))
                    {
                        bool agreement = sqlClass.UpdateCustomerAgreement(functionsClass.CleanUp(customerSignature.Text), readWarnings.Checked, Convert.ToInt64(Session["userID"].ToString()));

                        if (agreement)
                        {
                            HOTBAL.Customer customerInfo = sqlClass.GetCustomerInformationByID(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()));
                            //Send registration email
                            functionsClass.SendMail(customerInfo.Email, "registration@hottropicaltans.net", "Registration for HOTTropicalTans.net", "Thank you for signing up at HOTTropicalTans.Net.  For your records, your username is " + functionsClass.CleanUp(customerInfo.OnlineName) + ".  Should you forget your username or password, you can retrieve or reset them on the website.  Thank you again!");
                            Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_PUBLIC_URL);
                        }
                        else
                        {
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                        }
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                }
            }
        }
    }
}