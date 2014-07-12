using System;
using System.Web.UI.WebControls;

namespace HOTPOS
{
    public partial class TransactionItemDelete : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass TansFunctionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods TansSqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                int transactionNumber = Convert.ToInt32(Request.QueryString["ID"].ToString());
                bool isSuccess = TansSqlClass.DeleteTanningTransactionItems(transactionNumber);

                if (!isSuccess)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "&#149; " + HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                }
                else
                {
                    Response.Redirect(HOTBAL.POSConstants.TRANSACTION_DETAILS_URL + "?ID=" + Request.QueryString["TID"].ToString());
                }
            }
        }
    }
}