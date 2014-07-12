using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation.admin
{
    public partial class AddProduct : System.Web.UI.Page
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addProduct_Click(object sender, EventArgs e)
        {
            bool response = methodsClass.AddItem(productName.Text, Convert.ToDouble(productPrice.Text), productSubType.SelectedValue, isTaxable.Checked, 
                productDescription.Text, false, onSaleInStore.Checked, Convert.ToDouble(salePrice.Text), barCode.Text, "W", productType.SelectedValue, 
                false, true, true, Convert.ToInt32(productInventory.Text));

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