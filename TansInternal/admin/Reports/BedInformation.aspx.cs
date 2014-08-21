using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class BedInformation : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Bed Information";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);

            if (Request.QueryString["Date"] != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["Date"]))
                {
                    DateTime summaryDate = Convert.ToDateTime(Request.QueryString["Date"].ToString().Trim());
                    string[] tanListing = sqlClass.GetBedSummaryByDate(summaryDate);

                    if (tanListing != null)
                    {
                        if (tanListing.Length > 0)
                        {
                            foreach (string summary in tanListing)
                            {
                                string[] splitSummary = summary.Split(Convert.ToChar(","));

                                bedInformationList.Text += "<tr><td>" + splitSummary[0] + "</td><td>" + splitSummary[2] + "</td><td>" + splitSummary[1] + "</td></tr>";
                            }
                        }
                        else
                        {
                            bedInformationList.Text += "<tr><td colspan='3'>No bed information found for " + functionsClass.FormatSlash(summaryDate) + "</td></tr>";
                        }
                    }
                    else
                    {
                        bedInformationList.Text += "<tr><td colspan='3'>No bed information found for " + functionsClass.FormatSlash(summaryDate) + "</td></tr>";
                    }
                }
                else
                {
                    bedInformationList.Text += "<tr><td colspan='3'>No bed information found</td></tr>";
                }
            }
            else
            {
                bedInformationList.Text += "<tr><td colspan='3'>No bed information found</td></tr>";
            }
        }
    }
}