using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class ProductEdit : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Edit Product";
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            
            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);

            if (!Page.IsPostBack)
            {
                try
                {
                    HOTBAL.Product editProduct = sqlClass.GetProductByID(Convert.ToInt32(Request.QueryString["ID"]));

                    if (editProduct != null)
                    {
                        if (editProduct.ProductID != 0)
                        {
                            productType.Items.FindByValue(editProduct.ProductType).Selected = true;
                            productSubType.Items.FindByValue(editProduct.ProductSubType).Selected = true;

                            productName.Text = editProduct.ProductName;
                            productPrice.Text = String.Format("{0:F2}", editProduct.ProductPrice);
                            productBarCode.Text = editProduct.ProductCode;
                            productDescription.Text = editProduct.ProductDescription;
                            salePrice.Text = String.Format("{0:F2}", editProduct.ProductSalePrice);
                            wacoInventory.Text = editProduct.ProductCount.ToString();

                            productTaxed.Checked = editProduct.ProductTaxable;
                            availableInStore.Checked = editProduct.ProductAvailableInStore;
                            availableOnline.Checked = editProduct.ProductAvailableOnline;
                            saleOnline.Checked = editProduct.ProductSaleOnline;
                            saleInStore.Checked = editProduct.ProductSaleInStore;
                        }
                        else
                            errorLabel.Text += HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO;
                    }
                    else
                        errorLabel.Text += HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO;
                }
                catch (Exception ex)
                {
                    sqlClass.LogErrorMessage(ex, Request.QueryString["ID"].ToString(), "Admin: ProductEdit");
                    errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                }
            }
        }

        protected void editProduct_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            bool isSuccess = sqlClass.EditProduct(Convert.ToInt64(functionsClass.CleanUp(Request.QueryString["ID"])), productName.Text, productDescription.Text,
                productType.SelectedValue, productSubType.SelectedValue, productBarCode.Text, productPrice.Text, productTaxed.Checked, salePrice.Text, saleOnline.Checked, 
                saleInStore.Checked, (availableOnline.Checked ? true : false), (availableInStore.Checked ? false : true), Convert.ToInt32(wacoInventory.Text));

            if (isSuccess)
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL, false);
            else
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
        }

        protected void deleteProduct_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            bool isSuccess = sqlClass.UpdateProductStatus(Convert.ToInt64(functionsClass.CleanUp(Request.QueryString["ID"])), false);

            if (isSuccess)
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL, false);
            else
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
        }
    }
}