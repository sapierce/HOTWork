using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class ProductAdd : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Add Product";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);

            if (!Page.IsPostBack)
            {
                wacoInventory.Text = "0";
                productPrice.Text = "0.00";
                salePrice.Text = "0.00";
            }
        }

        protected void addProduct_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            Int64 productID = sqlClass.AddProduct(productName.Text, productDescription.Text, productCategory.SelectedValue, productSubCategory.SelectedValue,
                productBarCode.Text, productPrice.Text, productTaxed.Checked, salePrice.Text, saleOnline.Checked, saleInStore.Checked,
                (availableOnline.Checked ? true : false), (availableInStore.Checked ? true : false));

            if (productID > 0)
            {
                bool success = sqlClass.InsertProductInventory(productID, Convert.ToInt32(wacoInventory.Text), "W");

                if (success)
                {
                    sqlClass.LogErrorMessage(new Exception("NewProductAdded"), productName.Text, productDescription.Text);
                    Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL, false);
                }
                else
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
            else
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
        }
    }
}