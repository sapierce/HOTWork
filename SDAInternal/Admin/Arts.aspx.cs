using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class ArtsPage : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Administrate Arts";

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Action"].ToString() == "edit")
                {
                    ArtEditSetup(Convert.ToInt32(Request.QueryString["ID"].ToString()));
                    btnAdd.Visible = false;
                    btnEdit.Visible = true;
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

        private void ArtEditSetup(int ID)
        {
            HOTBAL.Art artResponse = new HOTBAL.Art();

            artResponse = sqlClass.GetArtByID(ID);

            txtArtName.Text = artResponse.Title;
        }

        public void btnAdd_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.AddArt(txtArtName.Text, 1);

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnEdit_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.UpdateArt(Convert.ToInt32(Request.QueryString["ID"].ToString()), txtArtName.Text);

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnDelete_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteArt(Convert.ToInt32(Request.QueryString["ID"].ToString()));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + Request.QueryString["Date"].ToString());
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }
    }
}