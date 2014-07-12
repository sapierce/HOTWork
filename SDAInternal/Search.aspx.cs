using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page allows for searching of students by entered first/last name,
    ///     and by first letter of the last name.
    /// </summary>
    public partial class Search : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This method sets up the page to show/hide panels as
        ///     needed initially.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Build the title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Search";

            // Is this a postback?
            if (!Page.IsPostBack)
            {
                // Show the main search panel
                mainSearch.Visible = true;

                // Hide the results panel
                searchResults.Visible = false;
            }
        }

        /// <summary>
        /// This method searches for students based on entered first and
        ///     last name characters.
        /// </summary>
        protected void searchByName_Click(Object sender, System.EventArgs e)
        {
            // Was the page valid?
            if (Page.IsValid)
            {
                // Set up the error label
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                try
                {
                    // Hide the main search panel
                    mainSearch.Visible = false;

                    // Display the results panel
                    searchResults.Visible = true;

                    // Get the list of students matching the entered criteria
                    List<HOTBAL.Student> studentList = sqlClass.GetStudentsByName(firstName.Text, lastName.Text, 1);

                    // Did we get any students?
                    if (studentList.Count > 0)
                    {
                        // Did we get any error message?
                        if (String.IsNullOrEmpty(studentList[0].Error))
                        {
                            // Loop through the returned students
                            foreach (HOTBAL.Student student in studentList)
                            {
                                // Output a new table for each student with a link to their information
                                searchResultsOutput.Text += "<tr><td><a href='" + HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL +
                                    "?ID=" + student.ID.ToString() + "'>" + student.LastName + ", " + student.FirstName + "</a></td></tr>";
                            }
                        }
                        else
                            // Output the received error
                            errorLabel.Text = studentList[0].Error;
                    }
                    else
                        // Output the error message
                        errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENTS;
                }
                catch (Exception ex)
                {
                    // Send the error and output the standard message
                    functionsClass.SendErrorMail("Search: byName", ex, "");
                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                }
            }
        }

        /// <summary>
        /// This method searches for students based on the first letter of their
        ///     last name.
        /// </summary>
        protected void customerLetter_Click(Object sender, System.EventArgs e)
        {
            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            try
            {
                // Hide the main search panel
                mainSearch.Visible = false;

                // Display the search results panel
                searchResults.Visible = true;

                // Transform the input into a LinkButton
                LinkButton button = (LinkButton)sender;

                // Get the list of students who match the criteria
                List<HOTBAL.Student> studentList = sqlClass.GetStudentsByLastName(button.Text, 1);

                // Did we get any students?
                if (studentList.Count > 0)
                {
                    // Did we get an error?
                    if (String.IsNullOrEmpty(studentList[0].Error))
                    {
                        // Loop through the list of returned students
                        foreach (HOTBAL.Student student in studentList)
                        {
                            // Output a new table for each student with a link to their information
                            searchResultsOutput.Text += "<tr><td><a href='" + HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL +
                                "?ID=" + student.ID.ToString() + "'>" + student.LastName + ", " + student.FirstName + "</a></td></tr>";
                        }
                    }
                    else
                        // Output the received error
                        errorLabel.Text = studentList[0].Error;
                }
                else
                    // Output the error message
                    errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENTS;
            }
            catch (Exception ex)
            {
                // Send the error and output the standard message
                functionsClass.SendErrorMail("Search: letterClick", ex, "");
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
            }
        }
    }
}