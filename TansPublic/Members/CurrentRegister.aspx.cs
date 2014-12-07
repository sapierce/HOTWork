using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace PublicWebsite.MembersArea
{
    public partial class CurrentRegister : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                unknownUser.Visible = false;
                existingUser.Visible = false;
                confirmSingleMember.Visible = false;
                confirmMultipleMember.Visible = false;
                selectUser.Visible = false;
                signUpSearch.Visible = true;
            }
        }

        public void RegisterUser()
        {
            try
            {
                if (HttpContext.Current.Session["userID"] == null)
                {
                    if (!String.IsNullOrEmpty(multipleUsers.SelectedValue))
                    {
                        HttpContext.Current.Session["userID"] = multipleUsers.SelectedValue;
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                    }
                }

                bool userNameCheck = sqlClass.UserNameCheck(functionsClass.InternalCleanUp(userName.Text));

                if (!userNameCheck)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_USER_EXISTS;
                }
                else
                {
                    bool userAddResponse = sqlClass.InsertOnlineUser(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()), userName.Text, password.Text, emailAddress.Text, receiveSpecials.Checked);

                    if (userAddResponse)
                    {
                        userAddResponse = sqlClass.UpdateOnlineStatus(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()), true);

                        if (userAddResponse)
                        {
                            functionsClass.SendMail(emailAddress.Text, "register@hottropicaltans.com", "Registration for HOTTropicalTans.net", "Thank you for signing up at HOTTropicalTans.Net.  For your records, your username is " + functionsClass.CleanUp(userName.Text) + ".  Should you forget your username or password, you can retrieve or reset them on the website.  Thank you again!");

                            Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_PUBLIC_URL, false);
                        }
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                    }
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "SignUp: RegisterUser");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Error registering.  Please try again.  If you continue to receive this error, please contact us at <a href='mailto:contact@hottropicaltans.com' class='center'>contact@hottropicaltans.net</a>.<br>";
            }
            finally
            {
            }
        }

        protected void findCustomer_OnClick(object sender, EventArgs e)
        {
            Page.Validate("lookupUser");
            if (Page.IsValid)
            {
                List<HOTBAL.Customer> customerResponse = sqlClass.GetCustomerByName(firstName.Text, lastName.Text, false);

                if (String.IsNullOrEmpty(customerResponse[0].Error))
                {
                    //At least one customer found
                    firstName.Text = "";
                    lastName.Text = "";

                    if (customerResponse.Count == 1)
                    {
                        if (customerResponse[0].OnlineUser == false)
                        {
                            //Single user
                            confirmSingleMember.Visible = true;
                            selectUser.Visible = true;
                            signUpSearch.Visible = false;
                            confirmLastName.Text = customerResponse[0].LastName;
                            confirmFirstName.Text = customerResponse[0].FirstName;
                            confirmJoinDate.Text = functionsClass.FormatSlash(customerResponse[0].JoinDate);
                            confirmPlan.Text = customerResponse[0].Plan;
                            HttpContext.Current.Session["userID"] = customerResponse[0].ID.ToString();
                        }
                        else
                        {
                            signUpSearch.Visible = false;
                            existingUser.Visible = true;
                        }
                    }
                    else if (customerResponse.Count > 1)
                    {
                        //Multiple users
                        confirmMultipleMember.Visible = true;
                        selectUser.Visible = true;
                        signUpSearch.Visible = false;

                        foreach (HOTBAL.Customer x in customerResponse)
                        {
                            if (x.OnlineUser == false)
                            {
                                multipleUsers.Items.Add(new ListItem("<b>Name:</b>" + x.LastName +
                                    ", " + x.FirstName +
                                    " | <b>Join Date:</b> " + functionsClass.FormatSlash(x.JoinDate) +
                                    " | <b>Plan:</b> " + x.Plan, x.ID.ToString()));
                            }
                        }
                    }
                    else
                    {
                        //No customers found
                        unknownUser.Visible = true;
                        signUpSearch.Visible = false;
                    }
                }
                else
                {
                    if (customerResponse[0].Error == HOTBAL.TansMessages.ERROR_CANNOT_FIND_CUSTOMER_SITE)
                    {
                        signUpSearch.Visible = false;
                        unknownUser.Visible = true;
                    }
                    else if (customerResponse[0].Error == HOTBAL.TansMessages.ERROR_EXISTING_ACCOUNT_SITE)
                    {
                        signUpSearch.Visible = false;
                        existingUser.Visible = true;
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = customerResponse[0].Error;
                    }
                }
            }
        }

        protected void addNewUser_OnClick(object sender, EventArgs e)
        {
            Page.Validate("addUser");
            if (Page.IsValid)
            {
                RegisterUser();
            }
        }
    }
}