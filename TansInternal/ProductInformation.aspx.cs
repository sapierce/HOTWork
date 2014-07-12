using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class ProductInformation : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Product Information";

                if (Request.QueryString != null)
                {
                    HOTBAL.Product productListing = sqlClass.GetProductByID(Convert.ToInt32(Request.QueryString["ID"]));

                    if (productListing != null)
                    {
                        string fileName = "";

                        if (productListing.ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO)
                        {
                            noProduct.Text = HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO;
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(productListing.ProductFileName))
                            {
                                fileName = productListing.ProductFileName + "-lg.gif";
                            }
                            else
                            {
                                switch (productListing.ProductType)
                                {
                                    case "LTN":
                                        fileName = "noltn-lg.gif";
                                        break;
                                    case "ACC":
                                        switch (productListing.ProductSubType)
                                        {
                                            case "GB":
                                                fileName = "nogb-lg.gif";
                                                break;
                                            case "LB":
                                                fileName = "nolb-lg.gif";
                                                break;
                                            default:
                                                fileName = "nooth-lg.gif";
                                                break;
                                        }
                                        break;
                                    default:
                                        fileName = "nooth-lg.gif";
                                        break;
                                }
                            }

                            switch (productListing.ProductType)
                            {
                                case "LTN":
                                    productType.Text = "Lotion";

                                    switch (productListing.ProductSubType)
                                    {
                                        case "LT":
                                            productType.Text += "-Tingle";
                                            break;
                                        case "LN":
                                            productType.Text += "-NonTingle";
                                            break;
                                        case "LM":
                                            productType.Text += "-Mystic";
                                            break;
                                        case "LS":
                                            productType.Text += "-Sample";
                                            break;
                                        case "LO":
                                            productType.Text += "-Moisturizer";
                                            break;
                                        default:
                                            productType.Text += "-" + productListing.ProductSubType;
                                            break;
                                    }
                                    break;
                                case "ACC":
                                    switch (productListing.ProductSubType)
                                    {
                                        case "GB":
                                            productType.Text = "Gift Bag";
                                            break;
                                        case "LB":
                                            productType.Text = "Lip Balm";
                                            break;
                                        default:
                                            productType.Text = "Other Accessory";
                                            break;
                                    }
                                    break;
                                default:
                                    productType.Text = "Other Accessory";
                                    break;
                            }

                            productImage.Text = "<img src='/products/" + fileName + "' alt='" + productListing.ProductName + "' border=0 />";
                            productName.Text = productListing.ProductName;

                            if (!productListing.Active)
                                productName.Text += "-INACTIVE";

                            if ((productListing.ProductSaleInStore) || (productListing.ProductSaleOnline))
                            {
                                productPrice.Text = String.Format("{0:C}", productListing.ProductPrice);
                                productSalePrice.Text = String.Format("{0:C}", productListing.ProductSalePrice);
                                productTotalPrice.Text = String.Format("{0:C}", (productListing.ProductPrice + (productListing.ProductPrice * 0.0825)));
                                productTotalSalePrice.Text = String.Format("{0:C}", (productListing.ProductSalePrice + (productListing.ProductSalePrice * 0.0825)));
                            }
                            else
                            {
                                productPrice.Text = productSalePrice.Text = String.Format("{0:C}", productListing.ProductPrice);
                                productTotalPrice.Text = productTotalSalePrice.Text = String.Format("{0:C}", (productListing.ProductPrice + (productListing.ProductPrice * 0.0825)));
                            }
                            productDescription.Text = productListing.ProductDescription;
                            productCount.Text = productListing.ProductCount.ToString();
                        }
                    }
                    else
                    {
                        noProduct.Text = HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO;
                    }
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                sqlClass.LogErrorMessage(ex, "", "Internal: ProductInformation: Catch");
            }
        }
    }
}