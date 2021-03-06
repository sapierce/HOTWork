﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class StudentPhone : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                if (Request.QueryString["NID"] != null)
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["NID"]))
                        {
                            int phoneId = Convert.ToInt32(Request.QueryString["NID"].ToString());
                            editPhonePanel.Visible = true;
                            addPhonePanel.Visible = false;
                            HOTBAL.StudentPhone studentPhone = sqlClass.GetStudentPhoneById(phoneId);

                            if (String.IsNullOrEmpty(studentPhone.ErrorMessage))
                            {
                                editRelationShip.Text = studentPhone.RelationshipToStudent;
                                editPhoneNumber.Text = studentPhone.PhoneNumber;
                            }
                            else
                                errorLabel.Text = studentPhone.ErrorMessage;
                        }
                    }
                else
                {
                    editPhonePanel.Visible = false;
                    addPhonePanel.Visible = true;
                }
            }
        }

        protected void editPhone_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            bool isSuccessful = sqlClass.UpdateStudentPhoneById(Convert.ToInt32(Request.QueryString["NID"].ToString()), editRelationShip.Text, editPhoneNumber.Text);

            if (isSuccessful)
                Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString());
            else
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        protected void deletePhone_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            bool isSuccessful = sqlClass.DeleteStudentPhoneById(Convert.ToInt32(Request.QueryString["NID"].ToString()));

            if (isSuccessful)
                Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString());
            else
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        protected void addPhone_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            bool isSuccessful = sqlClass.AddStudentPhone(Convert.ToInt32(Request.QueryString["ID"].ToString()), addRelationship.Text, addPhoneNumber.Text);

            if (isSuccessful)
                Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString());
            else
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }
    }
}