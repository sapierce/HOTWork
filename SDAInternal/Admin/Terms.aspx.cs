using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class TermsPage : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Administrate Terms";

            if (!Page.IsPostBack)
            {
                PopulateBelt();

                if (Request.QueryString["Action"] == "edit")
                {
                    TermEditSetup(Convert.ToInt32(Request.QueryString["ID"].ToString()));
                    btnEdit.Visible = true;
                    btnAdd.Visible = false;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnAdd.Visible = true;
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        private void PopulateBelt()
        {
            ddlBeltName.Items.Clear();
            ddlBeltName.Items.Add(new ListItem("-Choose A Belt-", "0"));

            List<HOTBAL.Belt> beltsResponse = new List<HOTBAL.Belt>();

            beltsResponse = sqlClass.GetAllSDABelts();

            if (beltsResponse != null)
            {
                if (String.IsNullOrEmpty(beltsResponse[0].Error))
                {
                    foreach (HOTBAL.Belt b in beltsResponse)
                    {
                        ddlBeltName.Items.Add(new ListItem(b.Title, b.ID.ToString()));
                    }
                }
                else
                {
                    lblError.Text = beltsResponse[0].Error;
                }
            }
        }

        private void TermEditSetup(int ID)
        {
            HOTBAL.Term termResponse = new HOTBAL.Term();

            termResponse = sqlClass.GetTermByID(ID);

            if (termResponse != null)
            {
                if (String.IsNullOrEmpty(termResponse.Error))
                {
                    txtEnglish.Text = termResponse.English;
                    txtChinese.Text = termResponse.Chinese;
                    ddlBeltName.Items.FindByValue(termResponse.BeltID.ToString()).Selected = true;
                }
                else
                {
                    lblError.Text = termResponse.Error;
                }
            }
        }

        public void btnAdd_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.AddTerm(txtEnglish.Text, txtChinese.Text, Convert.ToInt32(ddlBeltName.SelectedValue));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnEdit_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.UpdateTerm(Convert.ToInt32(Request.QueryString["ID"].ToString()), txtEnglish.Text, txtChinese.Text, Convert.ToInt32(ddlBeltName.SelectedValue));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnDelete_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteTerm(Convert.ToInt32(Request.QueryString["ID"].ToString()));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }
    }
}