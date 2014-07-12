using System;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class ClassDelete : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This page is called from a student's list of non-recurring classes or lessons to 
        ///     delete the requested class or lesson. If the delete was successful, they are
        ///     redirected back to the student information page.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            try
            {
                // Do the ClassID and StudentID QueryString elements exist?
                if ((Request.QueryString["CID"] != null) && (Request.QueryString["ID"] != null))
                {
                    // Do the ClassID and StudentID QueryString elements have values?
                    if ((!String.IsNullOrEmpty(Request.QueryString["CID"])) && (!String.IsNullOrEmpty(Request.QueryString["ID"])))
                    {
                        // Delete the requested class or lesson
                        bool response = methodsClass.DeleteCourse(Convert.ToInt32(Request.QueryString["CID"].ToString()));

                        // Was the delete successful?
                        if (!response)
                        {
                            // Send the error and output the error message
                            functionsClass.SendErrorMail("ClassDelete", new Exception(response.ToString()), Request.QueryString["CID"].ToString());
                            errorLabel.Text = HOTBAL.SDAMessages.ERROR_DELETE_CLASS;
                        }
                        else
                            // Redirect to the student information
                            Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString());
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
                functionsClass.SendErrorMail("ClassDelete: PageLoad", ex, "");
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_DELETE_CLASS;
            }
        }
    }
}