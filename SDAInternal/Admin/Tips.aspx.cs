using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class TipsPage : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Administrate Tips";

            if (!Page.IsPostBack)
            {
                //PopulateArt();
                PopulateBelt();

                if (Request.QueryString["Action"] == "edit")
                {
                    TipEditSetup(Convert.ToInt32(Request.QueryString["ID"].ToString()));
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

        //private void PopulateArt()
        //{
        //    ddlArtName.Items.Clear();
        //    ddlArtName.Items.Add(new ListItem("-Choose An Art-", "0"));

        //    HOTBAL.Arts artsResponse = new HOTBAL.Arts();

        //    artsResponse = sqlClass.GetAllArts();

        //    if (artsResponse != null)
        //    {
        //        if (String.IsNullOrEmpty(artsResponse.Art[0].Error))
        //        {
        //            foreach (HOTBAL.Art a in artsResponse)
        //            {
        //                ddlArtName.Items.Add(new ListItem(a.Title, a.ID.ToString()));
        //            }
        //        }
        //        else
        //        {
        //            lblError.Text = artsResponse.Art[0].Error;
        //        }
        //    }
        //}

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

        private void TipEditSetup(int ID)
        {
            HOTBAL.Tip tipResponse = new HOTBAL.Tip();

            tipResponse = sqlClass.GetTipByID(ID);

            if (tipResponse != null)
            {
                if (String.IsNullOrEmpty(tipResponse.Error))
                {
                    txtTipName.Text = tipResponse.Title;
                    txtTipLevel.Text = tipResponse.Level;
                    //ddlArtName.Items.FindByValue(tipResponse.BeltID.ToString()).Selected = true;
                    ddlBeltName.Items.FindByValue(tipResponse.BeltID.ToString()).Selected = true;
                    if (tipResponse.LastTipIndicator)
                    {
                        chkLastTip.Checked = true;
                    }
                }
                else
                    lblError.Text = tipResponse.Error;
            }
        }

        public void btnAdd_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.AddTip(txtTipName.Text, Convert.ToInt32(txtTipLevel.Text), Convert.ToInt32(ddlBeltName.SelectedValue), (chkLastTip.Checked == true ? "1" : "0"));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnEdit_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.UpdateTip(Convert.ToInt32(Request.QueryString["ID"].ToString()), txtTipName.Text, Convert.ToInt32(txtTipLevel.Text), Convert.ToInt32(ddlBeltName.SelectedValue), (chkLastTip.Checked == true ? "1" : "0"));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnDelete_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteTip(Convert.ToInt32(Request.QueryString["ID"].ToString()));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }
    }
}