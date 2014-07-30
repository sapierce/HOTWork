﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class ClassesPage : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Administrate Classes";

            if (!Page.IsPostBack)
            {
                PopulateArt();
                PopulateInstructors();
                PopulateStatic();
                PopulateClass(Convert.ToInt32(Request.QueryString["ID"].ToString()));
            }
        }

        private void PopulateArt()
        {
            sltArtFirst.Items.Clear();
            sltArtFirst.Items.Add(new ListItem("-Choose An Art-", "0"));
            sltArtSecond.Items.Clear();
            sltArtSecond.Items.Add(new ListItem("-Choose An Art-", "0"));
            sltArtSecond.Items.Add(new ListItem("None", "0"));

            List<HOTBAL.Art> artsResponse = sqlClass.GetAllSDAArts();

            if (artsResponse != null)
            {
                if (String.IsNullOrEmpty(artsResponse[0].Error))
                {
                    foreach (HOTBAL.Art a in artsResponse)
                    {
                        sltArtFirst.Items.Add(new ListItem(a.Title, a.ID.ToString()));
                        sltArtSecond.Items.Add(new ListItem(a.Title, a.ID.ToString()));
                    }
                }
                else
                {
                    lblError.Text = artsResponse[0].Error;
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

        private void PopulateStatic()
        {
            sltRecurringClass.Items.Add(new ListItem("Not Recurring", ""));
            sltRecurringClass.Items.Add(new ListItem("Monday", "MON"));
            sltRecurringClass.Items.Add(new ListItem("Tuesday", "TUE"));
            sltRecurringClass.Items.Add(new ListItem("Wednesday", "WED"));
            sltRecurringClass.Items.Add(new ListItem("Thursday", "THU"));
            sltRecurringClass.Items.Add(new ListItem("Friday", "FRI"));
            sltRecurringClass.Items.Add(new ListItem("Saturday", "SAT"));
            sltRecurringClass.Items.Add(new ListItem("Sunday", "SUN"));
        }

        private void PopulateClass(int ID)
        {
            HOTBAL.Course courseResponse = new HOTBAL.Course();
            courseResponse = sqlClass.GetCourseInformation(ID);

            if (courseResponse != null)
            {
                if (String.IsNullOrEmpty(courseResponse.Error))
                {
                    txtTime.Text = courseResponse.Time;
                    txtTitle.Text = courseResponse.Title;
                    sltArtFirst.Items.FindByValue(courseResponse.FirstArtID.ToString()).Selected = true;
                    sltArtSecond.Items.FindByValue(courseResponse.SecondArtID.ToString()).Selected = true;
                    sltInstructor.Items.FindByValue(courseResponse.InstructorID.ToString()).Selected = true;
                    sltRecurringClass.Items.FindByValue(courseResponse.Day.ToString()).Selected = true;
                }
                else
                {
                    lblError.Text = courseResponse.Error;
                }
            }
        }

        public void btnEdit_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.UpdateCourse(Convert.ToInt32(Request.QueryString["ID"].ToString()), 
                txtTitle.Text, Convert.ToInt32(sltArtFirst.SelectedValue), 
                Convert.ToInt32(sltArtSecond.SelectedValue), sltRecurringClass.SelectedValue, 
                txtTime.Text, Convert.ToInt32(sltInstructor.SelectedValue), 
                (sltRecurringClass.SelectedValue == "0" ? "0" : "1"));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnDelete_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteCourse(Convert.ToInt32(Request.QueryString["ID"].ToString()));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }
    }
}