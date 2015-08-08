using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class LotionsList : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString != null)
                {
                    switch (functionsClass.CleanUp(Request.QueryString["Type"].ToString().ToUpper()))
                    {
                        case "LM":
                            litLotions.Text = "Mystic | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LN' class='center'>Non-Tingle</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LT' class='center'>Tingle</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LS' class='center'>Samples</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LO' class='center'>Moisturizers</a><br />";
                            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Mystic Lotions";
                            break;

                        case "LN":
                            litLotions.Text = "<a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LM' class='center'>Mystic</a> | Non-Tingle | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LT' class='center'>Tingle</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LS' class='center'>Samples</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LO' class='center'>Moisturizers</a><br />";
                            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Non-Tingle Lotions";
                            break;

                        case "LT":
                            litLotions.Text = "<a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LM' class='center'>Mystic</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LN' class='center'>Non-Tingle</a> | Tingle | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LS' class='center'>Samples</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LO' class='center'>Moisturizers</a><br />";
                            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Tingle Lotions";
                            break;

                        case "LS":
                            litLotions.Text = "<a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LM' class='center'>Mystic</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LN' class='center'>Non-Tingle</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LT' class='center'>Tingle</a> | Samples | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LO' class='center'>Moisturizers</a><br />";
                            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Lotion Samples";
                            break;

                        case "LO":
                            litLotions.Text = "<a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LM' class='center'>Mystic</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LN' class='center'>Non-Tingle</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LT' class='center'>Tingle</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LS' class='center'>Samples</a> | Moisturizers<br />";
                            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Moisturizers";
                            break;

                        default:
                            litLotions.Text = "<a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LM' class='center'>Mystic</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LN' class='center'>Non-Tingle</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LT' class='center'>Tingle</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LS' class='center'>Samples</a> | <a href='" +
                                HOTBAL.TansConstants.LOTIONS_PUBLIC_URL + "?Type=LO' class='center'>Moisturizers</a><br />";
                            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Lotions";
                            break;
                    }
                }

                List<HOTBAL.Product> productListing = sqlClass.GetLotionsByType(Request.QueryString["Type"].ToString().ToUpper());

                if (productListing != null)
                {
                    if (productListing[0].ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE)
                    {
                        lotionsList.Text = "<tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr>";
                    }
                    else
                    {
                        string fileName;
                        lotionsList.Text = "<tr><td><br /></td><td style='text-align: center;'><b>Item</b></td><td style='text-align: center;'><b>Price</b></td><td><br /></td></tr>";
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
                            lotionsList.Text += "<tr><td rowspan='2' style='text-align: center;'><a href='" +
                                HOTBAL.TansConstants.PRODUCT_PUBLIC_URL + "?ID=" +
                                p.ProductId + "'><img src='products/" +
                                fileName + "' alt='" +
                                p.ProductName + "' border=0></a></td><td valign='top' align='left'><b><a href='" +
                                HOTBAL.TansConstants.PRODUCT_PUBLIC_URL + "?ID=" +
                                p.ProductId + "'>" +
                                p.ProductName + "</a></b></td><td valign='top' align='center'>";
                            if (p.IsOnSaleOnline)
                            {
                                lotionsList.Text += "<span style='color:red;'>$" +
                                    p.ProductSalePrice + "</span><br /><span style='font-size: 9px'>Reg - $" +
                                    p.ProductPrice + "</span>";
                            }
                            else
                            {
                                lotionsList.Text += "$" + p.ProductPrice;
                            }
                            lotionsList.Text += "</td><td style='text-align: center;vertical-align:top;'>";

                            if (p.ProductCount > 1)
                            {
                                lotionsList.Text += "<a href='" + HOTBAL.TansConstants.SHOPPING_PUBLIC_URL + "?action=add&ItemID=" +
                                    p.ProductId + "'>Add to Cart</a></td></tr>";
                            }
                            else
                            {
                                lotionsList.Text += "<i>Out of Stock Online</i></td></tr>";
                            }
                            string prodDesc;

                            if (p.ProductDescription.Length > 150)
                            {
                                prodDesc = p.ProductDescription.Substring(0, 150) + "<a href='" +
                                    HOTBAL.TansConstants.PRODUCT_PUBLIC_URL + "?ID=" + p.ProductId + "'>...</a>";
                            }
                            else
                            {
                                prodDesc = p.ProductDescription;
                            }
                            lotionsList.Text += "<tr><td colspan='3' style='text-align:left;vertical-align:top;'><i>" + prodDesc + "</i></td></tr>";
                        }

                        lotionsList.Text += "<tr><td colspan='4'><br /><b>Can't find what you're looking for here? Visit us in store for more options!</b></td></tr>";
                    }
                }
                else
                {
                    lotionsList.Text = "<tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr>";
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, Request.QueryString["Type"].ToString().ToUpper(), "Site: LotionsList");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }
    }
}