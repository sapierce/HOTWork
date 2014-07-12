using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileSite
{
    public partial class About : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            string timeTable;
            try
            {
                List<HOTBAL.Time> wacoTimes = sqlClass.GetLocationTimes("W", "T");

                timeTable = "<table class='table'>";
                timeTable += "<thead><th colspan='3' class='tableHeader'>Hours of Operation</th></thead>";
                if (wacoTimes != null)
                {
                    //We have times for Waco
                    foreach (HOTBAL.Time x in wacoTimes)
                    {
                        //Loop through each received time and ouput it
                        if (x != null)
                        {
                            timeTable += "<tr><td class='tableCellHeaderRight'>" + x.WebTimeDay + "</td><td class='tableCell'>" +
                                    x.BeginTime + "</td><td class='tableCell'>" +
                                    x.EndTime + "</td></tr>";
                        }
                    }
                    timeTable += "</table>";
                    hoursOfOperation.Text = timeTable;
                }
                else
                {
                    // We didn't find any hours for Waco
                    hoursOfOperation.Text = HOTBAL.TansMessages.ERROR_ABOUT_HOURS;
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "Mobile: About"); 
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + HOTBAL.TansMessages.ERROR_GENERIC + "<br />";
            }
        }
    }
}
