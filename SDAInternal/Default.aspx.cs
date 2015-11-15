using System;
using System.Web.UI;

namespace HOTSelfDefense
{
    /// <summary>
    /// This is the default page for the HOT Self Defense scheduling site. On this page
    ///     the selected day's schedule is output along with the option to add addition
    ///     classes or lessons.
    /// </summary>
    public partial class _Default : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods methodClass = new HOTBAL.SDAMethods();

        /// <summary>
        /// This method determines what date we are on for displaying the 
        ///     schedule.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Build the title
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Schedule";

            // Is this a PostBack
            if (!Page.IsPostBack)
            {
                // Does the Date element of the QueryString exist?
                if (Request.QueryString["Date"] != null)
                    // Does the Date element of the QueryString have a value
                    if (!String.IsNullOrEmpty(Request.QueryString["Date"]))
                        // Use the given QueryString Date to populate the schedule
                        outputSchedule.Text = methodClass.GetSchedule(Convert.ToDateTime(Request.QueryString["Date"]));
                    else
                        // Use the current date to populate the schedule
                        outputSchedule.Text = methodClass.GetSchedule(DateTime.Now.AddHours(1));
                else
                    // Use the current date to populate the schedule
                    outputSchedule.Text = methodClass.GetSchedule(DateTime.Now.AddHours(1));
            }
        }

        /// <summary>
        /// This is the OnClick event for the date button. It will repopulate
        ///     the schedule based on the given date.
        /// </summary>
        protected void changeDate_Click(object sender, EventArgs e)
        {
            // Get the value in the scheduleDate text box
                string goToDate = scheduleDate.Text.Trim();

                // Was there a value in the schedule date text box?
                if (String.IsNullOrEmpty(goToDate))
                    // Set the new schedule date to the current date
                    goToDate = DateTime.Now.ToShortDateString();

                // Repopulate the schedule with the given date
                outputSchedule.Text = methodClass.GetSchedule(Convert.ToDateTime(goToDate));
        }
    }
}