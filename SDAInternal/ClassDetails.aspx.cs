using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page outputs detail information about a selected class or lesson. It lists
    ///     primary information as well as a list of students. For each student, a link is
    ///     given to check the student in for attendance, as well as notes about the student.
    ///     There are also links to generate a printable version of this list and to add new
    ///     students to this class/lesson.
    /// </summary>
    public partial class ClassDetails : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// The initial loading method. The class ID is passed in via QueryString and information
        ///     retrieved based on that ID.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Build the title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Class Details";

            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            try
            {
                // Does the ClassID QueryString element exist?
                if (Request.QueryString["ID"] != null)
                {
                    // Does the ClassID QueryString element have a value?
                    if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                    {
                        // Get class detail information
                        HOTBAL.Course courseDetails = methodsClass.GetClassInformation(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                        // Did we find class detail information?
                        if (courseDetails.ID > 0)
                        {
                            // Get and output the full art name
                            classArt.Text = methodsClass.GetArtTitle(courseDetails.FirstArtID);

                            // Is there a secondary art?
                            if (courseDetails.SecondArtID > 0)
                                // Get and output the full secondary art name
                                classArt.Text += "/" + methodsClass.GetArtTitle(courseDetails.SecondArtID);

                            // Get the class instructor
                            HOTBAL.Instructor getClassInstructor = methodsClass.GetInstructorByID(courseDetails.InstructorID);

                            // Did we get an error?
                            if (String.IsNullOrEmpty(getClassInstructor.Error))
                                // Output the instructor name
                                classInstructor.Text = getClassInstructor.FirstName + " " + getClassInstructor.LastName;
                            else
                                // Output the error message
                                classInstructor.Text = getClassInstructor.Error;

                            // Output the class title
                            classTitle.Text = courseDetails.Title;

                            // Create the printable roster link
                            printRoster.Text = "<a href='" + HOTBAL.SDAConstants.CLASS_PRINT_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString() + "'>Printable Class Roster</a>";

                            // Build the add student link
                            addStudent.NavigateUrl = HOTBAL.SDAConstants.CLASS_STUDENT_ADD_INTERNAL_URL + "?ID=" + courseDetails.ID.ToString();

                            // Get students registered for this class
                            List<HOTBAL.Student> courseStudents = methodsClass.GetStudentsByClass(courseDetails.ID);

                            // Did we get the list of students back?
                            if (courseStudents.Count > 0)
                            {
                                // Did we get an error message back?
                                if (String.IsNullOrEmpty(courseStudents[0].Error))
                                {
                                    // Loop through the returned list of students
                                    foreach (HOTBAL.Student student in courseStudents)
                                    {
                                        // Start a new table row
                                        classRoster.Text += "<tr><td>";

                                        // Is this student checked in to this class?
                                        bool isCheckedIn = methodsClass.IsStudentCheckedIn(Convert.ToInt32(Request.QueryString["ID"].ToString()), student.ID);

                                        if (!isCheckedIn)
                                            // Output a link to allow the student to check in
                                            classRoster.Text += "<a href='" + HOTBAL.SDAConstants.STUDENT_CHECK_IN_INTERNAL_URL +
                                                "?ID=" + student.ID + "&CID=" + Request.QueryString["ID"].ToString() +
                                                "'>Check In</a></td>";
                                        else
                                            // Student is already checked in
                                            classRoster.Text += "Checked In</td>";

                                        // Output the student name with a link to their information and notes about the student
                                        classRoster.Text += "<td><a href='" + HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL +
                                            "?ID=" + student.ID + "'>" + student.LastName + ", " + student.FirstName +
                                            "</a></td><td>" + student.Note + "</td></tr>";
                                    }
                                }
                                else
                                    // Output the received error message
                                    errorLabel.Text = courseStudents[0].Error;
                            }
                            else
                                // Output the no students message
                                classRoster.Text = "<tr><td colspan='3'>" + HOTBAL.SDAMessages.NO_STUDENTS_CLASS + "</td></tr>";
                        }
                        else
                            // Output the error message
                            errorLabel.Text = HOTBAL.SDAMessages.NO_CLASS;
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
                functionsClass.SendErrorMail("ClassDetails: PageLoad", ex, "");
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
            }
        }
    }
}