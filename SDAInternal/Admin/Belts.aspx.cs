using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class BeltsPage : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Administrate Belts";

            if (!Page.IsPostBack)
            {
                PopulateArt();

                if (Request.QueryString["Action"] == "edit")
                {
                    BeltEditSetup(Convert.ToInt32(Request.QueryString["ID"].ToString()));
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

        private void PopulateArt()
        {
            sltArt.Items.Clear();
            sltArt.Items.Add(new ListItem("-Choose An Art-", "0"));

            List<HOTBAL.Art> artsResponse = sqlClass.GetAllSDAArts();

            if (artsResponse != null)
            {
                if (String.IsNullOrEmpty(artsResponse[0].ErrorMessage))
                {
                    foreach (HOTBAL.Art a in artsResponse)
                    {
                        sltArt.Items.Add(new ListItem(a.ArtTitle, a.ArtId.ToString()));
                    }
                }
                else
                {
                    lblError.Text = artsResponse[0].ErrorMessage;
                }
            }
        }

        private void BeltEditSetup(int ID)
        {
            HOTBAL.Belt beltResponse = new HOTBAL.Belt();

            beltResponse = sqlClass.GetBeltByID(ID);

            txtBelt.Text = beltResponse.BeltTitle;
            txtBeltLevel.Text = beltResponse.BeltLevel;
            txtClass.Text = beltResponse.ClassCount.ToString();
            sltArt.Items.FindByValue(beltResponse.ArtId.ToString()).Selected = true;
            sltClassOrTip.Items.FindByValue(beltResponse.ClassOrTip).Selected = true;
        }

        public void btnAdd_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.AddBelt(txtBelt.Text, Convert.ToInt32(txtBeltLevel.Text), Convert.ToInt32(sltArt.SelectedValue), sltClassOrTip.SelectedValue, Convert.ToInt32(txtClass.Text));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnEdit_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.UpdateBelt(Convert.ToInt32(Request.QueryString["ID"].ToString()), txtBelt.Text, Convert.ToInt32(txtBeltLevel.Text), Convert.ToInt32(sltArt.SelectedValue), sltClassOrTip.SelectedValue, Convert.ToInt32(txtClass.Text));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnDelete_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteBelt(Convert.ToInt32(Request.QueryString["ID"].ToString()));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }
    }
}