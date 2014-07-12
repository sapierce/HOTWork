using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class SiteNotice : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Site Notices";

            if (!Page.IsPostBack)
            {
                if (!functionsClass.isAdmin())
                    Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
                else
                {
                    HOTBAL.SiteNotification currentSiteNotice = sqlClass.GetSiteNotification();
                    if (currentSiteNotice.NoticeID != 0)
                    {
                        noticeText.Text = currentSiteNotice.NoticeText;
                        startDate.Text = currentSiteNotice.StartDate.ToShortDateString();
                        endDate.Text = currentSiteNotice.EndDate.ToShortDateString();
                        noticeID.Value = currentSiteNotice.NoticeID.ToString();
                    }
                    else
                    {
                        startDate.Text = endDate.Text = functionsClass.FormatSlash(DateTime.Now);
                    }
                }
            }
        }

        protected void addNotice_OnClick(object sender, EventArgs e)
        {
            bool success = false;
            if (noticeID.Value != "0")
            {
                // Updating notice
                success = sqlClass.UpdateSiteNotification(Convert.ToInt64(noticeID.Value),
                    noticeText.Text,
                    Convert.ToDateTime(startDate.Text),
                    Convert.ToDateTime(endDate.Text));
            }
            else
            {
                // Inserting notice
                success = sqlClass.InsertSiteNotification(noticeText.Text,
                    Convert.ToDateTime(startDate.Text),
                    Convert.ToDateTime(endDate.Text));
            }

            if (success)
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL, false);
            else
            {
                sqlClass.LogErrorMessage(new Exception("AddNoticeError"), "", "Internal: Admin: addNotice_OnClick");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL + "<br />";
            }
        }
    }
}