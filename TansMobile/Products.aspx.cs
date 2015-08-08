using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileSite
{
    public partial class Products : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<HOTBAL.Product> productListing = sqlClass.GetAccessoriesByType("LB");

                if (productListing != null)
                {
                    if (productListing[0].ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE)
                    {
                        lipBalmList.Text = "<table style='width: 300px;'><tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr></table>";
                    }
                    else
                    {
                        lipBalmList.Text = "<table style='width: 300px;'><tr><td><br /></td><td><b>Item</b></td><td><b>Price</b></td><td><br /></td></tr>";
                        foreach (HOTBAL.Product p in productListing)
                        {
                            lipBalmList.Text += "<tr><td rowspan='2' style='vertical-align:top;'><a href='" + HOTBAL.TansConstants.PRODUCTS_DETAILS_MOBILE_URL + "?ID=" + p.ProductId + "'><img src='/products/";

                            if (String.IsNullOrEmpty(p.ProductFileName))
                            {
                                lipBalmList.Text += "nolb-sm.gif";
                            }
                            else
                            {
                                lipBalmList.Text += p.ProductFileName + "-sm.gif";
                            }

                            lipBalmList.Text += "' alt='" + p.ProductName + "' border=0 style='vertical-align:top;'></a></td><td style='vertical-align:top;'><b><a href='" + HOTBAL.TansConstants.PRODUCTS_DETAILS_MOBILE_URL + "?ID="
                                + p.ProductId + "'>" + p.ProductName + "</a></b></td><td style='vertical-align:top;'>";

                            if (p.IsOnSaleOnline)
                            {
                                lipBalmList.Text += "<span style='color:red;'>$" + p.ProductSalePrice + "</span><br><span style='font-size: 9px'>Reg - $" + p.ProductPrice + "</span>";
                            }
                            else
                            {
                                lipBalmList.Text += "$" + p.ProductPrice;
                            }

                            lipBalmList.Text += "</td><td style='vertical-align:top;'>";

                            //if (p.ProductCount > 1)
                            //{
                            //    lipBalmList.Text += "<a href='ShoppingCart.aspx?action=add&ItemID=" + p.ProductID + "'>Add to Cart</a>";
                            //}
                            //else
                            //{
                            //    lipBalmList.Text += "<em>Out of Stock Online</em>";
                            //}
                            lipBalmList.Text += "</td></tr>";

                            if (p.ProductDescription.Length > 150)
                            {
                                lipBalmList.Text += "<tr><td colspan='3' style='vertical-align:top;'><em>" + p.ProductDescription.Substring(0, 150) + "<a href='" + HOTBAL.TansConstants.PRODUCTS_DETAILS_MOBILE_URL + "?ID=" + p.ProductId + "&type=lip'>...</a></em></td></tr>";
                            }
                            else
                            {
                                lipBalmList.Text += "<tr><td colspan='3' style='vertical-align:top;'><em>" + p.ProductDescription + "</em></td></tr>";
                            }
                        }

                        lipBalmList.Text += "<tr><td colspan='4'><br /><b>Can't find what you're looking for here? Visit us in store for more options!</b></td></tr></table>";
                    }
                }
                else
                {
                    lipBalmList.Text = "<table style='width: 300px;'><tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr></table>";
                }

                productListing = sqlClass.GetAccessoriesByType("GB");

                if (productListing != null)
                {
                    if (productListing[0].ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE)
                    {
                        giftBagList.Text = "<table style='width: 300px;'><tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr></table>";
                    }
                    else
                    {
                        giftBagList.Text = "<table style='width: 300px;'><tr><td><br /></td><td><b>Item</b></td><td><b>Price</b></td><td><br /></td></tr>";
                        foreach (HOTBAL.Product p in productListing)
                        {
                            giftBagList.Text += "<tr><td rowspan='2' style='vertical-align:top;'><a href='" + HOTBAL.TansConstants.PRODUCTS_DETAILS_MOBILE_URL + "?ID=" + p.ProductId + "'><img src='/products/";

                            if (String.IsNullOrEmpty(p.ProductFileName))
                            {
                                giftBagList.Text += "nogb-sm.gif";
                            }
                            else
                            {
                                giftBagList.Text += p.ProductFileName + "-sm.gif";
                            }

                            giftBagList.Text += "' alt='" + p.ProductName + "' border=0 style='height: 100px;'></a></td><td style='vertical-align:top;'><b><a href='" + HOTBAL.TansConstants.PRODUCTS_DETAILS_MOBILE_URL + "?ID="
                                + p.ProductId + "'>" + p.ProductName + "</a></b></td><td style='vertical-align:top;'>";

                            if (p.IsOnSaleOnline)
                            {
                                giftBagList.Text += "<span style='color: red;'>$" + p.ProductSalePrice + "</span><br><span style='font-size: 9px'>Reg - $" + p.ProductPrice + "</span>";
                            }
                            else
                            {
                                giftBagList.Text += "$" + p.ProductPrice;
                            }

                            giftBagList.Text += "</td><td style='vertical-align:top;'>";

                            //if (p.ProductCount > 1)
                            //{
                            //    giftBagList.Text += "<a href='ShoppingCart.aspx?action=add&ItemID=" + p.ProductID + "'>Add to Cart</a></td></tr>";
                            //}
                            //else
                            //{
                            //    giftBagList.Text += "<em>Out of Stock Online</em></td></tr>";
                            //}
                            giftBagList.Text += "</td></tr>";

                            if (p.ProductDescription.Length > 150)
                            {
                                giftBagList.Text += "<tr><td colspan='3' style='vertical-align:top;'><em>" + p.ProductDescription.Substring(0, 150) + "<a href='" + HOTBAL.TansConstants.PRODUCTS_DETAILS_MOBILE_URL + "?ID=" + p.ProductId + "'>...</a></em></td></tr>";
                            }
                            else
                            {
                                giftBagList.Text += "<tr><td colspan='3' style='vertical-align:top;'><em>" + p.ProductDescription + "</em></td></tr>";
                            }
                        }

                        giftBagList.Text += "<tr><td colspan='4'><br /><b>Can't find what you're looking for here? Visit us in store for more options!</b></td></tr></table>";
                    }
                }
                else
                {
                    giftBagList.Text = "<table style='width: 300px;'><tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr></table>";
                }

                productListing = sqlClass.GetAccessoriesByType("OT");

                if (productListing != null)
                {
                    if (productListing[0].ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE)
                    {
                        otherAccessories.Text = "<table style='width: 300px;'><tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr></table>";
                    }
                    else
                    {
                        otherAccessories.Text = "<table style='width: 300px;'><tr><td><br /></td><td><b>Item</b></td><td><b>Price</b></td><td><br /></td></tr>";
                        foreach (HOTBAL.Product p in productListing)
                        {
                            otherAccessories.Text += "<tr><td rowspan='2' align='center' style='vertical-align:top;'><a href='" + HOTBAL.TansConstants.PRODUCTS_DETAILS_MOBILE_URL + "?ID=" + p.ProductId + "&Type=oth'><img src='/products/";

                            if (String.IsNullOrEmpty(p.ProductFileName))
                            {
                                otherAccessories.Text += "nooth-sm.gif";
                            }
                            else
                            {
                                otherAccessories.Text += p.ProductFileName + "-sm.gif";
                            }

                            otherAccessories.Text += "' alt='" + p.ProductName + "' border=0 style='vertical-align:top;'></a></td><td valign='top'><b><a href='" + HOTBAL.TansConstants.PRODUCTS_DETAILS_MOBILE_URL + "?ID="
                                + p.ProductId + "'>" + p.ProductName + "</a></b></td><td valign='top'><center>";

                            if (p.IsOnSaleOnline)
                            {
                                otherAccessories.Text += "<span style='color:red;'>$" + p.ProductSalePrice + "</span><br><span style='font-size: 9px'>Reg - $" + p.ProductPrice + "</span>";
                            }
                            else
                            {
                                otherAccessories.Text += "$" + p.ProductPrice;
                            }

                            otherAccessories.Text += "</center></td><td style='vertical-align:top;'>";

                            //if (p.ProductCount > 1)
                            //{
                            //    otherAccessories.Text += "<a href='ShoppingCart.aspx?action=add&ItemID=" + p.ProductID + "'>Add to Cart</a></td></tr>";
                            //}
                            //else
                            //{
                            //    otherAccessories.Text += "<em>Out of Stock Online</em></td></tr>";
                            //}
                            otherAccessories.Text += "</td></tr>";

                            if (p.ProductDescription.Length > 150)
                            {
                                otherAccessories.Text += "<tr><td colspan='3' style='vertical-align:top;'><em>" + p.ProductDescription.Substring(0, 150) + "<a href='" + HOTBAL.TansConstants.PRODUCTS_DETAILS_MOBILE_URL + "?ID=" + p.ProductId + "'>...</a></em></td></tr>";
                            }
                            else
                            {
                                otherAccessories.Text += "<tr><td colspan='3' style='vertical-align:top;'><em>" + p.ProductDescription + "</em></td></tr>";
                            }
                        }

                        otherAccessories.Text += "<tr><td colspan='4'><br /><b>Can't find what you're looking for here? Visit us in store for more options!</b></td></tr></table>";
                    }
                }
                else
                {
                    otherAccessories.Text = "<table style='width: 300px;'><tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr></table>";
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "Mobile: Products");
                //errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }
    }
}