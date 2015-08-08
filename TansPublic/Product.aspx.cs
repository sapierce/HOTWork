using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class Product : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString != null)
                {
                    HOTBAL.Product productListing = sqlClass.GetProductByID(Convert.ToInt32(Request.QueryString["ID"]));

                    if (productListing != null)
                    {
                        string fileName = "";

                        if (productListing.ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO)
                        {
                            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Product Details";

                            productInformation.Text = "<tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO + "</td></tr>";
                        }
                        else
                        {
                            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - " + productListing.ProductName;

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
                                        switch(productListing.ProductSubType)
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

                            if (productListing.ProductType == "LTN")
                            {
                                lotionsList.Text = "<a href='" +
                                            HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LM' class='center'>Mystic</a> | <a href='" +
                                            HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LN' class='center'>Non-Tingle</a> | <a href='" +
                                            HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LT' class='center'>Tingle</a> | <a href='" +
                                            HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LS' class='center'>Samples</a> | <a href='" +
                                            HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LO' class='center'>Moisturizers</a><br />";
                            }

                            productInformation.Text += "<tr><td rowspan=4 width=150><img src='products/" + fileName + "' alt='" +
                                productListing.ProductName + "' border=0></td><td valign='top' align='left' class='product'><b>" +
                                productListing.ProductName + "</b></td></tr><tr><td valign='top' align='left' height='5' class='product'><b>Price: </b> ";

                            if (productListing.IsOnSaleOnline)
                            {
                                productInformation.Text += "<font color='red'>$" + productListing.ProductSalePrice +
                                    "</font><br><font style='font-size: 9px'>Reg - $" + productListing.ProductPrice + "</font>";
                            }
                            else
                            {
                                productInformation.Text += "$" + productListing.ProductPrice;
                            }
                            productInformation.Text += "</td></tr><tr><td valign='top' align='left'><b>Description: </b>" +
                                productListing.ProductDescription + "</td></tr><tr><td height='5' align='right' class='product'>";

                            if (productListing.ProductCount > 1)
                            {
                                productInformation.Text += "<a href='" + HOTBAL.TansConstants.SHOPPING_PUBLIC_URL + "?action=add&ItemID=" +
                                    productListing.ProductId + "&count=1'>Add to Cart</a></td></tr>";
                            }
                            else
                            {
                                productInformation.Text += "<i>Out of Stock</i></td></tr>";
                            }
                        }
                    }
                    else
                    {
                        Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Product Details";

                        productInformation.Text = HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO;
                    }
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                sqlClass.LogErrorMessage(ex, "", "Site: ProductDetails: Catch");
            }
        }
    }
}