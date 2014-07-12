using System;
using System.Collections.Generic;

namespace MobileSite
{
    public partial class Beds : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods methodsClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<HOTBAL.Package> packageRequest = methodsClass.OnlinePackageList("MY");
                mysticFeatures.Text = "• The ONLY official Mystic Tan provider in Waco<br />";
                if (packageRequest != null)
                {
                    mysticPackages.Text = "<table class='PKGL'><tr><td><b>Package</b></td><td><b>Price</b></td><td><br></td></tr>";
                    foreach (HOTBAL.Package p in packageRequest)
                    {
                        mysticPackages.Text += "<tr><td valign='middle'>" + p.PackageName
                            + "</td><td valign='middle'>";
                        if (p.PackageSaleOnline)
                        {
                            mysticPackages.Text += "<span style='color: red;'>" + p.PackageSalePrice + "</span><br><span style='font-size: 9px'>Reg - " + p.PackagePrice + "</span>";
                        }
                        else
                        {
                            mysticPackages.Text += p.PackagePrice;
                        }

                        mysticPackages.Text += "</td><td>";

                        if ((p.PackageAvailableOnline) && (!p.PackageAvailableInStore))
                        {
                            mysticPackages.Text += "<img src='/images/online-only.gif'></td></tr>";
                        }
                        else
                        {
                            mysticPackages.Text += "<br></td></tr>";
                        }
                    }

                    mysticPackages.Text += "</table>";
                }

                packageRequest = methodsClass.OnlinePackageList("BB");

                superFeatures.Text = "<b>Sun Source VHR</b><br />• 32 High-power tanning lamps<br />• 32 High-efficiency 100W ballast<br />• 12 minute bed<br />";
                if (packageRequest != null)
                {
                    superPackages.Text = "<table class='PKGL'><tr><td><b>Package</b></td><td><b>Price</b></td><td><br></td><td><br></td></tr>";
                    foreach (HOTBAL.Package p in packageRequest)
                    {
                        superPackages.Text += "<tr><td valign='middle'>" + p.PackageName
                            + "</td><td valign='middle'>";
                        if (p.PackageSaleOnline)
                        {
                            superPackages.Text += "<span style='color: red;'>" + p.PackageSalePrice + "</span><br><span style='font-size: 9px'>Reg - " + p.PackagePrice + "</span>";
                        }
                        else
                        {
                            superPackages.Text += p.PackagePrice;
                        }

                        superPackages.Text += "</td><td>";

                        if ((p.PackageAvailableOnline) && (!p.PackageAvailableInStore))
                        {
                            superPackages.Text += "<img src='/images/online-only.gif'></td></tr>";
                        }
                        else
                        {
                            superPackages.Text += "<br></td></tr>";
                        }
                    }

                    superPackages.Text += "</table>";
                }

                packageRequest = methodsClass.OnlinePackageList("SB");

                singleFeatures.Text = "<b>SonnenBraune Klassik</b><br>• 26 High-power tanning lamps<br />• 20 minute bed<br />";
                if (packageRequest != null)
                {
                    singlePackages.Text = "<table class='PKGL'><tr><td><b>Package</b></td><td><b>Price</b></td><td><br></td><td><br></td></tr>";
                    foreach (HOTBAL.Package p in packageRequest)
                    {
                        singlePackages.Text += "<tr><td valign='middle'>" + p.PackageName
                            + "</td><td valign='middle'>";
                        if (p.PackageSaleOnline)
                        {
                            singlePackages.Text += "<span style='color: red;'>" + p.PackageSalePrice + "</span><br><span style='font-size: 9px'>Reg - " + p.PackagePrice + "</span>";
                        }
                        else
                        {
                            singlePackages.Text += p.PackagePrice;
                        }

                        singlePackages.Text += "</td><td>";

                        if ((p.PackageAvailableOnline) && (!p.PackageAvailableInStore))
                        {
                            singlePackages.Text += "<img src='/images/online-only.gif'></td></tr>";
                        }
                        else
                        {
                            singlePackages.Text += "<br></td></tr>";
                        }
                    }

                    singlePackages.Text += "</table>";
                }
            }
            catch (Exception ex)
            {
                methodsClass.LogErrorMessage(ex, "", "Mobile: PackageDetails");
            }
        }
    }
}