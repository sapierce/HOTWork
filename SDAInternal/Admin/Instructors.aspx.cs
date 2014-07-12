using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class InstructorsPage : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Administrate Instructors";

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Action"] == "edit")
                {
                    InstructorEditSetup(Convert.ToInt32(Request.QueryString["ID"].ToString()));
                    btnEdit.Visible = true;
                    btnAdd.Visible = false;
                    btnDelete.Visible = true;
                }
                else
                    {
                        btnEdit.Visible = false;
                        btnAdd.Visible = true;
                    btnDelete.Visible = false;
                }
            }
        }

        private void InstructorEditSetup(int ID)
        {
            HOTBAL.Instructor instructorResponse = new HOTBAL.Instructor();

            instructorResponse = sqlClass.GetInstructorByID(ID);

            txtBio.Text = instructorResponse.Bio;
            txtFName.Text = instructorResponse.FirstName;
            txtLName.Text = instructorResponse.LastName;
            ddlType.Items.FindByValue(instructorResponse.Type).Selected = true;
        }

        public void btnAdd_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.AddInstructor(txtFName.Text, txtLName.Text, ddlType.SelectedValue, txtBio.Text);

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnEdit_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.UpdateInstructor(Convert.ToInt32(Request.QueryString["ID"].ToString()), txtFName.Text, txtLName.Text, ddlType.SelectedValue, txtBio.Text);

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnDelete_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteInstructor(Convert.ToInt32(Request.QueryString["ID"].ToString()));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }
    }
}