using System;
using System.Web.UI;

namespace HOTTropicalTans.admin
{
    public partial class CustomerComments : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Customer Comments";

            if (!functionsClass.isAdmin())
                Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
            else
            {
                //TODO
                //List<HOTDAL.Comment> unansweredComments = sqlClass.GetAllUnRepliedComments();

                //if (unansweredComments != null)
                //{
                //    foreach (HOTDAL.Comment comment in unansweredComments)
                //    {
                //        customerComments.Text += "<tr><td><a href='" + HOTBAL.TansConstants.CUSTOMER_REPLY_INTERNAL_URL +
                //            "?ID=" + comment.CommentID + "'>Reply</a></td><td>" +
                //            comment.CommentTime + "</td><td>" +
                //            comment.CustomerName + "</td><td>" +
                //            comment.CustomerEmail + "</td><td>" +
                //            comment.CommentAbout + "</td><td>" +
                //            comment.CommentText + "</td></tr>";
                //    }
                //}
                //else
                //{
                //    customerComments.Text = "<tr><td colspan='6'>No unanswered comments</td></tr>";
                //}
            }
        }
    }
}