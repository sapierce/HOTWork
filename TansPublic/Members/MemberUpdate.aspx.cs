using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite.MembersArea
{
    public partial class MemberUpdate : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = Page.Title + " - Member Information Update";
            try
            {
                if (!functionsClass.isLoggedIn())
                {
                    errorMessage.Text = HOTBAL.TansMessages.SESSION_EXPIRED_PUBLIC;
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        //Get customer information
                        HOTBAL.Customer user = new HOTBAL.Customer();
                        user = sqlClass.GetCustomerInformationByID(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()));

                        if (String.IsNullOrEmpty(user.Error))
                        {
                            emailAddress.Text = user.Email;
                            if (user.ReceiveSpecials == true)
                            {
                                receiveSpecials.Checked = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                sqlClass.LogErrorMessage(ex, "", "MemberUpdate: Page Load");
            }
        }

        public void updateInformation_Click(Object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                try
                {
                    if (HttpContext.Current.Session["userID"] == null)
                    {
                        errorMessage.Text = HOTBAL.TansMessages.SESSION_EXPIRED_PUBLIC;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(oldPassword.Text))
                        {
                            //Get customer information
                            HOTBAL.Customer user = new HOTBAL.Customer();
                            user = sqlClass.GetCustomerInformationByID(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()));

                            //Updating password
                            bool passwordCheck = sqlClass.VerifyLogin(user.OnlineName, oldPassword.Text);

                            if (passwordCheck)
                            {
                                //Password is valid, update the password
                                bool passwordUpdate = sqlClass.UpdatePasswordOnly(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()), newPassword.Text);

                                if (!passwordUpdate)
                                {
                                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                    errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC;
                                }
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text += HOTBAL.TansMessages.ERROR_INVALID_OLD_PASSWORD;
                            }
                        }

                        if (String.IsNullOrEmpty(errorMessage.Text))
                        {
                            //Updating something else
                            bool update = sqlClass.UpdateOnlineInfo(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()), functionsClass.InternalCleanUp(emailAddress.Text), receiveSpecials.Checked);

                            if (!update)
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC;
                            }
                        }

                        if (String.IsNullOrEmpty(errorMessage.Text))
                        {
                            Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_PUBLIC_URL, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                    sqlClass.LogErrorMessage(ex, "", "MemberUpdate: updateInformation_Click");
                }
            }
        }
    }
}