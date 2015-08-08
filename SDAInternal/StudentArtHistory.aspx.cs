using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page displays the students art history.
    /// </summary>
    public partial class StudentArtHistory : System.Web.UI.Page
    {
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This method validates that a student has been selected and then
        ///     outputs the available art history for that student.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Built the page title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Student Art History";

            // Set up the error label and output the error message
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            // Does the ID element exist?
            if (Request.QueryString["ID"] != null)
            {
                // Does the ID element have a value?
                if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    // Get the student information
                    HOTBAL.Student studentResponse = methodsClass.GetStudentInformation(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                    // Set the student name
                    studentName.Text = studentResponse.FirstName + " " + studentResponse.LastName;

                    // Get the students completed arts
                    List<HOTBAL.StudentArt> studentNewArts = methodsClass.GetStudentCompletedArts(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                    // Did we get any records?
                    if (studentNewArts.Count > 0)
                    {
                        // Did we get an error?
                        if (String.IsNullOrEmpty(studentNewArts[0].ErrorMessage))
                        {
                            // Loop through the returned records
                            foreach (HOTBAL.StudentArt a in studentNewArts)
                            {
                                // Output each history records
                                currentHistory.Text += "<tr><td>"
                                    + a.CompletionDate + "</td><td>" + a.ArtTitle + "</td><td>"
                                    + a.BeltTitle + "</td><td>" + a.TipTitle + "</td></tr>";
                            }
                        }
                        else
                            // Output the error message
                            errorLabel.Text = studentNewArts[0].ErrorMessage;
                    }
                    else
                        // Output that no history is available
                        currentHistory.Text = "<tr><td colspan='4'><b>No Current History Available</b></td></tr>";

                    // Get students previous completed arts
                    List<HOTBAL.StudentArt> studentOldArts = methodsClass.GetStudentCompletedArtsPrevious(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                    // Did we get any records?
                    if (studentOldArts.Count > 0)
                    {
                        // Did we get an error?
                        if (String.IsNullOrEmpty(studentOldArts[0].ErrorMessage))
                        {
                            // Loop through the returned records
                            foreach (HOTBAL.StudentArt a in studentOldArts)
                            {
                                // Output each history records
                                previousHistory.Text += "<tr><td>"
                                    + a.CompletionDate + "</td><td>" + a.ArtTitle + "</td><td>"
                                    + a.BeltTitle + "</td><td>" + a.TipTitle + "</td></tr>";
                            }
                        }
                        else
                            // Output the error message
                            errorLabel.Text = studentOldArts[0].ErrorMessage;
                    }
                    else
                        // Output that no history is available
                        previousHistory.Text = "<tr><td colspan='4'><b>No Previous History Available</b></td></tr>";
                }
                else
                    // Output the error message
                    errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENT_FOUND;
            }
            else
                // Output the error message
                errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENT_FOUND;
        }
    }
}