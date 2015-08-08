using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTPOS
{
    public partial class TransactionVoid : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass tansFunctionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods tansSqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["ID"].ToString().Trim()))
                {
                    int transactionID = Convert.ToInt32(Request.QueryString["ID"].ToString().Trim());
                    HOTBAL.Transaction transactionInformation = tansSqlClass.GetCustomerTransaction(transactionID);

                    if (Request.QueryString["Task"] != null)
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["Task"].ToString().Trim()))
                        {
                            if (Request.QueryString["Task"].ToString().Trim() == "Void")
                                // Voiding this transaction
                                tansSqlClass.UpdateTransaction(transactionID, transactionInformation.SellerId, transactionInformation.TransactionTotal.ToString(), tansFunctionsClass.FormatDash(transactionInformation.TransactionDate), transactionInformation.PaymentMethod, true, transactionInformation.IsTransactionPaid);
                            else if (Request.QueryString["Task"].ToString().Trim() == "Unvoid")
                                // Unvoiding this transaction
                                tansSqlClass.UpdateTransaction(transactionID, transactionInformation.SellerId, transactionInformation.TransactionTotal.ToString(), tansFunctionsClass.FormatDash(transactionInformation.TransactionDate), transactionInformation.PaymentMethod, false, transactionInformation.IsTransactionPaid);
                        }
                    }
                }
            }

            Response.Redirect(HOTBAL.POSConstants.DEFAULT_URL, false);
        }
    }
}