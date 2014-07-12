using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page allows for information about a new student to 
    ///     be collected and a new student added to the system.
    /// </summary>
    public partial class StudentAdd : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This process sets up the new student page.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Build the title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Add Student";

            // Default the birthdate to today's date
            birthdayDate.Text = functionsClass.FormatSlash(DateTime.Now);

            // Populate the drop down list of available arts
            populateArt();
        }

        /// <summary>
        /// Populates the arts drop down with the currently offered
        ///     martial arts/programs.
        /// </summary>
        private void populateArt()
        {
            // Clear the drop downs
            artList.Items.Clear();

            // Add in the default "Choose" option
            artList.Items.Add(new ListItem("-Choose An Art-", "0"));

            // Get the list of available arts
            List<HOTBAL.Art> allArts = methodsClass.GetAllSDAArts();

            // Did we get the list of arts?
            if (allArts.Count > 0)
            {
                // Did we get an error when getting the arts?
                if (String.IsNullOrEmpty(allArts[0].Error))
                {
                    // Loop through the list of returned arts
                    foreach (HOTBAL.Art art in allArts)
                    {
                        // Add the art to the art list
                        artList.Items.Add(new ListItem(art.Title, art.ID.ToString()));
                    }
                }
                else
                {
                    // Set up the error label and output the received error
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = allArts[0].Error;
                }
            }
            else
            {
                // Set up the error label and output the error message
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.SDAMessages.NO_ARTS;
            }
        }

        /// <summary>
        /// This is the onClick event for adding a student. Once the information
        ///     is validated, the student information is saved and the user is 
        ///     directed to the student information page.
        /// </summary>
        protected void addStudent_Click(object sender, EventArgs e)
        {
            // Was the page valid?
            if (Page.IsValid)
            {
                // Build the error message label
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");

                // Set up the default student Id
                long studentID = 0;

                // Build the full payment plan
                string paymentPlan = (intervalCount.Text + " " + paymentInterval.SelectedValue).Trim();

                try
                {
                    // Add the student information
                    studentID = methodsClass.AddNewStudent(functionsClass.CleanUp(firstName.Text), functionsClass.CleanUp(lastName.Text),
                        functionsClass.CleanUp(address.Text), functionsClass.CleanUp(city.Text), functionsClass.CleanUp(state.Text),
                        functionsClass.CleanUp(zipCode.Text), functionsClass.CleanUp(emergencyContact.Text), Convert.ToDateTime(birthdayDate.Text), DateTime.Now,
                        paymentPlan, Convert.ToDouble(paymentAmount.Text), Convert.ToInt32(artList.SelectedValue));

                    // Did we get a valid student Id back?
                    if (studentID == 0)
                        // Output the error message
                        errorLabel.Text = HOTBAL.SDAMessages.ERROR_ADD_STUDENT;
                    else
                        // Redirect to the new student information
                        Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + studentID);
                }
                catch (Exception ex)
                {
                    // Send the error and output the standard message
                    functionsClass.SendErrorMail("AddStudent: onClick", ex, "");
                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                }
            }
        }
    }
}