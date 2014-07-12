using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    /// <summary>
    /// This page adds a new class or lesson. Both types collect the art(s), date, time,
    ///     instructor, and if the class/lesson is recurring. If this is a class, the title
    ///     is requested. If this is a lesson, a student needs to be selected.
    /// </summary>
    public partial class ClassAdd : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodsClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// Initial loading method. If the requested date/time are passed in via QueryString,
        ///     those values are set. The drop down lists are populated with the necessary
        ///     information based on the passed in values or the current date.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Build the title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Add Class/Lesson";

            // Is this a PostBack?
            if (!Page.IsPostBack)
            {
                // Set up the error label
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                try
                {
                    // Populate the instructors list
                    populateInstructors();

                    // Populate the arts list
                    populateArts();

                    // Populate the students list
                    populateStudents();

                    // Did we get a date and time element in the QueryString?
                    if ((Request.QueryString["Date"] != null) && (Request.QueryString["Time"] != null))
                    {
                        // If we have date from the QueryString, populate it.
                        //  Otherwise, populate with the current date
                        if (!String.IsNullOrEmpty(Request.QueryString["Date"]))
                            beginDate.Text = functionsClass.FormatSlash(Convert.ToDateTime(Request.QueryString["Date"].ToString()));
                        else
                            beginDate.Text = functionsClass.FormatSlash(DateTime.Now);

                        // Get the times for the selected/current date
                        populateTimes();

                        // If we have time from the QueryString, select it
                        if (!String.IsNullOrEmpty(Request.QueryString["Time"]))
                            startTime.Items.FindByValue(Request.QueryString["Time"]).Selected = true;
                    }
                    else
                    {
                        // No pre-selected date or time, so use current date
                        beginDate.Text = functionsClass.FormatSlash(DateTime.Now);

                        // Get the times for the current date
                        populateTimes();
                    }

                    // Populate the recurring options
                    recurringClass.Items.Add(new ListItem("Not Recurring", "0"));
                    recurringClass.Items.Add(new ListItem(DayOfWeek.Sunday.ToString()));
                    recurringClass.Items.Add(new ListItem(DayOfWeek.Monday.ToString()));
                    recurringClass.Items.Add(new ListItem(DayOfWeek.Tuesday.ToString()));
                    recurringClass.Items.Add(new ListItem(DayOfWeek.Wednesday.ToString()));
                    recurringClass.Items.Add(new ListItem(DayOfWeek.Thursday.ToString()));
                    recurringClass.Items.Add(new ListItem(DayOfWeek.Friday.ToString()));
                    recurringClass.Items.Add(new ListItem(DayOfWeek.Saturday.ToString()));
                }
                catch (Exception ex)
                {
                    // Send the error and output the standard message
                    functionsClass.SendErrorMail("ClassAdd: PageLoad", ex, "");
                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                }
            }
        }

        /// <summary>
        /// Populates the first and second arts drop downs with the currently offered
        ///     martial arts/programs.
        /// </summary>
        private void populateArts()
        {
            // Clear the drop downs
            artFirst.Items.Clear();
            artSecond.Items.Clear();

            // Add in the default "Choose" option
            artFirst.Items.Add(new ListItem("-Choose An Art-", "0"));
            artSecond.Items.Add(new ListItem("-Choose An Art-", "0"));

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
                        artFirst.Items.Add(new ListItem(art.Title, art.ID.ToString()));
                        artSecond.Items.Add(new ListItem(art.Title, art.ID.ToString()));
                    }
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
        /// Populates the instructors drop down with currently active instructors.
        /// </summary>
        private void populateInstructors()
        {
            // Clear the current drop downs
            instructor.Items.Clear();

            // Add in the default "Choose" option
            instructor.Items.Add(new ListItem("-Choose An Instructor-", "0"));

            // Get the list of instructors
            List<HOTBAL.Instructor> instructorList = methodsClass.GetAllInstructors();

            // Did we get the list of instructors?
            if (instructorList.Count > 0)
            {
                // Did we get an error when getting the instructors?
                if (String.IsNullOrEmpty(instructorList[0].Error))
                {
                    // Loop through the list of returned instructors
                    foreach (HOTBAL.Instructor person in instructorList)
                    {
                        // Add the instructor to the list
                        instructor.Items.Add(new ListItem(person.FirstName + " " + person.LastName, person.ID.ToString()));
                    }
                }
                else
                {
                    // Set up the error label and output the received error
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = instructorList[0].Error;
                }
            }
            else
            {
                // Set up the error label and output the error message
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.SDAMessages.NO_INSTRUCTORS;
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

                    // Was a student ID element passed in?
                    if (Request.QueryString["ID"] != null)
                        // Does the student ID element have a value?
                        if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                            // Default to the passed in student ID
                            studentList.Items.FindByValue(Request.QueryString["ID"].ToString()).Selected = true;
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
        /// Populates the times drop down based on the entered date.
        /// </summary>
        private void populateTimes()
        {
            // Clear the current drop downs
            startTime.Items.Clear();

            // Add in the default "Choose" option
            startTime.Items.Insert(0, new ListItem("-Choose-", "0"));

            // Is the begin date set?
            if (String.IsNullOrEmpty(beginDate.Text))
                // Use the current date
                beginDate.Text = DateTime.Now.ToShortDateString();

            // Get the list of times based on the begin date
            List<string> timeList = methodsClass.GetAvailableTimes(Convert.ToDateTime(beginDate.Text));

            // Did we get the list of times?
            if (timeList.Count > 0)
            {
                // Loop through the list of times
                foreach (string time in timeList)
                {
                    // Is the entered begin date today?
                    if (Convert.ToDateTime(beginDate.Text) == DateTime.Now)
                    {
                        // Has the time already passed?
                        if (Convert.ToDateTime(time) > DateTime.Now)
                            // Add the future time to the list
                            startTime.Items.Add(new ListItem(time, time));
                    }
                    else
                        // Add the time to the list
                        startTime.Items.Add(new ListItem(time, time));
                }
            }
            else
            {
                // Set up the error label and output the error message
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.SDAMessages.NO_TIMES;
            }
        }

        /// <summary>
        /// If the begin date changes, call the populateTimes() method to repopulate
        ///     the times list with times valid for the given date.
        /// </summary>
        protected void beginDate_TextChanged(object sender, EventArgs e)
        {
            // Call the times method
            populateTimes();
        }

        /// <summary>
        /// Adds a new lesson with the entered information. It then adds the selected student to the
        ///     lesson and if it is a non-recurring lesson, adds it to the class table.
        /// </summary>
        protected void addLesson_Click(object sender, EventArgs e)
        {
            // Validate the page with this validation group
            Page.Validate("addLesson");

            // Was the page valid?
            if (Page.IsValid)
            {
                // Set up the error label
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                try
                {
                    // Add the lesson with the entered information
                    long courseID = methodsClass.AddCourse(studentList.SelectedItem.Text, Convert.ToInt32(artFirst.SelectedValue), Convert.ToInt32(artSecond.SelectedValue),
                        (recurringClass.SelectedValue == "0" ? Convert.ToDateTime(beginDate.Text).DayOfWeek.ToString() : recurringClass.SelectedValue),
                        startTime.SelectedValue, Convert.ToInt32(instructor.SelectedValue), (recurringClass.SelectedValue == "0" ? 0 : 1), "L");

                    // Did we get a valid course ID back?
                    if (courseID != 0)
                    {
                        // Add the selected student to the lesson
                        bool classResponse = methodsClass.AddStudentCourse(courseID, Convert.ToInt32(studentList.SelectedValue));

                        // Was adding the student successful?
                        if (classResponse)
                        {
                            // Is the lesson recurring?
                            if (recurringClass.SelectedValue == "0")
                            {
                                // One time lesson, so go ahead and add a new class
                                classResponse = methodsClass.AddClass(beginDate.Text, courseID);

                                // Was adding the class successful?
                                if (!classResponse)
                                    // Output the standard message
                                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_ADD_CLASS;
                            }
                        }
                        else
                            // Output the error message
                            errorLabel.Text = HOTBAL.SDAMessages.ERROR_ADD_STUDENT_CLASS;
                    }
                    else
                        // Output the error message
                        errorLabel.Text = HOTBAL.SDAMessages.ERROR_ADD_CLASS;

                    // Did we have any errors and did we get the course ID?
                    if (string.IsNullOrEmpty(errorLabel.Text) && (courseID != 0))
                        // Redirect back to the main page
                        Response.Redirect(HOTBAL.SDAConstants.MAIN_INTERNAL_URL);
                }
                catch (Exception ex)
                {
                    // Send the error and output the standard message
                    functionsClass.SendErrorMail("ClassAdd: addLesson", ex, "");
                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                }
            }
        }

        /// <summary>
        /// Adds a new class with the entered information. If this is a non-recurring class, it is
        ///     added to the class table.
        /// </summary>
        protected void addClass_Click(object sender, EventArgs e)
        {
            // Validate the page with this validation group
            Page.Validate("addClass");

            // Was the page valid?
            if (Page.IsValid)
            {
                // Set up the error label
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                try
                {
                    // Add the class with the entered information
                    long courseID = methodsClass.AddCourse(classTitle.Text, Convert.ToInt32(artFirst.SelectedValue), Convert.ToInt32(artSecond.SelectedValue),
                        (recurringClass.SelectedValue == "0" ? Convert.ToDateTime(beginDate.Text).DayOfWeek.ToString() : recurringClass.SelectedValue),
                        startTime.SelectedValue, Convert.ToInt32(instructor.SelectedValue), (recurringClass.SelectedValue == "0" ? 0 : 1), "C");

                    // Did we get a valid course ID back?
                    if (courseID != 0)
                    {
                        // Is the class recurring?
                        if (recurringClass.SelectedValue == "0")
                        {
                            // One time class, so go ahead and add a new class
                            bool classResponse = methodsClass.AddClass(beginDate.Text, courseID);

                            // Was adding the class successful?
                            if (!classResponse)
                                // Output the standard message
                                errorLabel.Text = HOTBAL.SDAMessages.ERROR_ADD_CLASS;
                        }
                    }
                    else
                        // Output the standard message
                        errorLabel.Text = HOTBAL.SDAMessages.ERROR_ADD_CLASS;

                    // Did we have any errors and did we get the course ID?
                    if (string.IsNullOrEmpty(errorLabel.Text) && (courseID != 0))
                        // Redirect back to the main page
                        Response.Redirect(HOTBAL.SDAConstants.MAIN_INTERNAL_URL);
                }
                catch (Exception ex)
                {
                    // Send the error and output the standard message
                    functionsClass.SendErrorMail("ClassAdd: addClass", ex, "");
                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                }
            }
        }
    }
}