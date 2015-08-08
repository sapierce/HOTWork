using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page allows a new art to be added to a student. It allows the art,
    ///     belt, and tip or class count to be set initially.
    /// </summary>
    public partial class StudentAddArt : System.Web.UI.Page
    {
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This is the initial method for the page. It determines if we have received a student
        ///     ID and if so, builds the list of available arts.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Build the title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Add Student Art";

            // Is this a page postback?
            if (!Page.IsPostBack)
            {
                // Set up the error label
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");

                // Does the ID element exist?
                if (Request.QueryString["ID"] != null)
                    // Does the ID element have a value?
                    if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                        // Populate the arts
                        populateArts();
                    else
                        // Output the error message
                        errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENTS;
                else
                    // Output the error message
                    errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENTS;
            }
        }

        /// <summary>
        /// Populates the arts drop down with the currently offered
        ///     martial arts/programs.
        /// </summary>
        private void populateArts()
        {
            // Clear the drop downs
            studentArt.Items.Clear();

            // Add in the default "Choose" option
            studentArt.Items.Add(new ListItem("-Choose An Art-", "0"));

            // Get the list of available arts
            List<HOTBAL.Art> artList = methodsClass.GetAllSDAArts();

            // Did we get the list of arts?
            if (artList.Count > 0)
            {
                // Did we get an error when getting the arts?
                if (String.IsNullOrEmpty(artList[0].ErrorMessage))
                {
                    // Loop through the list of returned arts
                    foreach (HOTBAL.Art art in artList)
                    {
                        // Add the art to the first and second art lists
                        studentArt.Items.Add(new ListItem(art.ArtTitle, art.ArtId.ToString()));
                    }
                }
                else
                {
                    // Set up the error label and output the received error
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = artList[0].ErrorMessage;
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
        /// This is the onChange event for the studentArt drop down. Once an art
        ///     has been selected, this method gets the available belts for that
        ///     art and populates the studentBelt drop down with the values.
        /// </summary>
        protected void studentArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the drop down
            studentBelt.Items.Clear();

            // Add in the default "Choose" option
            studentBelt.Items.Add(new ListItem("-Choose a Belt-", "-1"));

            // Get the list of available belts
            List<HOTBAL.Belt> beltList = methodsClass.GetArtBelts(Convert.ToInt32(studentArt.SelectedValue.ToString()));

            // Did we get the list of belts?
            if (beltList.Count > 0)
            {
                // Did we get an error when getting the belts?
                if (String.IsNullOrEmpty(beltList[0].ErrorMessage))
                {
                    // Loop through the list of returned belts
                    foreach (HOTBAL.Belt belt in beltList)
                    {
                        // Add each belt to the drop down list
                        studentBelt.Items.Add(new ListItem(belt.BeltTitle, belt.BeltId.ToString()));
                    }
                }
                else
                {
                    // Set up the error label and output the received error
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = beltList[0].ErrorMessage;
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
        /// This is the onChange event for the studentBelt drop down. Once a belt
        ///     has been selected, this method determines if this belt uses tips or
        ///     class counts and displays options based on that criteria.
        /// </summary>
        protected void studentBelt_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the belt detail information
            HOTBAL.Belt beltList = methodsClass.GetBeltByID(Convert.ToInt32(studentBelt.SelectedValue.ToString()));

            // Did we get an error getting the belt information?
            if (String.IsNullOrEmpty(beltList.ErrorMessage))
            {
                // Did we get a valid belt?
                if (beltList.BeltId > 0)
                {
                    // Does this belt use tips or class counts?
                    if (beltList.ClassOrTip == "T")
                    {
                        // Set the label
                        tipOrClass.Text = "Tip:";

                        // Hide the class count option
                        classCount.Visible = false;

                        // Show the tip selection option
                        studentTip.Visible = true;

                        // Clear the drop down
                        studentTip.Items.Clear();

                        // Add in the default "Choose" option
                        studentTip.Items.Add(new ListItem("-Choose A Tip-", "0"));

                        // Get the tips available for this belt
                        List<HOTBAL.Tip> tipList = methodsClass.GetBeltTips(Convert.ToInt32(studentBelt.SelectedValue.ToString()));

                        // Did we get any tips?
                        if (tipList.Count > 0)
                        {
                            // Did we get an error getting the tips?
                            if (String.IsNullOrEmpty(tipList[0].ErrorMessage))
                            {
                                // Loop through the returned tips
                                foreach (HOTBAL.Tip tip in tipList)
                                {
                                    // Output the tips to the drop down
                                    studentTip.Items.Add(new ListItem(tip.TipTitle, tip.TipId.ToString()));
                                }
                            }
                            else
                            {
                                // Set up the error label and output the error message
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text = tipList[0].ErrorMessage;
                            }
                        }
                    }
                    else
                    {
                        // Set the label
                        tipOrClass.Text = "Class Count:";

                        // Show the class count option
                        classCount.Visible = true;

                        // Hide the tip selection option
                        studentTip.Visible = false;
                    }
                }
                else
                {
                    // Set up the error label and output the error message
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.SDAMessages.NO_TIP_CLASS;
                }
            }
            else
            {
                // Set up the error label and output the error message
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = beltList.ErrorMessage;
            }
        }

        /// <summary>
        /// This is the submit event for adding an art to a student. Once the page has
        ///     passed validation, the information is saved to the student. If the save
        ///     is successful, the user is redirected back to the student information.
        /// </summary>
        protected void addArt_Click(Object sender, EventArgs e)
        {
            // Was the page valid?
            if (Page.IsValid)
            {
                // Add the art to the student
                bool response = methodsClass.AddStudentArt(Convert.ToInt32(Request.QueryString["ID"].ToString()),
                    Convert.ToInt32(studentArt.SelectedValue), Convert.ToInt32(studentBelt.SelectedValue),
                    Convert.ToInt32(studentTip.SelectedValue), Convert.ToInt32(classCount.Text),
                    DateTime.Now, artUpdater.Text);

                // Was the save successful?
                if (response)
                    // Redirect to the student information page
                    Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString());
                else
                {
                    // Set up the error label and output the error message
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_ADD_STUDENT_ART;
                }
            }
        }
    }
}