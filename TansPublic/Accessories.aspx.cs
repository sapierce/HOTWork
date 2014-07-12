using System;
using System.Web.UI;

namespace PublicWebsite
{
    public partial class Accessories : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Accessories";
        }
    }
}