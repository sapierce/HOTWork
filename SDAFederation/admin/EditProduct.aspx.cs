using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation.admin
{
    public partial class EditProduct : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Product productInformation = methodsClass.GetItemByID(Convert.ToInt32(Request.QueryString["ID"]));

            if (productInformation.ProductId != 0)
            {
                productType.Items.FindByValue(productInformation.ProductType).Selected = true;
                productDescription.Text = productInformation.ProductDescription;
                productInventory.Text = productInformation.ProductCount.ToString();
                productName.Text = productInformation.ProductName;
                productPrice.Text = productInformation.ProductPrice.ToString();
                productSubType.Items.FindByValue(productInformation.ProductSubType).Selected = true;
                barCode.Text = productInformation.ProductCode;
                isTaxable.Checked = productInformation.IsTaxable;
                onSaleInStore.Checked = productInformation.IsOnSaleInStore;
                salePrice.Text = productInformation.ProductSalePrice.ToString();
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.ERROR_ADD_PRODUCT;
            }
        }

        protected void editProduct_Click(object sender, EventArgs e)
        {
            bool response = methodsClass.UpdateItem(Convert.ToInt32(Request.QueryString["ID"]), productName.Text, Convert.ToDouble(productPrice.Text), 
                productSubType.SelectedValue, isTaxable.Checked, productDescription.Text, false, onSaleInStore.Checked, Convert.ToDouble(salePrice.Text), 
                barCode.Text, "W", productType.SelectedValue, false, true, true, Convert.ToInt32(productInventory.Text));

            if (response)
                Response.Redirect(FederationConstants.ADMIN_DEFAULT_URL);
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + FederationMessages.ERROR_ADD_PRODUCT;
            }
        }
    }
}