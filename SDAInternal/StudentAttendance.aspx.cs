using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class StudentAttendancePage : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This page outputs a students attendance history for a given class.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the page title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Student Attendance";

            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            try
            {
                // Does the ID element exist?
                if (Request.QueryString["ID"] != null)
                {
                    // Does the CID element exist?
                    if (Request.QueryString["CID"] != null)
                    {
                        // Do the ID and CID elements have values?
                        if ((!String.IsNullOrEmpty(Request.QueryString["ID"].ToString())) && (!String.IsNullOrEmpty(Request.QueryString["CID"].ToString())))
                        {
                            // Get the student information
                            HOTBAL.Student studentInformation = methodsClass.GetStudentInformation(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                            // Did we get an error?
                            if (String.IsNullOrEmpty(studentInformation.Error))
                            {
                                // Output the student name
                                studentName.Text = studentInformation.FirstName + " " + studentInformation.LastName;

                                // Get the course information
                                HOTBAL.Course courseInformation = methodsClass.GetCourseInformation(Convert.ToInt32(Request.QueryString["CID"].ToString()));

                                // Did we get an error?
                                if (String.IsNullOrEmpty(courseInformation.Error))
                                {
                                    // Output the course title
                                    courseName.Text = courseInformation.Title;

                                    // Get the student attendance
                                    List<HOTBAL.ClassAttendance> studentAttend = methodsClass.GetStudentAttendance(Convert.ToInt32(Request.QueryString["ID"].ToString()), Convert.ToInt32(Request.QueryString["CID"].ToString()));

                                    // Did we get records?
                                    if (studentAttend.Count > 0)
                                    {
                                        // Bind the returned records to the datagrid
                                        attendanceList.DataSource = studentAttend;
                                        attendanceList.DataBind();
                                    }
                                    else
                                        // Output the error message
                                        errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENT_ATTEND;
                                }
                                else
                                    // Output the error message
                                    errorLabel.Text = HOTBAL.SDAMessages.NO_CLASS;
                            }
                            else
                                // Output the error message
                                errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENT_FOUND;
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
                    errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENTS;
            }
            catch (Exception ex)
            {
                // Send the error and output the standard message
                functionsClass.SendErrorMail("StudentAttendance: PageLoad", ex, "");
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_STUDENT_CHECK;
            }
        }
    }
}