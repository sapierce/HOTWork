using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class ClassStudentAdd : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This page adds a student to a given class or lesson. Once a student
        ///     is selected, they are added to the class and the user redirected
        ///     to the class detail page.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Build the title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Add Student to Class";

            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            // Is this a PostBack?
            if (!Page.IsPostBack)
            {
                // Does the ID element exist in the QueryString?
                if (Request.QueryString["ID"] != null)
                {
                    // Does the ID element have a value?
                    if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                        // Populate the student list
                        populateStudents();
                    else
                        // Output the error message
                        errorLabel.Text = HOTBAL.SDAMessages.NO_CLASS;
                }
                else
                    // Output the error message
                    errorLabel.Text = HOTBAL.SDAMessages.NO_CLASS;
            }
        }

        /// <summary>
        /// Populates the students drop down with currently active students.
        /// </summary>
        private void populateStudents()
        {
            // Clear the current drop down
            studentList.Items.Clear();

            // Add in the default "Choose" option
            studentList.Items.Add(new ListItem("-Choose A Student-", "0"));

            // Get the list of students
            List<HOTBAL.Student> allStudents = methodsClass.GetAllStudents();

            // Did we get the list of students?
            if (allStudents.Count > 0)
            {
                // Did we get an error when getting the students?
                if (String.IsNullOrEmpty(allStudents[0].Error))
                {
                    // Loop through the list of returned students
                    foreach (HOTBAL.Student student in allStudents)
                    {
                        // Add the student to the list
                        studentList.Items.Add(new ListItem(student.LastName + ", " + student.FirstName, student.ID.ToString()));
                    }
                }
                else
                {
                    // Set up the error label and output the received error
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = allStudents[0].Error;
                }
            }
            else
            {
                // Set up the error label and output the error message
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENTS;
            }
        }

        /// <summary>
        /// This method adds a student to the requested class/lesson. If successful,
        ///     the user is redirected back to the class/lesson details page. Otherwise,
        ///     they are presented with an error message.
        /// </summary>
        protected void addStudentLesson_Click(object sender, EventArgs e)
        {
            // Was the page valid?
            if (Page.IsValid)
            {
                // Set up the error label
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                try
                {
                    // Add the selected student to the lesson
                    bool response = methodsClass.AddStudentCourse(Convert.ToInt32(Request.QueryString["ID"].ToString()), Convert.ToInt32(studentList.SelectedValue.ToString()));

                    // Was adding the student successful?
                    if (response)
                        // Redirect back to the class/lesson page
                        Response.Redirect(HOTBAL.SDAConstants.CLASS_DETAIL_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString());
                    else
                        // Output the error message
                        errorLabel.Text = HOTBAL.SDAMessages.ERROR_ADD_STUDENT_CLASS;
                }
                catch (Exception ex)
                {
                    // Send the error and output the standard message
                    functionsClass.SendErrorMail("ClassStudentAdd: addStudentLesson", ex, "");
                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                }
            }
        }
    }
}