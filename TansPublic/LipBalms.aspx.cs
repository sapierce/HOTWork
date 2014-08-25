using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class LipBalms : System.Web.UI.Page
    {
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the page title
            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Lip Balms";

            try
            {
                // Get the available lip balms
                List<HOTBAL.Product> productListing = sqlClass.GetAccessoriesByType("LB");

                // Was a product object returned?
                if (productListing != null)
                {
                    // Does the name of the first object contain an error?
                    if (productListing[0].ProductName == HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE)
                    {
                        // Output the none available message
                        lipBalms.Text = "<tr><td colspan='4'>" + HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr>";
                    }
                    else
                    {
                        // Output the header
                        lipBalms.Text = "<tr><td class='centerAlignHeaderNoBorder'><br /></td>" +
                            "<td class='centerAlignHeaderNoBorder'>Item</td>" +
                            "<td class='centerAlignHeaderNoBorder'>Price</td>" +
                            "<td class='centerAlignHeaderNoBorder'><br /></td></tr>";

                        // Loop through the returned products
                        foreach (HOTBAL.Product p in productListing)
                        {
                            // Output the returned product information
                            lipBalms.Text += "<tr><td rowspan='2' class='productImage'><a href='" +
                                HOTBAL.TansConstants.PRODUCT_PUBLIC_URL + "?ID=" +
                                p.ProductID + "'><img src='products/";

                            // Do we have an image?
                            if (String.IsNullOrEmpty(p.ProductFileName))
                            {
                                // Use the default
                                lipBalms.Text += "nolb-sm.gif";
                            }
                            else
                            {
                                // Build the image
                                lipBalms.Text += p.ProductFileName + "-sm.gif";
                            }

                            // Build the product information
                            lipBalms.Text += "' alt='" + p.ProductName + "' border='0'></a></td><td class='productInformation'><b><a href='" +
                                HOTBAL.TansConstants.PRODUCT_PUBLIC_URL + "?ID=" +
                                p.ProductID + "'>" +
                                p.ProductName + "</a></b></td><td class='productInformation' style='text-align:center !important;'>";

                            // Is this product on sale?
                            if (p.ProductSaleOnline)
                            {
                                // Output the sale price with the regular price underneath
                                lipBalms.Text += "<span style='color:red;'>" +
                                    p.ProductSalePrice.ToString("C") + "</span><br><span style='font-size: 9px'>Reg - " +
                                    p.ProductPrice.ToString("C") + "</span>";
                            }
                            else
                            {
                                lipBalms.Text += p.ProductPrice.ToString("C");
                            }

                            lipBalms.Text += "</td><td class='productInformation'>";

                            // Are there at least two of this product in stock?
                            if (p.ProductCount > 1)
                            {
                                // Add a link to the shopping cart
                                lipBalms.Text += "<a href='" +
                                    HOTBAL.TansConstants.SHOPPING_PUBLIC_URL + "?action=add&ItemID=" +
                                    p.ProductID + "'>Add to Cart</a></td></tr>";
                            }
                            else
                            {
                                // Output that the product is not available for purchase online
                                lipBalms.Text += "<i>Out of Stock Online</i></td></tr>";
                            }

                            // Is the description longer than 150 characters?
                            if (p.ProductDescription.Length > 150)
                            {
                                // Output an abbreviated description
                                lipBalms.Text += "<tr><td colspan='3' class='productDetail' style='height:100px;'><i>" +
                                    p.ProductDescription.Substring(0, 150) + "<a href='" +
                                    HOTBAL.TansConstants.PRODUCT_PUBLIC_URL + "?ID=" +
                                    p.ProductID + "'>...</a></i></td></tr>";
                            }
                            else
                            {
                                // Output the full description
                                lipBalms.Text += "<tr><td colspan='3' class='productDetail' style='height:100px;'><i>" +
                                    p.ProductDescription + "</i></td></tr>";
                            }
                        }

                        // Output the footer
                        lipBalms.Text += "<tfoot><tr><td colspan='4'><br /><b>Can't find what you're looking for here? Visit us in store for more options!</b></td></tr></tfoot>";
                    }
                }
                else
                {
                    // Output that none of the selected type can be found
                    lipBalms.Text = "<tr><td colspan='4'>" +
                        HOTBAL.TansMessages.ERROR_NO_PRODUCT_TYPE + "</td></tr>";
                }
            }
            catch (Exception ex)
            {
                // Log and output error message
                sqlClass.LogErrorMessage(ex, "", "Site: LipBalms");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }
    }
}