using System;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class StudentDelete : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        ///
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            try
            {
                // Does the StudentID QueryString element exist?
                if (Request.QueryString["ID"] != null)
                {
                    // Does the StudentID QueryString element have values?
                    if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                    {
                        // Remove the student
                        bool response = methodsClass.DeleteStudent(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                        // Was the delete successful?
                        if (!response)
                        {
                            // Send the error and output the error message
                            functionsClass.SendErrorMail("DeleteStudent", new Exception(response.ToString()), Request.QueryString["ID"].ToString());
                            errorLabel.Text = HOTBAL.SDAMessages.ERROR_DELETE_STUDENT;
                        }
                        else
                            // Redirect to the main page
                            Response.Redirect(HOTBAL.SDAConstants.MAIN_INTERNAL_URL);
                    }
                    else
                        // Output the error message
                        errorLabel.Text = HOTBAL.SDAMessages.NO_CLASS;
                }
                else
                    // Output the error message
                    errorLabel.Text = HOTBAL.SDAMessages.NO_CLASS;
            }
            catch (Exception ex)
            {
                // Send the error and output the standard message
                functionsClass.SendErrorMail("StudentDelete: PageLoad", ex, "");
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_STUDENT_CHECK;
            }
        }
    }
}