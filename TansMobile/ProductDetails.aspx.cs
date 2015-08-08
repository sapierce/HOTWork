using System;
using System.Web.UI;

namespace MobileSite
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = Page.Header.Title + " - Product Details";

            if (Request.QueryString != null)
            {
                HOTBAL.Product productListing = sqlClass.GetProductByID(Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ID"])));

                if (productListing != null)
                {
                    string fileName = "";

                    if (productListing.ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO)
                    {
                        productInformation.Text = "<tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO + "</td></tr>";
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(productListing.ProductFileName))
                        {
                            fileName = productListing.ProductFileName + "-lg.gif";
                        }
                        else
                        {
                            switch(functionsClass.CleanUp(Request.QueryString["Type"].ToString()))
                            {
                                case "lotion":
                                    fileName = "noltn-lg.gif";
                                    break;
                                case "gift":
                                    fileName = "nogb-lg.gif";
                                    break;
                                case "lip":
                                    fileName = "nolb-lg.gif";
                                    break;
                                default:
                                    fileName = "nooth-lg.gif";
                                    break;
                            }
                        }

                        productInformation.Text += "<tr><td><img src='/products/" + fileName + "' alt='" + productListing.ProductName + "' border=0></td></tr>";
                        productInformation.Text += "<tr><td valign='top' align='left' class='product'><b>" + productListing.ProductName + "</b></td></tr>";
                        productInformation.Text += "<tr><td valign='top' align='left' height='5' class='product'><b>Price: </b> ";

                        if (productListing.IsOnSaleOnline)
                        {
                            productInformation.Text += "<font color='red'>$" + productListing.ProductSalePrice + "</font><br><font style='font-size: 9px'>Reg - $" + productListing.ProductPrice + "</font>";
                        }
                        else
                        {
                            productInformation.Text += "$" + productListing.ProductPrice;
                        }
                        productInformation.Text += "</td></tr>";
                        productInformation.Text += "<tr><td valign='top' align='left'><b>Description: </b>" + productListing.ProductDescription + "</td></tr>";
                        productInformation.Text += "<tr><td height='5' align='right' class='product'>";

                        //if (productListing.ProductCount > 1)
                        //{
                        //    productInformation.Text += "<a href='shoppingcart.aspx?action=add&ItemID=" + productListing.ProductID + "&count=1'>Add to Cart</a></td></tr>";
                        //}
                        //else
                        //{
                        //    productInformation.Text += "<i>Out of Stock</i></td></tr>";
                        //}
                        productInformation.Text += "</td></tr>";
                    }
                }
                else
                {
                    productInformation.Text = HOTBAL.TansMessages.ERROR_NO_PRODUCT_INFO;
                }
            }
        }
    }
}