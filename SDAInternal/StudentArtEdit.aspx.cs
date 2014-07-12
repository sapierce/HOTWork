using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page allows student arts to be updated with new
    ///     belts, tips or class counts
    /// </summary>
    public partial class StudentArtEdit : System.Web.UI.Page
    {
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This is the initial method which validates that we have a reference
        ///     id, and then retrieves the associated information.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the page title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Edit Student Art";

            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            // Is this a postback?
            if (!Page.IsPostBack)
            {
                // Does the XID element exist?
                if (Request.QueryString["XID"] != null)
                {
                    // Does the XID element have a value?
                    if (!String.IsNullOrEmpty(Request.QueryString["XID"]))
                    {
                        // Get the information associated with this id
                        HOTBAL.StudentArt artResponse = methodsClass.GetStudentArt(Convert.ToInt32(Request.QueryString["XID"].ToString()));

                        // Did we get an error?
                        if (!String.IsNullOrEmpty(artResponse.Error))
                        {
                            // Populate the arts and select the current art
                            populateArts(artResponse.ArtID);

                            // Populate the belts and select the current belt
                            populateBelts(artResponse.ArtID, artResponse.BeltID);

                            // Is the current belt tips or classes?
                            if (artResponse.ClassOrTip == "T")
                            {
                                // Hide the class count
                                classCount.Visible = false;

                                // Show the tips drop down
                                studentTip.Visible = true;

                                // Populate the tips and select the current tip
                                populateTips(artResponse.BeltID, artResponse.TipID);
                            }
                            else
                            {
                                // Show the class count
                                classCount.Visible = true;

                                // Hide the tips drop down
                                studentTip.Visible = false;
                            }
                        }
                        else
                            // Output the received error
                            errorLabel.Text = artResponse.Error;
                    }
                    else
                        // Output the received error
                        errorLabel.Text = HOTBAL.SDAMessages.NO_ARTS;
                }
                else
                    // Output the received error
                    errorLabel.Text = HOTBAL.SDAMessages.NO_ARTS;
            }
        }

        /// <summary>
        /// This is the onChange event for the studentBelt drop down. When a
        ///     new belt is selected, the belt detail information is retrieved
        ///     and either the class count entry or the tips drop down displayed.
        /// </summary>
        protected void studentBelt_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the belt detail information
            HOTBAL.Belt beltDetail = methodsClass.GetBeltByID(Convert.ToInt32(studentBelt.SelectedValue.ToString()));

            // Did we get an error message?
            if (String.IsNullOrEmpty(beltDetail.Error))
            {
                // Does this belt use tips of classes?
                if (beltDetail.ClassOrTip == "T")
                {
                    // Hide the class count
                    classCount.Visible = false;

                    // Show the tips drop down
                    studentTip.Visible = true;

                    // Populate the tips associated with the new belt
                    populateTips(Convert.ToInt32(studentBelt.SelectedValue), 0);
                }
                else
                {
                    // Show the class count
                    classCount.Visible = true;

                    // Hide the tips drop down
                    studentTip.Visible = false;
                }
            }
            else
            {
                // Set up the error label and output the received error
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = beltDetail.Error;
            }
        }

        /// <summary>
        /// This is the onClick event for the editArt button. Once the page has
        ///     been validated, we check to see if we need to update the current
        ///     record, or mark it complete and create a new record. If this is
        ///     successful, the user is directed back to the student information
        ///     page.
        /// </summary>
        protected void editArt_Click(Object sender, EventArgs e)
        {
            // Set the initial response value
            bool response = false;

            // Get the current associated record
            HOTBAL.StudentArt artResponse = methodsClass.GetStudentArt(Convert.ToInt32(Request.QueryString["XID"].ToString()));

            // Did the belt change? Did the tip change?
            if ((studentBelt.SelectedValue != artResponse.BeltID.ToString()) || (studentTip.SelectedValue != artResponse.TipID.ToString()))
            {
                // Automatically mark current record complete and generate a new row
                response = methodsClass.UpdateStudentArt(Convert.ToInt32(Request.QueryString["ID"].ToString()), Convert.ToInt32(Request.QueryString["XID"].ToString()),
                    Convert.ToInt32(studentArt.SelectedValue), Convert.ToInt32(studentBelt.SelectedValue), Convert.ToInt32(studentTip.SelectedValue), Convert.ToInt32(classCount.Text),
                    DateTime.Now, artUpdater.Text, "C");
            }
            else
            {
                // Update the current row
                response = methodsClass.UpdateStudentArt(Convert.ToInt32(Request.QueryString["ID"].ToString()), Convert.ToInt32(Request.QueryString["XID"].ToString()),
                    Convert.ToInt32(studentArt.SelectedValue), Convert.ToInt32(studentBelt.SelectedValue), Convert.ToInt32(studentTip.SelectedValue), Convert.ToInt32(classCount.Text),
                    DateTime.Now, artUpdater.Text, "U");
            }

            // Was the save successful?
            if (response)
                // Redirect to the student information page
                Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString());
            else
            {
                // Set up the error label and output the received error
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
            }
        }

        /// <summary>
        /// Populates the arts drop down with the currently offered
        ///     martial arts/programs.
        /// </summary>
        /// <param name="artId"></param>
        private void populateArts(int artId)
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
                if (String.IsNullOrEmpty(artList[0].Error))
                {
                    // Loop through the list of returned arts
                    foreach (HOTBAL.Art art in artList)
                    {
                        // Add the art to the first and second art lists
                        studentArt.Items.Add(new ListItem(art.Title, art.ID.ToString()));
                    }

                    // Select the current art as the default
                    studentArt.Items.FindByValue(artId.ToString()).Selected = true;
                }
                else
                {
                    // Set up the error label and output the received error
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = artList[0].Error;
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
        /// This method populates the studentBelt drop down. Once an art
        ///     has been selected, this method gets the available belts for that
        ///     art and populates the studentBelt drop down with the values.
        /// </summary>
        /// <param name="artId"></param>
        /// <param name="beltId"></param>
        private void populateBelts(int artId, int beltId)
        {
            // Clear the drop down
            studentBelt.Items.Clear();

            // Add in the default "Choose" option
            studentBelt.Items.Add(new ListItem("-Choose a Belt-", "-1"));

            // Get the list of available belts
            List<HOTBAL.Belt> beltList = methodsClass.GetArtBelts(artId);

            // Did we get the list of belts?
            if (beltList.Count > 0)
            {
                // Did we get an error when getting the belts?
                if (String.IsNullOrEmpty(beltList[0].Error))
                {
                    // Loop through the list of returned belts
                    foreach (HOTBAL.Belt belt in beltList)
                    {
                        // Add each belt to the drop down list
                        studentBelt.Items.Add(new ListItem(belt.Title, belt.ID.ToString()));
                    }

                    // Select the current belt as the default
                    studentBelt.Items.FindByValue(beltId.ToString()).Selected = true;
                }
                else
                {
                    // Set up the error label and output the received error
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = beltList[0].Error;
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
        /// This method populates the studentTips drop down with tips
        ///     associated with the selected belt. If a tips is already
        ///     active, it is set to the default.
        /// </summary>
        /// <param name="artID"></param>
        /// <param name="beltID"></param>
        /// <param name="TipID"></param>
        private void populateTips(int beltID, int tipID)
        {
            // Clear the drop down
            studentTip.Items.Clear();

            // Set the "Choose" default
            studentTip.Items.Add(new ListItem("-Choose A Tip-", "0"));

            // Get the tips associated with this belt
            List<HOTBAL.Tip> tipList = methodsClass.GetBeltTips(beltID);

            // Did we get any records
            if (tipList.Count > 0)
            {
                // Did we get an error?
                if (String.IsNullOrEmpty(tipList[0].Error))
                {
                    // Loop through the returned tips
                    foreach (HOTBAL.Tip tip in tipList)
                    {
                        // Add the tip to the drop down list
                        studentTip.Items.Add(new ListItem(tip.Title, tip.ID.ToString()));
                    }

                    // Set the existing tip as the default
                    studentTip.Items.FindByValue(tipID.ToString()).Selected = true;
                }
                else
                {
                    // Set up the error label and output the received error
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = tipList[0].Error;
                }
            }
        }
    }
}