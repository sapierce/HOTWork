using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class StudentAddLesson : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Add Lesson";
            long courseID = 0;
            
            if (Page.IsPostBack)
            {
                //This is a lesson
                //Add to the CRSE_DOMN
                courseID = sqlClass.AddCourse(txtTitle.Text, Convert.ToInt32(sltArtFirst.SelectedValue), Convert.ToInt32(sltArtSecond.SelectedValue),
                    (sltRecurringLesson.SelectedValue == "0" ? "" : sltRecurringLesson.SelectedValue), txtTime.Text + " " + sltAMPM.SelectedValue, Convert.ToInt32(sltInstructor.SelectedValue),
                    (sltRecurringLesson.SelectedValue == "0" ? 0 : 1), "L");

                if (courseID != 0)
                {
                    //Add student to STDT_CRSE_XREF
                    bool classResponse = sqlClass.AddStudentCourse(courseID, Convert.ToInt32(sltStudentList.SelectedValue));

                    if (classResponse)
                    {
                        if (sltRecurringLesson.SelectedValue == "0")
                        {
                            //One time class, so go ahead and add it to the CLASS_DOMN
                            classResponse = sqlClass.AddClass(txtDate.Text, courseID);
                            if (classResponse)
                            {
                                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                            }
                        }
                    }
                    else
                    {
                        lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                    }
                }
                else
                {
                    lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
                }

                if (string.IsNullOrEmpty(lblError.Text) && (courseID != 0))
                {
                    Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString() + "&Date=" + Request.QueryString["Date"]);
                }
            }
            else
            {
                PopulateInstructors();
                PopulateArts();
                PopulateStudents();

                //Set AM/PM values
                sltAMPM.Items.Add(new ListItem("AM", "AM"));
                sltAMPM.Items.Add(new ListItem("PM", "PM"));

                if (!String.IsNullOrEmpty(Request.QueryString["Time"]))
                {
                    string[] splitTime = new string[2];
                    splitTime = Request.QueryString["Time"].ToString().Split(Convert.ToChar(" "));

                    txtTime.Text = splitTime[0].ToString();
                    if (splitTime[1].ToString() == "PM")
                    {
                        sltAMPM.Items.FindByValue("PM").Selected = true;
                    }
                }

                if (!String.IsNullOrEmpty(Request.QueryString["Date"]))
                {
                    txtDate.Text = Request.QueryString["Date"].ToString();
                }

                sltRecurringLesson.Items.Add(new ListItem("Not Recurring", "0"));
                sltRecurringLesson.Items.Add(new ListItem("Sunday", "SUN"));
                sltRecurringLesson.Items.Add(new ListItem("Monday", "MON"));
                sltRecurringLesson.Items.Add(new ListItem("Tueday", "TUE"));
                sltRecurringLesson.Items.Add(new ListItem("Wednesday", "WED"));
                sltRecurringLesson.Items.Add(new ListItem("Thursday", "THU"));
                sltRecurringLesson.Items.Add(new ListItem("Friday", "FRI"));
                sltRecurringLesson.Items.Add(new ListItem("Saturday", "SAT"));
            }
        }

        private void PopulateArts()
        {
            sltArtFirst.Items.Clear();
            sltArtFirst.Items.Add(new ListItem("-Choose An Art-", "0"));
            sltArtSecond.Items.Clear();
            sltArtSecond.Items.Add(new ListItem("-Choose An Art-", "0"));

            List<HOTBAL.Art> artList = sqlClass.GetAllSDAArts();

            if (artList != null)
            {
                if (String.IsNullOrEmpty(artList[0].Error))
                {
                    foreach (HOTBAL.Art a in artList)
                    {
                        sltArtFirst.Items.Add(new ListItem(a.Title, a.ID.ToString()));
                        sltArtSecond.Items.Add(new ListItem(a.Title, a.ID.ToString()));
                    }
                }
            }
        }

        private void PopulateInstructors()
        {
            sltInstructor.Items.Clear();
            sltInstructor.Items.Add(new ListItem("-Choose An Instructor-", "0"));

            List<HOTBAL.Instructor> instList = sqlClass.GetAllInstructors();

            if (instList != null)
            {
                if (String.IsNullOrEmpty(instList[0].Error))
                {
                    foreach (HOTBAL.Instructor i in instList)
                    {
                        sltInstructor.Items.Add(new ListItem(i.FirstName + " " + i.LastName, i.ID.ToString()));
                    }
                }
            }
        }

        private void PopulateStudents()
        {
            sltStudentList.Items.Clear();
            sltStudentList.Items.Add(new ListItem("-Choose A Student-", "0"));

            List<HOTBAL.Student> stdtList = sqlClass.GetAllStudents();

            if (stdtList != null)
            {
                if (String.IsNullOrEmpty(stdtList[0].Error))
                {
                    foreach (HOTBAL.Student s in stdtList)
                    {
                        sltStudentList.Items.Add(new ListItem(s.LastName + ", " + s.FirstName, s.ID.ToString()));
                    }
                    sltStudentList.Items.FindByValue(Request.QueryString["ID"].ToString()).Selected = true;
                }
            }
        }
    }
}