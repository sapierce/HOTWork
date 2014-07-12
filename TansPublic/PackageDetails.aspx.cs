using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class PackageDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
            HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

            try
            {
                List<HOTBAL.Package> packageRequest = sqlClass.OnlinePackageList(Request.QueryString["Type"].ToString().Trim().ToUpper());

                if (functionsClass.CleanUp(Request.QueryString["Type"].ToString().Trim().ToUpper()) == "MY")
                {
                    Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Mystic Tanning";
                    bedPicture.Text = "<img src='images/mystic-bed.gif' border='0' alt='Mystic Tanning' />";
                    bedFeatures.Text = "<img src='images/MysticTan Logo small.gif' alt='Authentic MysticTan Partner' /><br />• The ONLY official Mystic Tan provider in Waco<br />• <a href='#mystic' class='center'>See what a Mystic Tan can do for you!</a> <br />";
                    bedAdditional.Text = "<a name='mystic'><table><tr><td valign='top'><img src='images/mystic3.jpg' alt='Before Mystic'></td><td valign='top'><img src='images/mystic2.jpg' alt='After One Mystic Tan'></td><td valign='top'><img src='images/mystic1.jpg' alt='After Two Mystic Sessions'></td></tr></table>";
                }
                else if (functionsClass.CleanUp(Request.QueryString["Type"].ToString().Trim().ToUpper()) == "BB")
                {
                    Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Super Beds";
                    bedPicture.Text = "<img src='images/super-bed.gif' border='0' alt='Super Bed Tanning' />";
                    bedFeatures.Text = "<b>Sun Source VHR</b><br />• 32 High-power tanning lamps<br />• 32 High-efficiency 100W ballast<br />• 12 minute bed<br />";
                }
                else if (functionsClass.CleanUp(Request.QueryString["Type"].ToString().Trim().ToUpper()) == "SB")
                {
                    Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Single Beds";
                    bedPicture.Text = "<img src='images/single-bed.gif' border='0' alt='Single Bed Tanning' />";
                    bedFeatures.Text = "<b>SonnenBraune Klassik</b><br>• 26 High-power tanning lamps<br />• 20 minute bed<br />";
                }
                else
                {
                    Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Tanning Beds";
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                    sqlClass.LogErrorMessage(new Exception("PackageNotFound"), Request.QueryString["Type"].ToString().Trim().ToUpper(), "Site: PackageDetails");
                }

                if (packageRequest != null)
                {
                    bedPackages.Text = "<table class='PKGL'><tr><td><b>Package</b></td><td><b>Price</b></td><td><br></td><td><br></td></tr>";

                    foreach (HOTBAL.Package p in packageRequest)
                    {
                        bedPackages.Text += "<tr><td valign='middle'>" + p.PackageName
                            + "</td><td valign='middle'>";
                        if (p.PackageSaleOnline)
                        {
                            bedPackages.Text += "<span style='color: red;'>" + p.PackageSalePrice + "</span><br><span style='font-size: 9px'>Reg - " + p.PackagePrice + "</span>";
                        }
                        else
                        {
                            bedPackages.Text += p.PackagePrice;
                        }

                        if (p.PackageName != "No packages available")
                        {
                            bedPackages.Text += "</td><td style='vertical-align: middle'><a href='" + HOTBAL.TansConstants.SHOPPING_PUBLIC_URL + "?action=add&ItemID=" + p.PackageID + "' class='center'>Add to Cart</a></td><td>";
                        }
                        else
                        {
                            bedPackages.Text += "</td><td style='vertical-align: middle'></td><td>";
                        }

                        if ((p.PackageAvailableOnline) && (!p.PackageAvailableInStore))
                        {
                            bedPackages.Text += "<img src='images/online-only.gif'></td></tr>";
                        }
                        else
                        {
                            bedPackages.Text += "<br></td></tr>";
                        }
                    }

                    bedPackages.Text += "</table>";
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                sqlClass.LogErrorMessage(ex, Request.QueryString["Type"].ToString().Trim().ToUpper(), "Site: PackageDetails: Catch");
            }
        }
    }
}