using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PublicWebsite
{
    public partial class _Default : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the page name
            Page.Title = HOTBAL.TansConstants.PUBLIC_NAME;

            // Set the welcome notice text
            welcomeNotice.Text = "Welcome to HOT Tropical Tans!";

            // Set the default page text
            mainSiteText.Text = "Waco's ONLY official Mystic Tan provider as well as a provider of superior regular tanning. <br /><br />" +
                "We offer single sessions, multiple sessions, and several unlimited packages at a variety of tanning levels. Check out our <a href='" +
                HOTBAL.TansConstants.SPECIALS_PUBLIC_URL + "'>online specials</a>, <a href='" +
                HOTBAL.TansConstants.MEMBERS_PUBLIC_URL + "'>member benefits</a>, and wide array of <a href='" +
                HOTBAL.TansConstants.ACCESSORIES_PUBLIC_URL + "'>tanning accessories</a>.<br /><br />" +
                "Join today!";
            //Page.Master.FindControl("MemberInfo").Controls.Add(functionsClass.MemberLoggedIn());

            try
            {
                // Get any site notifications
                HOTBAL.SiteNotification getSiteNotice = sqlClass.GetSiteNotification();

                // Were there any site notifications?
                if (!String.IsNullOrEmpty(getSiteNotice.NoticeText))
                {
                    // Output the site notification
                    siteNotice.Text = "<table class='noticeBox'><tr><td>" + getSiteNotice.NoticeText + "</td></tr></table>";
                }
            }
            catch (Exception ex)
            {
                // Log and show error message
                sqlClass.LogErrorMessage(ex, "", "Site: Default: PageLoad");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }
    }
}