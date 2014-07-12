using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class About : System.Web.UI.Page
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
            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - About Us";

            // Get the store hours
            // Get the Waco Tanning Hours
            buildStoreHours("W", "T");
        }

        /// <summary>
        /// This method gets and outputs the store hours for the given location and location type
        /// </summary>
        /// <param name="location">For which location do we want hours?</param>
        /// <param name="locationType">What type of hours do we want?</param>
        private void buildStoreHours(string location, string locationType)
        {
            try
            {
                List<HOTBAL.Time> storeTimes = sqlClass.GetLocationTimes(location, locationType);

                if (storeTimes != null)
                {
                    string timeTable = "<table class='tanning'><thead><tr><th colspan='3'>Hours of Operation</th></tr></thead><tbody>";

                    // Found hours for location and type given
                    foreach (HOTBAL.Time time in storeTimes)
                    {
                        // Loop through each received time and ouput it
                        if (time != null)
                        {
                            timeTable += "<tr><td class='rightAlignHeader'>" + time.WebTimeDay + "</td><td>" +
                                    time.BeginTime + "</td><td>" +
                                    time.EndTime + "</td></tr>";
                        }
                    }
                    timeTable += "</tbody><tfoot><tr><td colspan='3'><em>Last appointments are taken 30 minutes before closing</em></td></tr></tfoot></table>";
                    hoursOfOperation.Text = timeTable;
                }
                else
                {
                    // Unable to find any hours for location and type given
                    hoursOfOperation.Text = HOTBAL.TansMessages.ERROR_ABOUT_HOURS;
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "Site: About: PageLoad");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC + "<br />";
            }
        }
    }
}