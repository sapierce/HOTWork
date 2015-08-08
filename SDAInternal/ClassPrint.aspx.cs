using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page outputs a printable version of detail information about a selected class
    ///     or lesson. It lists primary information as well as a list of students. For each
    ///     student, it is noted if they are checked in or not as well as general notes.
    /// </summary>
    public partial class ClassPrint : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// The initial loading method. The class ID is passed in via QueryString and information
        ///     retrieved based on that ID.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
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
                        if (courseDetails.CourseId > 0)
                        {
                            // Get and output the full art name
                            classArt.Text = methodsClass.GetArtTitle(courseDetails.FirstArtId);

                            // Is there a secondary art?
                            if (courseDetails.SecondArtId > 0)
                                // Get and output the full secondary art name
                                classArt.Text += "/" + methodsClass.GetArtTitle(courseDetails.SecondArtId);

                            // Get the class instructor
                            HOTBAL.Instructor getClassInstructor = methodsClass.GetInstructorByID(courseDetails.InstructorId);

                            // Did we get an error?
                            if (String.IsNullOrEmpty(getClassInstructor.ErrorMessage))
                                // Output the instructor name
                                classInstructor.Text = getClassInstructor.FirstName + " " + getClassInstructor.LastName;
                            else
                                // Output the error message
                                classInstructor.Text = getClassInstructor.ErrorMessage;

                            // Output the class title
                            classTitle.Text = courseDetails.CourseTitle;

                            // Get students registered for this class
                            List<HOTBAL.Student> courseStudents = methodsClass.GetStudentsByClass(courseDetails.CourseId);

                            // Did we get the list of students back?
                            if (courseStudents.Count > 0)
                            {
                                // Did we get an error message back?
                                if (String.IsNullOrEmpty(courseStudents[0].ErrorMessage))
                                {
                                    // Loop through the returned list of students
                                    foreach (HOTBAL.Student student in courseStudents)
                                    {
                                        // Start a new table row
                                        printRoster.Text += "<tr><td>";

                                        // Is this student checked in to this class?
                                        bool isCheckedIn = methodsClass.IsStudentCheckedIn(Convert.ToInt32(Request.QueryString["ID"].ToString()), student.StudentId);

                                        if (isCheckedIn)
                                            // Student is already checked in
                                            printRoster.Text += "Checked In</td>";
                                        else
                                            // Student is not checked in
                                            printRoster.Text += "Not Checked In</td>";

                                        // Output the student name and notes about the student
                                        printRoster.Text += "<td>" + student.LastName + ", " + student.FirstName + 
                                            (String.IsNullOrEmpty(student.Suffix) ? "" : " " + student.Suffix) + "</td><td>" + student.Note + "</td>";

                                        // Get student information for this art
                                        HOTBAL.StudentArt artResponse = methodsClass.GetStudentArtByID(courseDetails.FirstArtId, student.StudentId);

                                        // Did we get an error message?
                                        if (String.IsNullOrEmpty(artResponse.ErrorMessage))
                                            // Output the student's current belt and tip/class count
                                            printRoster.Text += "<td>" + artResponse.BeltTitle + "</td><td>" +
                                                (artResponse.ClassOrTip == "T" ? artResponse.TipTitle : artResponse.ClassCount.ToString()) + "</td>";
                                        else
                                            // Output the received error message
                                            errorMessage.Text = artResponse.ErrorMessage;

                                        // Is there a second art>
                                        if (courseDetails.SecondArtId != 0)
                                        {
                                            // Get student information for this art
                                            HOTBAL.StudentArt art2Response = methodsClass.GetStudentArtByID(courseDetails.SecondArtId, student.StudentId);

                                            // Did we get an error message?
                                            if (String.IsNullOrEmpty(art2Response.ErrorMessage))
                                                // Output the student's current belt and tip/class count
                                                printRoster.Text += "<td>" + art2Response.BeltTitle + "</td><td>" +
                                                    (art2Response.ClassOrTip == "T" ? art2Response.TipTitle : art2Response.ClassCount.ToString()) + "</td>";
                                            else
                                                // Output the received error message
                                                errorMessage.Text = art2Response.ErrorMessage;
                                        }
                                        else
                                            // Output empty cells
                                            printRoster.Text += "<td><br /></td><td><br /></td>";

                                        // Finish the row
                                        printRoster.Text += "</tr>";
                                    }
                                }
                                else
                                    // Output the received error message
                                    errorMessage.Text = courseStudents[0].ErrorMessage;
                            }
                            else
                                // Output the no students message
                                printRoster.Text = "<tr><td colspan='7'>" + HOTBAL.SDAMessages.NO_STUDENTS_CLASS + "</td></tr>";
                        }
                        else
                            // Output the error message
                            errorMessage.Text = HOTBAL.SDAMessages.NO_CLASS;
                    }
                    else
                        // Output the error message
                        errorMessage.Text = HOTBAL.SDAMessages.NO_CLASS;
                }
                else
                    // Output the error message
                    errorMessage.Text = HOTBAL.SDAMessages.NO_CLASS;
            }
            catch (Exception ex)
            {
                // Send the error and output the standard message
                functionsClass.SendErrorMail("ClassPrint: PageLoad", ex, "");
                errorMessage.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
            }
        }
    }
}