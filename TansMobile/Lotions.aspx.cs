using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileSite
{
    public partial class Lotions : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<HOTBAL.Product> productListing = sqlClass.GetLotionsByType(Request.QueryString["Type"].ToString().ToUpper());

            if (productListing != null)
            {
                if (productListing[0].ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE)
                {
                    lotionsList.Text = "<table><tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr></table>";
                }
                else
                {
                    string fileName;
                    lotionsList.Text = "<table><tr><td><br /></td><td style='text-align: center;'><b>Item</b></td><td style='text-align: center;'><b>Price</b></td><td><br /></td></tr>";
                    foreach (HOTBAL.Product p in productListing)
                    {
                        if (!String.IsNullOrEmpty(p.ProductFileName))
                        {
                            fileName = p.ProductFileName + "-sm.gif";
                        }
                        else
                        {
                            fileName = "noltn-sm.gif";
                        }
                        lotionsList.Text += "<tr><td rowspan='2' style='text-align: center;'><a href='ProductDetails.aspx?ID=" + p.ProductID + "&type=lotion'><img src='/products/" + fileName + "' alt='" + p.ProductName + "' border=0></a></td><td valign='top' align='left'><b><a href='productdetails.aspx?ID=" + p.ProductID + "&type=lotion'>" + p.ProductName + "</a></b></td><td valign='top' align='center'>";
                        if (p.ProductSaleOnline)
                        {
                            lotionsList.Text += "<span style='color:red;'>$" + p.ProductSalePrice + "</span><br /><span style='font-size: 9px'>Reg - $" + p.ProductPrice + "</span>";
                        }
                        else
                        {
                            lotionsList.Text += "$" + p.ProductPrice;
                        }
                        lotionsList.Text += "</td><td style='text-align: center;vertical-align:top;'>";

                        //if (p.ProductCount > 1)
                        //{
                        //    lotionsList.Text += "<a href='ShoppingCart.aspx?action=add&ItemID=" + p.ProductID + "'>Add to Cart</a>";
                        //}
                        //else
                        //{
                        //    lotionsList.Text += "<i>Out of Stock Online</i>";
                        //}
                        lotionsList.Text += "</td></tr>";
                        string prodDesc;

                        if (p.ProductDescription.Length > 150)
                        {
                            prodDesc = p.ProductDescription.Substring(0, 150) + "<a href='ProductDetails.aspx?ID=" + p.ProductID + "&type=lotion'>...</a>";
                        }
                        else
                        {
                            prodDesc = p.ProductDescription;
                        }
                        lotionsList.Text += "<tr><td colspan='3' style='text-align:left;vertical-align:top;'><i>" + prodDesc + "</i></td></tr>";
                    }

                    lotionsList.Text += "<tr><td colspan='4'><br /><b>Can't find what you're looking for here? Visit us in store for more options!</b></td></tr></table>";
                }
            }
            else
            {
                lotionsList.Text = "<table><tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr></table>";
            }
        }
    }
}