using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.Net.Mail;

namespace PublicWebsite.MembersArea
{
    public partial class NewRegister : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void registerNew_OnClick(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "";
                AddNewUser();
                if (String.IsNullOrEmpty(errorMessage.Text))
                {
                    AgeCheck();
                }

                if (String.IsNullOrEmpty(errorMessage.Text))
                {
                    Response.Redirect(HOTBAL.TansConstants.REGISTER_AGREE_PUBLIC_URL);
                }
            }
        }

        public void AgeCheck()
        {
            // Check to see if they are 16-18, or 13-15, or under 13
            TimeSpan ts = DateTime.Now - Convert.ToDateTime(dateOfBirth.Text);
            bool note = false;
            if ((ts.Days >= 5840) && (ts.Days < 6570))
            {
                note = sqlClass.AddCustomerNote(Convert.ToInt32(Session["userID"].ToString()), "Requires parental signature", false, false, false);
                HttpContext.Current.Session["age"] = "16";
            }
            else if ((ts.Days >= 4745) && (ts.Days < 5840))
            {
                note = sqlClass.AddCustomerNote(Convert.ToInt32(Session["userID"].ToString()), "Requires parental signature and presence", false, false, false);
                HttpContext.Current.Session["age"] = "13";
            }
            else if (ts.Days < 4745)
            {
                note = sqlClass.AddCustomerNote(Convert.ToInt32(Session["userID"].ToString()), "Requires physicians note, parental signature and presence", false, false, false);
                HttpContext.Current.Session["age"] = "12"; 
            }
            else
            {
                HttpContext.Current.Session["age"] = "18";
            }
        }

        public void AddNewUser()
        {
            try
            {
                bool userNameCheck = sqlClass.UserNameCheck(functionsClass.LightCleanUp(userName.Text));

                if (!userNameCheck)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_USER_EXISTS;
                }
                else
                {
                    AddNewCustomer();

                    if (String.IsNullOrEmpty(errorMessage.Text))
                    {
                        bool userAddResponse = sqlClass.InsertOnlineUser(Convert.ToInt64(Session["userID"]), userName.Text, password.Text, emailAddress.Text, receiveSpecials.Checked);

                        if (userAddResponse)
                        {
                            long siteID = sqlClass.TanIDByUserName(userName.Text);

                            if (siteID > 0)
                            {
                                Session["siteID"] = siteID;
                            }
                            else
                            {
                                //Error
                                Exception ex = new Exception("Error");
                                sqlClass.LogErrorMessage(ex, "", "SignUp: New: GetUser: Find");
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text += "Cannot find user ID.  Please try again.  If you continue to receive this error, please contact us at <a href='mailto:contact@hottropicaltans.com' class='center'>contact@hottropicaltans.net</a>.<br>";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "SignUp: New: UserIDCheck");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Error checking username.  Please try again.  If you continue to receive this error, please contact us at <a href='mailto:contact@hottropicaltans.com' class='center'>contact@hottropicaltans.net</a>.<br>";
            }
        }

        public bool AddNewCustomer()
        {
            bool success = false;
            try
            {
                // Insert into CUSTOMERS
                Int64 addCustomer = sqlClass.InsertNewCustomer(firstName.Text, 
                    lastName.Text,
                    DateTime.Now,
                    Convert.ToInt32(fitzpatrickNumber.SelectedValue), "Other", 
                    DateTime.Now, "", true, true, false, 0, Convert.ToDateTime("2001-01-01"));

                if (addCustomer != 0)
                {
                    HOTBAL.Customer newCustomer = sqlClass.GetCustomerInformationByID(addCustomer);
                    if (newCustomer != null)
                    {
                        if (String.IsNullOrEmpty(newCustomer.Error))
                        {
                            Session["userID"] = newCustomer.ID;
                            // Insert into CUST_NEW
                            bool addOnlineCustomer = sqlClass.InsertNewCustomerOnline(firstName.Text, 
                                lastName.Text, address.Text, 
                                city.Text, state.Text, 
                                zipCode.Text, phoneNumber.Text, 
                                Convert.ToDateTime(dateOfBirth.Text), Convert.ToInt32(fitzpatrickNumber.SelectedValue), 
                                (familyHistory.SelectedValue == "Y" ? true : false), (personalHistory.SelectedValue == "Y" ? true : false), newCustomer.ID);
                            if (addOnlineCustomer)
                            {
                                success = true;
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text = HOTBAL.TansMessages.ERROR_ADD_CUSTOMER;
                            }
                        }
                        else
                        {
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = HOTBAL.TansMessages.ERROR_ADD_CUSTOMER;
                        }
                    }
                    else
                    {
                        Exception ex = new Exception(addCustomer.ToString());
                        sqlClass.LogErrorMessage(ex, "", "SignUp: New: Null Customer");
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_ADD_CUSTOMER;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_ADD_CUSTOMER;
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "SignUp: New: AddNewCustomer");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Error adding new customer.  Please try again.  If you continue to receive this error, please contact us at <a href='mailto:contact@hottropicaltans.com' class='center'>contact@hottropicaltans.net</a>.<br>";
            }

            return success;
        }
    }
}
