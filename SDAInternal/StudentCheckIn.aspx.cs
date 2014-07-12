using System;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class StudentCheckIn : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This page is called from the class details page to check a student in to a
        ///     class and show their attendance of that class.
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
                        bool response = methodsClass.CheckInStudent(Convert.ToInt32(Request.QueryString["CID"].ToString()), Convert.ToInt32(Request.QueryString["ID"].ToString()));

                        // Was the check in successful?
                        if (!response)
                        {
                            // Send the error and output the error message
                            functionsClass.SendErrorMail("StudentCheckIn", new Exception(response.ToString()), Request.QueryString["CID"].ToString());
                            errorLabel.Text = HOTBAL.SDAMessages.ERROR_STUDENT_CHECK;
                        }
                        else
                            // Redirect to the class information
                            Response.Redirect(HOTBAL.SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + Request.QueryString["CID"].ToString());
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
                functionsClass.SendErrorMail("StudentCheckIn: PageLoad", ex, "");
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_STUDENT_CHECK;
            }
        }
    }
}