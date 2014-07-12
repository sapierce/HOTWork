using System;
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
                if (Request.QueryString["NID"] != null)
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["NID"]))
                        {
                            int phoneId = Convert.ToInt32(Request.QueryString["NID"].ToString());
                            editPhonePanel.Visible = true;
                            addPhonePanel.Visible = false;
                            HOTBAL.StudentPhone studentPhone = sqlClass.GetStudentPhoneById(phoneId);

                            if (String.IsNullOrEmpty(studentPhone.Error))
                            {
                                editRelationShip.Text = studentPhone.Relationship;
                                editPhoneNumber.Text = studentPhone.PhoneNumber;
                            }
                            else
                                lblError.Text = studentPhone.Error;
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
            bool isSuccessful = sqlClass.UpdateStudentPhoneById(Convert.ToInt32(Request.QueryString["NID"].ToString()), editRelationShip.Text, editPhoneNumber.Text);

            if (isSuccessful)
                Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString() + "&ID=" + Request.QueryString["ID"].ToString());
        }

        protected void deletePhone_Click(object sender, EventArgs e)
        {
            bool isSuccessful = sqlClass.DeleteStudentPhoneById(Convert.ToInt32(Request.QueryString["NID"].ToString()));

            if (isSuccessful)
                Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString() + "&ID=" + Request.QueryString["ID"].ToString());
        }

        protected void addPhone_Click(object sender, EventArgs e)
        {
            bool isSuccessful = sqlClass.AddStudentPhone(Convert.ToInt32(Request.QueryString["ID"].ToString()), addRelationship.Text, addPhoneNumber.Text);

            if (isSuccessful)
                Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString() + "&ID=" + Request.QueryString["ID"].ToString());
        }
    }
}