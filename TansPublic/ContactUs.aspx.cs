using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class ContactUs : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the page name
            Page.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Contact Us";

            // Is this an existing member who is already logged in?
            if (functionsClass.isLoggedIn())
            {
                try
                {
                    // Get the information we have
                    HOTBAL.Customer customerInformation = sqlClass.GetCustomerInformationByID(Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()));

                    // Was there an error getting the information?
                    if (String.IsNullOrEmpty(customerInformation.Error))
                    {
                        // Pre-populate the form with information we already have
                        enteredName.Text = customerInformation.FirstName + " " + customerInformation.LastName;
                        enteredEmail.Text = customerInformation.Email;
                    }
                    else
                    {
                        // Log and return an error message
                        sqlClass.LogErrorMessage(new Exception("CustomerInfoError"),
                            HttpContext.Current.Session["userID"].ToString() + "-" + customerInformation.Error,
                            "Site: ContactUs: CustomerInformation");
                    }
                }
                catch (Exception ex)
                {
                    // Log and return an error message
                    sqlClass.LogErrorMessage(ex, "", "Site: ContactUs: PageLoad");
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                }
            }

            // Show the comment form and hide the response panel
            enterComments.Visible = true;
            responsePanel.Visible = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void submitComment_Click(object sender, EventArgs e)
        {
            // Force the page to validate
            Page.Validate();

            // Did the page pass validation?
            if (Page.IsValid)
            {
                try
                {
                    // Send the e-mail with the customer comments
                    functionsClass.SendMail("lowlysacker@gmail.com",
                            (String.IsNullOrEmpty(functionsClass.InternalCleanUp(enteredEmail.Text)) ? "User@NoReply.Net" : functionsClass.InternalCleanUp(enteredEmail.Text)),
                            "Customer Comment - HOTTropicalTans.net - "
                            + (String.IsNullOrEmpty(functionsClass.CleanUp(commentAbout.SelectedValue)) ? "Unknown" : functionsClass.CleanUp(commentAbout.SelectedValue)),
                            "<b>Comment From:</b>"
                            + (String.IsNullOrEmpty(functionsClass.CleanUp(enteredName.Text)) ? "Unknown" : functionsClass.CleanUp(enteredName.Text))
                            + "<br /><b>Comment:</b>"
                            + (String.IsNullOrEmpty(functionsClass.CleanUp(enteredComment.Text)) ? "No Comment Left" : functionsClass.CleanUp(enteredComment.Text)));

                    // Log the customer comments to the database
                    bool response = sqlClass.AddComment((String.IsNullOrEmpty(functionsClass.InternalCleanUp(enteredEmail.Text)) ? "User@NoReply.Net" : functionsClass.InternalCleanUp(enteredEmail.Text)),
                        (String.IsNullOrEmpty(functionsClass.CleanUp(enteredName.Text)) ? "From Unknown" : functionsClass.CleanUp(enteredName.Text)),
                        (String.IsNullOrEmpty(functionsClass.CleanUp(commentAbout.SelectedValue)) ? "Unknown" : functionsClass.CleanUp(commentAbout.SelectedValue)),
                        (String.IsNullOrEmpty(functionsClass.CleanUp(enteredComment.Text)) ? "No Comment Left" : functionsClass.CleanUp(enteredComment.Text)));

                    // Was the log successful?
                    if (response)
                    {
                        // Hide the comment form and show the response panel
                        enterComments.Visible = false;
                        responsePanel.Visible = true;
                    }
                    else
                    {
                        // Hide the comment form and show the response panel
                        enterComments.Visible = false;
                        responsePanel.Visible = true;

                        // Show an error message
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                    }
                }
                catch (Exception ex)
                {
                    // Hide the comment form and show the response panel
                    enterComments.Visible = false;
                    responsePanel.Visible = true;

                    // Log and show error message
                    sqlClass.LogErrorMessage(ex, "", "Site: Contact Us: Submit");
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                }
            }
            else
            {
                // Show the error message
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_FORM_INVALID;
            }
        }
    }
}