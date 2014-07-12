using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense.Admin
{
    public partial class Items : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Administrate Items";
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Action"] == "edit")
                {
                    ItemEditSetup(Convert.ToInt32(Request.QueryString["ID"].ToString()));
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

        private void ItemEditSetup(int ID)
        {
            HOTBAL.Product itemResponse = sqlClass.GetItemByID(ID);

            txtProdName.Text = itemResponse.ProductName;
            sltProdType.Items.FindByValue(itemResponse.ProductType).Selected = true;
            sltProdSubType.Items.FindByValue(itemResponse.ProductSubType).Selected = true;
            txtProdPrice.Text = itemResponse.ProductPrice.ToString();
            chkProdTax.Checked = itemResponse.ProductTaxable;
            txtProdCode.Text = itemResponse.ProductCode;
            txtProdDesc.Text = itemResponse.ProductDescription;
            chkOnOn.Checked = itemResponse.ProductAvailableOnline;
            chkInOn.Checked = itemResponse.ProductAvailableInStore;
            chkNone.Checked = itemResponse.Active;
            txtWacoInv.Text = itemResponse.ProductCount.ToString();
            chkSlOn.Checked = itemResponse.ProductSaleOnline;
            chkSlIn.Checked = itemResponse.ProductSaleInStore;
            txtProdSale.Text = itemResponse.ProductSalePrice.ToString();
        }

        public void btnAdd_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.AddItem(txtProdName.Text, Convert.ToDouble(txtProdPrice.Text), sltProdSubType.SelectedValue, chkProdTax.Checked, txtProdDesc.Text,
                chkSlOn.Checked, chkSlIn.Checked, Convert.ToDouble(txtProdSale.Text), txtProdCode.Text, "W", sltProdType.SelectedValue, chkOnOn.Checked, true, 
                chkInOn.Checked, Convert.ToInt32(txtWacoInv.Text));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL);
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnEdit_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.UpdateItem(Convert.ToInt32(Request.QueryString["ID"].ToString()), txtProdName.Text, Convert.ToDouble(txtProdPrice.Text), 
                sltProdSubType.SelectedValue, chkProdTax.Checked, txtProdDesc.Text, chkSlOn.Checked, chkSlIn.Checked, Convert.ToDouble(txtProdSale.Text), 
                txtProdCode.Text, "W", sltProdType.SelectedValue, chkOnOn.Checked, true, chkInOn.Checked, Convert.ToInt32(txtWacoInv.Text));

            if (response)
                Response.Redirect(HOTBAL.SDAConstants.ADMIN_INTERNAL_URL);
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }

        public void btnDelete_onClick(Object sender, EventArgs e)
        {
            bool response = sqlClass.DeleteItem(Convert.ToInt32(Request.QueryString["ID"].ToString()));

            if (response)
                Response.Redirect("default.aspx");
            else
                lblError.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
        }
    }
}