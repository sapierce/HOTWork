using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using HOTBAL;

namespace SDAPOS
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public string scheduleDate = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime scheduleDate = DateTime.Now.AddHours(1);
            HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();

            // Does the Date element exist?
            if (Request.QueryString["Date"] != null)
            {
                // Does the Date element have a value?
                if (!String.IsNullOrEmpty(Request.QueryString["Date"]))
                {
                    // Use the Date element from the QueryString
                    scheduleDate = Convert.ToDateTime(Request.QueryString["Date"].ToString());
                    displayToday.Text = FunctionsClass.FormatSlash(Convert.ToDateTime(scheduleDate));
                    displayCurrent.Text = FunctionsClass.FormatSlash(Convert.ToDateTime(scheduleDate));
                }
            }

            if (String.IsNullOrEmpty(displayCurrent.Text))
            {
                // Use the default value
                displayToday.Text = FunctionsClass.FormatSlash(scheduleDate);
                displayCurrent.Text = FunctionsClass.FormatSlash(scheduleDate);
            }

            // Build the navigation URLs
            dailySchedule.NavigateUrl = HOTBAL.SDAConstants.MAIN_INTERNAL_URL;
            addClass.NavigateUrl = HOTBAL.SDAConstants.ADD_CLASS_INTERNAL_URL;
            addStudent.NavigateUrl = HOTBAL.SDAConstants.STUDENT_ADD_INTERNAL_URL;
            searchSDA.NavigateUrl = HOTBAL.SDAConstants.SEARCH_INTERNAL_URL;
            sdaPOS.NavigateUrl = HOTBAL.SDAConstants.POS_INTERNAL_URL;
            sdaAdministration.NavigateUrl = HOTBAL.SDAConstants.ADMIN_INTERNAL_URL;
            reportProblem.NavigateUrl = HOTBAL.SDAConstants.PROBLEMS_INTERAL_URL;
        }
    }
}
