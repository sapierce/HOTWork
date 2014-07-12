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
            SDAFunctionsClass FunctionsClass = new SDAFunctionsClass();
            if (String.IsNullOrEmpty(Request.QueryString["Date"]))
            {
                scheduleDate = FunctionsClass.FormatDash(DateTime.Now.AddHours(1));
                lblToday.Text = FunctionsClass.FormatSlash(Convert.ToDateTime(scheduleDate));
                lblCurrent.Text = FunctionsClass.FormatSlash(Convert.ToDateTime(scheduleDate));
            }
            else
            {
                scheduleDate = FunctionsClass.FormatDash(Convert.ToDateTime(Request.QueryString["Date"]));
                lblCurrent.Text = FunctionsClass.FormatSlash(Convert.ToDateTime(scheduleDate));
                scheduleDate = FunctionsClass.FormatDash(DateTime.Now.AddHours(1));
                lblToday.Text = FunctionsClass.FormatSlash(Convert.ToDateTime(scheduleDate));
            }

            lnkSchedule.NavigateUrl = HOTBAL.SDAConstants.MAIN_INTERNAL_URL + "?Date=" + scheduleDate;
            lnkAddClass.NavigateUrl = HOTBAL.SDAConstants.ADD_CLASS_INTERNAL_URL + "?Date=" + scheduleDate;
            lnkAddStudent.NavigateUrl = HOTBAL.SDAConstants.STUDENT_ADD_INTERNAL_URL + "?Date=" + scheduleDate;
            lnkAdmin.NavigateUrl = HOTBAL.SDAConstants.ADMIN_INTERNAL_URL + "?Date=" + scheduleDate;
            lnkProblem.NavigateUrl = HOTBAL.SDAConstants.PROBLEMS_INTERAL_URL + "?Date=" + scheduleDate;
            lnkPOS.NavigateUrl = HOTBAL.SDAConstants.POS_INTERNAL_URL + "?Date=" + scheduleDate;
            lnkSearch.NavigateUrl = HOTBAL.SDAConstants.SEARCH_INTERNAL_URL + "?Date=" + scheduleDate;
        }
    }
}
