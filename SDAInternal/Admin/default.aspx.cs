using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class _default : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Administration";

            if (!Page.IsPostBack)
            {
                // Load defaults
                string currentDate = FunctionsClass.FormatDash(DateTime.Now);
                if (Request.QueryString["Date"] != null)
                    currentDate = Request.QueryString["Date"].ToString();

                transactionStartDate.Text = currentDate;
                transactionEndDate.Text = currentDate;
                birthdayDays.Text = "14";
                passBeginDate.Text = currentDate;
                passEndDate.Text = currentDate;
                lastTipDate.Text = currentDate;
                attendanceDate.Text = currentDate;

                // Load drop downs
                populateArts();
                populateBelts();
                populateTips();
                populateCourses();
                populateTerms();
                populateItems();
                populateInstructors();
            }
        }

        #region onClicks
        //Adds
        public void addInstructor_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_INST_INTERNAL_URL + "?Action=add");
        }

        public void addArt_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_ART_INTERNAL_URL + "?Action=add");
        }

        public void addBelt_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_BELT_INTERNAL_URL + "?Action=add");
        }

        public void addTip_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_TIP_INTERNAL_URL + "?Action=add");
        }

        public void addTerm_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_TERM_INTERNAL_URL + "?Action=add");
        }

        public void addItem_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_ITEM_INTERNAL_URL + "?Action=add");
        }

        //Edits
        public void editInstructor_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_INST_INTERNAL_URL + "?Action=edit&ID=" + instructorSelection.SelectedValue.ToString());
        }

        public void editArt_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_ART_INTERNAL_URL + "?Action=edit&ID=" + artSelection.SelectedValue);
        }

        public void editBelt_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_BELT_INTERNAL_URL + "?Action=edit&ID=" + beltSelection.SelectedValue);
        }

        public void editTip_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_TIP_INTERNAL_URL + "?Action=edit&ID=" + tipSelection.SelectedValue);
        }

        public void editTerm_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_TERM_INTERNAL_URL + "?Action=edit&ID=" + termSelection.SelectedValue);
        }

        public void editItem_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_ITEM_INTERNAL_URL + "?Action=edit&ID=" + itemSelection.SelectedValue);
        }

        public void editCourse_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.ADMIN_CLASS_INTERNAL_URL + "?Action=edit&ID=" + courseSelection.SelectedValue);
        }

        //Others
        public void viewFullTransaction_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAPOSConstants.TRANSACTION_LOG_URL + "?StartDate=" + transactionStartDate.Text + "&EndDate=" + transactionEndDate.Text + "&Totals=" + totalsOnly.Checked.ToString(), false);
        }

        public void viewInventory_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.RPT_INVENTORY_INTERNAL_URL);
        }

        public void birthdayReport_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.RPT_BIRTHDAY_INTERNAL_URL + "?d=" + birthdayDays.Text);
        }

        public void beltsPassed_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.RPT_PASS_BELT_INTERNAL_URL + "?endDate=" + passEndDate.Text + "&beginDate=" + passBeginDate.Text);
        }

        public void lastTip_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.RPT_LAST_TIP_INTERNAL_URL + "?d=" + lastTipDate.Text);
        }

        public void classAttendance_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.RPT_ATTENDANCE_INTERNAL_URL + "?d=" + attendanceDate.Text);
        }

        public void activeStudents_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.SDAConstants.RPT_ACTIVE_STUDENTS_INTERNAL_URL);
        }
        #endregion

        private void populateArts()
        {
            artSelection.Items.Clear();
            artSelection.Items.Add(new ListItem("-Choose An Art-", "0"));

            List<HOTBAL.Art> artsList = sqlClass.GetAllSDAArts();

            if (artsList != null)
            {
                if (artsList.Count > 1)
                {
                    if (String.IsNullOrEmpty(artsList[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Art art in artsList)
                        {
                            artSelection.Items.Add(new ListItem(art.ArtTitle, art.ArtId.ToString()));
                        }
                    }
                    else
                        buildErrorMessage(artsList[0].ErrorMessage);
                }
            }
        }

        private void populateBelts()
        {
            beltSelection.Items.Clear();
            beltSelection.Items.Add(new ListItem("-Choose A Belt-", "0"));

            List<HOTBAL.Belt> beltsList = sqlClass.GetAllSDABelts();

            if (beltsList != null)
            {
                if (beltsList.Count > 0)
                {
                    if (string.IsNullOrEmpty(beltsList[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Belt belt in beltsList)
                        {
                            beltSelection.Items.Add(new ListItem(sqlClass.GetArtTitle(belt.ArtId) + "-" + belt.BeltTitle, belt.BeltId.ToString()));
                        }
                    }
                    else
                        buildErrorMessage(beltsList[0].ErrorMessage);
                }
            }
        }

        private void populateTips()
        {
            tipSelection.Items.Clear();
            tipSelection.Items.Add(new ListItem("-Choose A Tip-", "0"));

            List<HOTBAL.Tip> tipsList = sqlClass.GetAllTips();

            if (tipsList != null)
            {
                if (tipsList.Count > 0)
                {
                    if (string.IsNullOrEmpty(tipsList[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Tip tip in tipsList)
                        {
                            tipSelection.Items.Add(new ListItem(tip.TipTitle, tip.TipId.ToString()));
                        }
                    }
                    else
                        buildErrorMessage(tipsList[0].ErrorMessage);
                }
            }
        }

        private void populateCourses()
        {
            courseSelection.Items.Clear();
            courseSelection.Items.Add(new ListItem("-Choose A Class-", "0"));

            List<HOTBAL.Course> coursesList = sqlClass.GetAllActiveRepeatingClasses();

            if (coursesList != null)
            {
                if (coursesList.Count > 0)
                {
                    if (String.IsNullOrEmpty(coursesList[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Course course in coursesList)
                        {
                            courseSelection.Items.Add(new ListItem((course.CourseTitle + " (" + sqlClass.GetArtTitle(course.FirstArtId) +
                                (course.SecondArtId == 0 ? "" : "/" + sqlClass.GetArtTitle(course.SecondArtId)) + ") - " + course.Day + " - " + course.Time), course.CourseId.ToString()));
                        }
                    }
                    else
                        buildErrorMessage(coursesList[0].ErrorMessage);
                }
            }
        }

        private void populateTerms()
        {
            termSelection.Items.Clear();
            termSelection.Items.Add(new ListItem("-Choose A Term-", "0"));

            List<HOTBAL.Term> termsList = sqlClass.GetAllTerms();

            if (termsList != null)
            {
                if (termsList.Count > 0)
                {
                    if (String.IsNullOrEmpty(termsList[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Term term in termsList)
                        {
                            termSelection.Items.Add(new ListItem(term.EnglishTerm, term.TermId.ToString()));
                        }
                    }
                    else
                        buildErrorMessage(termsList[0].ErrorMessage);
                }
            }
        }

        private void populateItems()
        {
            itemSelection.Items.Clear();
            itemSelection.Items.Add(new ListItem("-Choose An Item-", "0"));

            List<HOTBAL.Product> itemsList = new List<HOTBAL.Product>();
            itemsList = sqlClass.GetAllItems();

            if (itemsList != null)
            {
                if (itemsList.Count > 0)
                {
                    if (String.IsNullOrEmpty(itemsList[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Product item in itemsList)
                        {
                            itemSelection.Items.Add(new ListItem(item.ProductName, item.ProductId.ToString()));
                        }
                    }
                    else
                        buildErrorMessage(itemsList[0].ErrorMessage);
                }
            }
        }

        private void populateInstructors()
        {
            instructorSelection.Items.Clear();
            instructorSelection.Items.Add(new ListItem("-Choose An Instructor-", "0"));

            List<HOTBAL.Instructor> instructorList = sqlClass.GetAllInstructors();

            if (instructorList != null)
            {
                if (instructorList.Count > 0)
                {
                    if (String.IsNullOrEmpty(instructorList[0].ErrorMessage))
                    {
                        foreach (HOTBAL.Instructor instructor in instructorList)
                        {
                            instructorSelection.Items.Add(new ListItem(instructor.FirstName + " " + instructor.LastName, instructor.InstructorId.ToString()));
                        }
                    }
                    else
                        buildErrorMessage(instructorList[0].ErrorMessage);
                }
            }
        }

        private void buildErrorMessage(string errorMessage)
        {
            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            if (String.IsNullOrEmpty(errorLabel.Text))
                errorLabel.Text = errorMessage;
            else
                errorLabel.Text += errorMessage;
        }
    }
}