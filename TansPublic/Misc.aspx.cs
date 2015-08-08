using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class Misc : System.Web.UI.Page
    {
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Other Accessories";
            try
            {
                List<HOTBAL.Product> productListing = sqlClass.GetAccessoriesByType("OT");

                if (productListing != null)
                {
                    if (productListing[0].ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE)
                    {
                        miscAccessories.Text = "<tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr>";
                    }
                    else
                    {
                        miscAccessories.Text = "<tr><td><br /></td><td><b>Item</b></td><td><b>Price</b></td><td><br /></td></tr>";
                        foreach (HOTBAL.Product p in productListing)
                        {
                            miscAccessories.Text += "<tr><td rowspan='2' align='center' align='top'><a href='" + 
                                HOTBAL.TansConstants.PRODUCT_PUBLIC_URL + "?ID=" + 
                                p.ProductId + "'><img src='products/";

                            if (String.IsNullOrEmpty(p.ProductFileName))
                            {
                                miscAccessories.Text += "nooth-sm.gif";
                            }
                            else
                            {
                                miscAccessories.Text += p.ProductFileName + "-sm.gif";
                            }

                            miscAccessories.Text += "' alt='" + p.ProductName + "' border=0></a></td><td valign='top'><b><a href='" + 
                                HOTBAL.TansConstants.PRODUCT_PUBLIC_URL + "?ID="
                                + p.ProductId + "'>" + 
                                p.ProductName + "</a></b></td><td valign='top'><center>";

                            if (p.IsOnSaleOnline)
                            {
                                miscAccessories.Text += "<span style='color:red;'>$" + 
                                    p.ProductSalePrice + "</span><br><span style='font-size: 9px'>Reg - $" + 
                                    p.ProductPrice + "</span>";
                            }
                            else
                            {
                                miscAccessories.Text += "$" + p.ProductPrice;
                            }

                            miscAccessories.Text += "</center></td><td valign='top'>";

                            if (p.ProductCount > 1)
                            {
                                miscAccessories.Text += "<a href='" + HOTBAL.TansConstants.SHOPPING_PUBLIC_URL + "?action=add&ItemID=" + 
                                    p.ProductId + "'>Add to Cart</a></td></tr>";
                            }
                            else
                            {
                                miscAccessories.Text += "<i>Out of Stock Online</i></td></tr>";
                            }

                            if (p.ProductDescription.Length > 150)
                            {
                                miscAccessories.Text += "<tr><td colspan='3' valign='top' height='100'><i>" +
                                    p.ProductDescription.Substring(0, 150) + "<a href='" + 
                                    HOTBAL.TansConstants.PRODUCT_PUBLIC_URL + "?ID=" + 
                                    p.ProductId + "'>...</a></i></td></tr>";
                            }
                            else
                            {
                                miscAccessories.Text += "<tr><td colspan='3' valign='top' height='100'><i>" + p.ProductDescription + "</i></td></tr>";
                            }
                        }

                        miscAccessories.Text += "<tr><td colspan='4'><br /><b>Can't find what you're looking for here? Visit us in store for more options!</b></td></tr>";
                    }
                }
                else
                {
                    miscAccessories.Text = "<tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr>";
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "Site: Misc Accessories");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }
    }
}