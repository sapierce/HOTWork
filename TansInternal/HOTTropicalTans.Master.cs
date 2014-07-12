using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace HOTTropicalTans
{
    public partial class InternalSite : System.Web.UI.MasterPage
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();

        public void Page_Load(object sender, EventArgs e)
        {
            DateTime twoWeekDate = DateTime.Now.AddDays(13);
            DateTime monthDate = DateTime.Now.AddDays(29);

            todayDate.Text = currentDate.Text = functionsClass.FormatSlash(DateTime.Now.AddHours(1));
            twoWeek.Text = functionsClass.FormatSlash(twoWeekDate);
            month.Text = functionsClass.FormatSlash(monthDate);

            viewSchedule.NavigateUrl = HOTBAL.TansConstants.MAIN_INTERNAL_URL;
            addCustomer.NavigateUrl = HOTBAL.TansConstants.CUSTOMER_ADD_INTERNAL_URL;
            giftCards.NavigateUrl = HOTBAL.POSConstants.GIFT_CARDS_URL;
            adminSection.NavigateUrl = HOTBAL.TansConstants.ADMIN_INTERNAL_URL;
            employeeClock.NavigateUrl = HOTBAL.TansConstants.EMP_INTERNAL_URL;
            reportProblem.NavigateUrl = HOTBAL.TansConstants.PROBLEMS_INTERNAL_URL;
            pointOfSale.NavigateUrl = HOTBAL.TansConstants.POS_INTERNAL_URL;
            search.NavigateUrl = HOTBAL.TansConstants.SEARCH_INTERNAL_URL;
            maPointOfSale.NavigateUrl = HOTBAL.SDAConstants.ROOT_URL + HOTBAL.SDAConstants.POS_INTERNAL_URL;

            if (ConfigurationManager.AppSettings["MassageEnabled"].ToString() == "Y")
            {
                viewMassageSchedule.Visible = true;
                viewMassageSchedule.NavigateUrl = "/Schedule/ScheduleMassage.aspx";
            }
            else
                viewMassageSchedule.Visible = false;
        }
    }
}
