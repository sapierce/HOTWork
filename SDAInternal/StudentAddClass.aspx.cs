using System;
using System.Collections.Generic;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page allows a student to be added to a selected class.
    /// </summary>
    public partial class StudentAddClass : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This method sets up the initial page, populating the list of classes
        ///     and validating that we have a student ID.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Build the title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Add Student to Class";

            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            try
            {
                // Does the ID element exist?
                if (Request.QueryString["ID"] != null)
                {
                    // Does the ID element have a value
                    if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                    {
                        // Populate drop down of classes
                        List<HOTBAL.Course> coursesResponse = methodsClass.GetAllActiveRepeatingClasses();

                        // Did we get any classes?
                        if (coursesResponse.Count > 0)
                        {
                            // Did we get an error message?
                            if (String.IsNullOrEmpty(coursesResponse[0].Error))
                            {
                                // Clear the drop down
                                classList.Items.Clear();

                                // Add the default option
                                classList.Items.Add(new ListItem("-Choose a Class-", "0"));

                                // Loop through the list of returned classes
                                foreach (HOTBAL.Course course in coursesResponse)
                                {
                                    // Add each class to the list
                                    classList.Items.Add(new ListItem(course.Title + "-" + course.Day + "-" + course.Time, course.ID.ToString()));
                                }
                            }
                            else
                                // Output the error message
                                errorLabel.Text = coursesResponse[0].Error;
                        }
                        else
                            // Output the error message
                            errorLabel.Text = HOTBAL.SDAMessages.NO_CLASS;
                    }
                    else
                        //Ooutput the error message
                        errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENTS;
                }
                else
                    //Ooutput the error message
                    errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENTS;
            }
            catch (Exception ex)
            {
                // Send the error and output the standard message
                functionsClass.SendErrorMail("StudentAddClass: PageLoad", ex, "");
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
            }
        }

        /// <summary>
        /// This is the onClick event to add a student to a chosen class. Once the page has 
        ///     been validated, the student will be added to the class. If that is successful,
        ///     the user is redirected back to the student information page.
        /// </summary>
        protected void addStudent_Click(object sender, EventArgs e)
        {
            // Was the page valid?
            if (Page.IsValid)
            {
                // Set up the error label
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                try
                {
                    // Add the student to the class
                    bool response = methodsClass.AddStudentCourse(Convert.ToInt32(classList.SelectedValue.ToString()), Convert.ToInt32(Request.QueryString["ID"].ToString()));

                    // Was the add successful?
                    if (response)
                        // Redirect to the student information page
                        Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString());
                    else
                        // Output the error message
                        errorLabel.Text = HOTBAL.SDAMessages.ERROR_ADD_STUDENT_CLASS;
                }
                catch (Exception ex)
                {
                    // Send the error and output the standard message
                    functionsClass.SendErrorMail("StudentAddClass: addStudent", ex, "");
                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                }
            }
        }
    }
}